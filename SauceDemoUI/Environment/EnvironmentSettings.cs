using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities;
using Newtonsoft.Json.Linq;

namespace SauceDemoUI.Environment
{
    internal static class EnvironmentSettings
    {
        internal static string ScreenshotFolderName = "";

        static EnvironmentSettings()
        {
            LoadEnvironmentValues(Utilities.ReadJsonFile("./../../../Environment/Environment.txt"));
        }

        static void LoadEnvironmentValues(JObject environmentJson)
        {
            ScreenshotFolderName = environmentJson["ScreenshotFolderName"].ToString();
        }
    }
}
