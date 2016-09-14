using Autofac;
using Elmah.Io.SlackBot.Commands;
using Elmah.Io.SlackBot.Users;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Nancy.Elmah;
using RestSharp;

namespace Elmah.Io.SlackBot
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            Elmahlogging.Enable(pipelines, "elmah");
        }

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