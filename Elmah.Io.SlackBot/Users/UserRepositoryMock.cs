using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elmah.Io.SlackBot.Users
{

    /// <summary>
    /// To be replaced with some data store repo or users will be wiped when api is restarted :-)
    /// </summary>
    public class UserRepositoryMock : IUserRepository
    {
        private static List<ElmahLog> DataStore { get; } = new List<ElmahLog>();

        public void RegisterLog(ElmahLog item)
        {
            DataStore.Add(item);
        }

        public List<ElmahLog> GetAll(string teamId)
        {
            return DataStore.Where(p => p.TeamId == teamId).ToList();
        }

        public ElmahLog GetLog(string teamId, string alias)
        {
            return DataStore.FirstOrDefault(p => p.TeamId == teamId && p.Alias == alias);
        }
    }
}
