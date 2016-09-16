using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace Elmah.Io.SlackBot.Extensions
{
    public static class RestClientExtensions
    {
        public static T DeserializeTo<T>(this IRestClient client, IRestRequest request) where T : new()
        {
            var response = client.Execute<T>(request);
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}
