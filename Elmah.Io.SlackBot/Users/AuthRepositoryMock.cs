using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elmah.Io.SlackBot.Users
{
    public class AuthRepositoryMock : IAuthRepository
    {
        private static readonly Dictionary<string, string> _dataStore = new Dictionary<string, string>();
        public string GetAuthToken(string teamId)
        {
            return _dataStore[teamId];
        }

        public void SetAuthToken(string teamId, string authToken)
        {
            _dataStore.Add(teamId, authToken);
        }
    }
}
