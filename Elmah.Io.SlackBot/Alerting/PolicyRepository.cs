using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Elmah.Io.SlackBot.Extensions;

namespace Elmah.Io.SlackBot.Alerting
{
    public class PolicyRepository : IPolicyRepository
    {
        private Dictionary<string, AlertPolicy> _dataStore = new Dictionary<string, AlertPolicy>();

        public void AddOrUpdate(AlertPolicy policy)
        {
            _dataStore.AddOrUpdate(policy.Id, policy);
        }

        public void Remove(string key)
        {
            _dataStore.Remove(key);
        }

        public AlertPolicy Get(string key)
        {
            return _dataStore[key];
        }
    }
}