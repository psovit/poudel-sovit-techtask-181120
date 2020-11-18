using System.IO;
using System.Text.Json;

namespace recipes_api.Utils
{
    public static class JsonFileReader
    {
        public static T Deserialize<T>(string filePath)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            var jsonString = File.ReadAllText(filePath);
            var ls = JsonSerializer.Deserialize<T>(jsonString, options);
            return ls;
        }
    }
}