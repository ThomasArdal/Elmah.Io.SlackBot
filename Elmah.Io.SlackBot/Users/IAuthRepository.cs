using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elmah.Io.SlackBot.Users
{
    public interface IAuthRepository
    {
        string GetAuthToken(string teamId);
        void SetAuthToken(string teamId, string authToken);
    }
}
