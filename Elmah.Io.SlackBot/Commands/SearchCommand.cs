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
            var logAlias = args[0];
            var query = args[1];
            var teamId = args[2];
            var log = userRepository.GetLog(teamId, logAlias);
            var request = new RestRequest($"messages?logid={log.LogId}&pagesize=20&query={query}", Method.GET);
            var result = Client.Execute<List<Message>>(request);
            var content = JsonConvert.DeserializeObject<ElmahIoResponse>(result.Content);
            return content.Messages.Select(q => $"{q.DateTime} {q.Title} {q.Application} {q.Source}"); // <-- format response according to Slack's docs
        }
    }
}
