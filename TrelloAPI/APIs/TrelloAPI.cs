using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.BaseClasses;

namespace TrelloAPI.APIs
{
    internal class TrelloAPI : BaseAPI
    {
        private const string APIKey = "f3001b9dcbdc1e91b3d72fca3a6ab921";
        private const string APIToken = "ATTAb1c68e2b28e8b02339c8b97f695d558043f3a928286250cfc3413d1fdf1e9a73679CB3EA";

        public TrelloAPI(string url) : base("https://api.trello.com/1/" + url)
        {

        }

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
