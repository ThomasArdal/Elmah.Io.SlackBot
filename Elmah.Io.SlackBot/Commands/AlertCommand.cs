using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elmah.Io.SlackBot.Alerting;
using Elmah.Io.SlackBot.Bot;
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
            // [alias] when [query] > [threshold] in [minutes] minutes to [channel]

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
                var response = Client.Execute<ElmahIoResponse>(request);
                var elmahResponse = JsonConvert.DeserializeObject<ElmahIoResponse>(response.Content);
                if (elmahResponse.Total > policy.Count)
                {
                    var authToken = authRepository.GetAuthToken(teamId);
                    SlackAlertBot.PostMessage(authToken, $"Alert for {policy.Query} > {policy.Count} in {policy.Minutes} minutes!\n<https://elmah.io/errorlog/search?logId={policy.LogId}&freeText={policy.Query}&hidden=false&groupBy=&sort=time&order=desc#searchTab|Go to log>", policy.Channel);
                }
            }, s => s.ToRunNow().AndEvery(30).Seconds());

            return new SlackResponse { Text = "Alert created" };


        }
    }
}
