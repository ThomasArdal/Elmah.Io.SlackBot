using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Input;
using Autofac;
using Elmah.Io.Client;
using Elmah.Io.SlackBot.Commands;
using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using RestSharp;

namespace Elmah.Io.SlackBot.Modules
{
    public class SlashCommandModule : NancyModule
    {

        public SlashCommandModule(ILifetimeScope scope)
        {
            Post["/handle"] = p =>
            {
                var command = this.Bind<SlashCommand>();
                var args = command.text.Split(' ').ToList();
                args.Add(command.team_id); 
                return scope.ResolveKeyed<SlashCommandBase>(args[0]).Run(args.Skip(1).ToArray());
            };
        }
    }
}