using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Elmah.Io.SlackBot;
using Microsoft.Owin;
using Owin;

namespace Elmah.Io.SlackBot
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();
        }
    }
}