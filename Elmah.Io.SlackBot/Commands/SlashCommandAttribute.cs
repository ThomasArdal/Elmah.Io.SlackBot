using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elmah.Io.SlackBot.Commands
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SlashCommandAttribute : Attribute
    {
        public string Name { get; private set; }

        public SlashCommandAttribute(string name)
        {
            Name = name;
        }
    }
}
