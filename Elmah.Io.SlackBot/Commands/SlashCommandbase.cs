using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Elmah.Io.SlackBot.Commands
{
    public abstract class SlashCommandBase
    {
        protected IRestClient Client { get; private set; }

        protected SlashCommandBase(IRestClient client)
        {
            Client = client;
        }

        public abstract object Run(string[] args);        
    }
}
