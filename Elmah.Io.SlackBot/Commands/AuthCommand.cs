using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Elmah.Io.SlackBot.Users;
using RestSharp;

namespace Elmah.Io.SlackBot.Commands
{
    [SlashCommand("auth")]
    public class AuthCommand : SlashCommandBase
    {
        private readonly IAuthRepository authRepository;

        public AuthCommand(IRestClient client, IAuthRepository authRepository) : base(client)
        {
            this.authRepository = authRepository;
        }

        public override object Run(string[] args)
        {
            authRepository.SetAuthToken(args[1], args[0]);
            return "Ok";
        }
    }
}