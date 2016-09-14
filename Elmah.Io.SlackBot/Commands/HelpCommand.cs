using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Elmah.Io.SlackBot.Commands
{
    public class HelpCommand : SlashCommandBase
    {
        public HelpCommand(IRestClient client) : base(client)
        {
        }

        public override object Run(string[] args)
        {
            return new SlackResponse
            {
                Text = @"Available commands\n"
                       + "`/elmah config [elmah_io_logid] [alias]`. - Maps your elmah.io log id to an alias\n"
            };
        }
    }
}
