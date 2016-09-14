using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentScheduler;
using RestSharp;

namespace Elmah.Io.SlackBot.Alerting
{
    public class AlertRunner
    {
        private readonly IRestClient restClient;

        public AlertRunner(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        public void Create(AlertPolicy policy)
        {
            JobManager.AddJob(() =>
            {
                var request = new RestRequest($"/messages?logid={policy.LogId}&pagesize=0&query={policy.Query}&from={DateTime.UtcNow.AddMinutes(-policy.Minutes)}&to={DateTime.UtcNow}");
            }, s => s.ToRunEvery(30).Seconds());
        }
    }
}
