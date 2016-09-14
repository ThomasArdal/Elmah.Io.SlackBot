using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elmah.Io.Client;

namespace Elmah.Io.SlackBot
{
    public class ElmahIoResponse
    {
        public List<Message> Messages { get; set; }
        public int Total { get; set; }

    }
}
