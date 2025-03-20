using System.Text.Json;

namespace Ambev.DeveloperEvaluation.Common.Json
{
    public static class JsonExtension
    {
        private static readonly JsonSerializerOptions _options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        public static string Serialize<T>(this T data)
        {
            return JsonSerializer.Serialize(data, _options);
        }

        public static T? Deserialize<T>(this string json)
        {
            return string.IsNullOrEmpty(json) ? default : JsonSerializer.Deserialize<T>(json, _options);           
        }
    }
}
