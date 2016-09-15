using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elmah.Io.SlackBot.Alerting;
using SlackAPI;

namespace Elmah.Io.SlackBot.Bot
{
    public static class SlackAlertBot
    {

        public static void PostMessage(string authToken, string text, string channelName)
        {
            var client = new SlackSocketClient(authToken);
            
            client.Connect((connected) =>
            {
                //This is called once the client has emitted the RTM start command
            }, () =>
            {
                client.GetChannelList(channelList =>
                {
                    var channel = channelList.channels.FirstOrDefault(c => c.name == channelName.Replace("#", ""));
                    client.PostMessage(x => { client.CloseSocket(); }, channel.id, text, "The Log Lady");
                });
                //This is called once the RTM client has connected to the end point
            });


        }
    }
}
