using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.BaseClasses;

namespace TrelloAPI.APIs
{
    internal class TrelloAPI(string url) : BaseAPI("https://api.trello.com/1/" + url)
    {
        private string APIKey = Environment.EnvironmentSettings.APIKey;
        private string APIToken = Environment.EnvironmentSettings.APIToken;

        protected Dictionary<string, object> GetSecurityHeaders()
        {
            return new Dictionary<string, object>()
            {
                { "key", APIKey },
                { "token", APIToken }
            };
        }
    }
}
