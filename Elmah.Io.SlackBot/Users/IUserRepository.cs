using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elmah.Io.SlackBot.Users
{
    public interface IUserRepository
    {
        void RegisterLog(ElmahLog item);
        List<ElmahLog> GetAll(string teamId);
        ElmahLog GetLog(string teamId, string alias);
    }
}
