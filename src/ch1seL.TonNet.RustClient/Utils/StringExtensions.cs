using System.Text.Json;

namespace ch1seL.TonNet.RustClient.Utils
{
    public static class StringExtensions
    {
        public static JsonElement GetParsedProperty(this string element, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(element)) return default;
            JsonDocument document = JsonDocument.Parse(element);
            return document.RootElement.TryGetProperty(propertyName, out JsonElement property) ? property : default;
        }
    }
}