using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elmah.Io.SlackBot.Alerting
{
    public class AlertPolicy
    {
        public string Id => $"{LogId}-{Query}";

        public string LogId { get; set; }
        public string Channel { get; set; }
        public int Count { get; set; }
        public int Minutes { get; set; }
        public string Query { get; set; }

        public DateTime? AlertStarted { get; set; }
    }
}
