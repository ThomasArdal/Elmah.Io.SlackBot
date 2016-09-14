using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Elmah.Io.SlackBot.Commands;
using Elmah.Io.SlackBot.Users;
using Nancy;
using Nancy.Bootstrappers.Autofac;
using RestSharp;

namespace Elmah.Io.SlackBot
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(ILifetimeScope container)
        {
            container.Update(builder =>
            {
                builder.Register<IRestClient>(h => new RestClient("https://elmah.io/api/v2")).SingleInstance();
                builder.Register<IUserRepository>(h => new UserRepositoryMock()).SingleInstance();
                builder.RegisterModule<CommandModule>();
            });
        }
    }
}