using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elmah.Io.SlackBot.Alerting;
using Elmah.Io.SlackBot.Bot;
using Elmah.Io.SlackBot.Extensions;
using Elmah.Io.SlackBot.Users;
using FluentScheduler;
using Newtonsoft.Json;
using RestSharp;

namespace Elmah.Io.SlackBot.Commands
{
    [SlashCommand("alert")]
    public class AlertCommand : SlashCommandBase
    {
        private readonly IUserRepository userRepository;
        private readonly IAuthRepository authRepository;

        public AlertCommand(IRestClient client, IUserRepository userRepository, IAuthRepository authRepository) : base(client)
        {
            this.userRepository = userRepository;
            this.authRepository = authRepository;
        }

        public override object Run(string[] args)
        {
            var teamId = args.LastOrDefault();
            var log = userRepository.GetLog(teamId, args[0]);

            var policy = new AlertPolicy();
            policy.LogId = log.LogId;
            policy.Query = args[2];
            policy.Count = int.Parse(args[4]);
            policy.Minutes = int.Parse(args[6]);
            policy.Channel = args[9];

            //TODO - save policy so it can be read back after app restart

            JobManager.AddJob(() =>
            {
                var request = new RestRequest($"/messages?logid={policy.LogId}&pagesize=0&query={policy.Query}&from={DateTime.UtcNow.AddMinutes(-policy.Minutes)}&to={DateTime.UtcNow}");
                var response = Client.DeserializeTo<ElmahIoResponse>(request);
                if (response.Total > policy.Count)
                {
                    if (policy.AlertStarted == null)
                    {
                        policy.AlertStarted = DateTime.UtcNow;
                        var authToken = authRepository.GetAuthToken(teamId);
                        SlackAlertBot.PostMessage(authToken,
                            $"Alert in `{log.Alias}` for query `{policy.Query}` is greater {policy.Count} logs entries in {policy.Minutes} minutes!\n<https://elmah.io/errorlog/search?logId={policy.LogId}&freeText={policy.Query}&hidden=false&groupBy=&sort=time&order=desc#searchTab|Go to log>",
                            policy.Channel);

                        //Realert after 30 min
                        JobManager.AddJob(() => policy.AlertStarted = null, s => s.ToRunOnceIn(30).Minutes());
                    }
                }
                else if (policy.AlertStarted != null)
                {

                    var authToken = authRepository.GetAuthToken(teamId);
                    SlackAlertBot.PostMessage(authToken,
                        $"Alert in `{log.Alias}` for query `{policy.Query}` closed after {(int)Math.Ceiling((DateTime.UtcNow - policy.AlertStarted.Value).TotalMinutes)}",
                        policy.Channel);
                    policy.AlertStarted = null;
                }
            }, s => s.ToRunNow().AndEvery(30).Seconds());

            return new SlackResponse { Text = "Alert created" };
        }
    }
}
