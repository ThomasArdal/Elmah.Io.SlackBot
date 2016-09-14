using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Elmah.Io.SlackBot.Users;
using RestSharp;

namespace Elmah.Io.SlackBot.Commands
{
    public class CommandModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<SlashCommandBase>(h => new SearchCommand(h.Resolve<IRestClient>(), h.Resolve<IUserRepository>())).Keyed<SlashCommandBase>("search");
            builder.Register<SlashCommandBase>(h => new RegisterCommand(h.Resolve<IRestClient>(), h.Resolve<IUserRepository>())).Keyed<SlashCommandBase>("register");
        }
    }
}