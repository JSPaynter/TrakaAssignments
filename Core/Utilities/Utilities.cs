using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities
{
    public static class Utilities
    {
        public static JObject ReadJsonFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException($"'{nameof(filePath)}' cannot be null or whitespace.", nameof(filePath));
            try
            {
                return JObject.Parse(ReadFile(filePath));
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to convert file {filePath} to JOBject\n{ex}");
            }
        }

        public static string ReadFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException($"'{nameof(filePath)}' cannot be null or whitespace.", nameof(filePath));
            try
            {
                return File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to read from file {filePath}\n{ex}");
            }
        }
    }
}
