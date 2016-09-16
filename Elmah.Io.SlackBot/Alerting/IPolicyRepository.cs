using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Elmah.Io.SlackBot.Alerting
{
    public interface IPolicyRepository
    {
        void AddOrUpdate(AlertPolicy policy);
        void Remove(string key);
        AlertPolicy Get(string key);
    }
}