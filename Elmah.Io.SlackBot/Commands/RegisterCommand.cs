using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elmah.Io.SlackBot.Users;
using RestSharp;

namespace Elmah.Io.SlackBot.Commands
{
    public class RegisterCommand : SlashCommandBase
    {
        private readonly IUserRepository userRepository;

        public RegisterCommand(IRestClient client, IUserRepository userRepository) : base(client)
        {
            this.userRepository = userRepository;
        }

        public override object Run(string[] args)
        {
            userRepository.RegisterLog(new ElmahLog {LogId = args[0], Alias = args[1], TeamId = args[2]});
            return null;
        }
    }
}
