using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elmah.Io.Client;
using Elmah.Io.SlackBot.Users;
using Newtonsoft.Json;
using RestSharp;

namespace Elmah.Io.SlackBot.Commands
{
    public class SearchCommand : SlashCommandBase
    {
        private readonly IUserRepository userRepository;

        public SearchCommand(IRestClient client, IUserRepository userRepository) : base(client)
        {
            this.userRepository = userRepository;
        }

        public override object Run(string[] args)
        {
            if (args.Length < 3)
            {
                return new SlackResponse {Text = "Usage: `/elmah search [log_alias] [query]`"};
            }
            var logAlias = args[0];
            var query = args[1];
            var teamId = args[2];
            var log = userRepository.GetLog(teamId, logAlias);
            if (log == null)
            {
                return new SlackResponse {Text = "Log not found. Has it been registered? `/elmah register [log_id] [log_alias]`"};
            }
            var request = new RestRequest($"messages?logid={log.LogId}&pagesize=20&query={query}", Method.GET);
            var result = Client.Execute<List<Message>>(request);
            var content = JsonConvert.DeserializeObject<ElmahIoResponse>(result.Content);
            var response = new SlackResponse
            {
                Text =
                    string.Join("\n",
                        content.Messages.Select(q => $"{q.DateTime} {q.Title} {q.Application} {q.Source}\n"))
            };
            var attachment = new SlackAttachment();
            attachment.Actions.Add(new ButtonAction { Text = "Send to channel", Name = "send_to_channel", Value = string.Join(",", args) });
            attachment.Actions.Add(new ButtonAction { Text = "Ignore matching errors", Name = "ignore", Value = string.Join(",", args) });
            response.Attachments.Add(attachment);
            return response;
            
        }
    }
}
