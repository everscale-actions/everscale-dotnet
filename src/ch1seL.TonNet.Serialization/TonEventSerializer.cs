using System;
using System.Linq;
using System.Text.Json;

namespace ch1seL.TonNet.Serialization
{
    public static class TonEventSerializer
    {
        public static TEvent Deserialize<TEvent>(string json)
        {
            var nestedTypes = typeof(TEvent).GetNestedTypes();

            JsonElement jsonElement = JsonDocument.Parse(json).RootElement;
            var nestedTypeName = jsonElement.GetProperty("type").GetString();

            Type type = nestedTypes.FirstOrDefault(t => t.Name == nestedTypeName);

            return type == null
                ? default
                : (TEvent) JsonSerializer.Deserialize(json, type);
        }
    }
}