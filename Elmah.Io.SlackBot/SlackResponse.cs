using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Elmah.Io.SlackBot
{
    public class SlackResponse
    {
        public string Text { get; set; }
        public List<SlackAttachment> Attachments { get; set; } = new List<SlackAttachment>();

    }

    public class SlackAttachment
    {
        public string Text { get; set; }
        public List<ButtonAction> Actions { get; set; } = new List<ButtonAction>();
    }

    public class ButtonAction
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string Type { get; } = "button";
        public string Value { get; set; } 
    }
}