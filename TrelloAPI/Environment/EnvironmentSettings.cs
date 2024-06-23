using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities;
using Newtonsoft.Json.Linq;

namespace TrelloAPI.Environment
{
    internal static class EnvironmentSettings
    {
        internal static string APIKey = "";
        internal static string APIToken = "";

        static EnvironmentSettings()
        {
            LoadEnvironmentValues(Utilities.ReadJsonFile("./../../../Environment/Environment.txt"));
        }

        static void LoadEnvironmentValues(JObject environmentJson)
        {
            APIKey = environmentJson["APIKey"].ToString();
            APIToken = environmentJson["APIToken"].ToString();
        }
    }
}
