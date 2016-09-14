using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Elmah.Io.SlackBot.Users;
using RestSharp;
using Module = Autofac.Module;

namespace Elmah.Io.SlackBot.Commands
{
    public class CommandModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            ThisAssembly.GetTypes().ToList().ForEach(p =>
            {
                var attribute = p.GetCustomAttribute<SlashCommandAttribute>();
                if (attribute != null)
                {
                    builder.RegisterType(p).Keyed<SlashCommandBase>(attribute.Name);
                }
            });
        }
    }
}