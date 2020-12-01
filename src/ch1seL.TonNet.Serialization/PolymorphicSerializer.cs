using System;
using System.Linq;
using System.Text.Json;

namespace ch1seL.TonNet.Serialization
{
    public static class PolymorphicSerializer
    {
        public static TEvent Deserialize<TEvent>(string json)
        {
            return Deserialize<TEvent>(JsonDocument.Parse(json).RootElement);
        }

        public static TEvent Deserialize<TEvent>(JsonElement jsonElement)
        {
            if (typeof(TEvent) == typeof(JsonElement)) return (TEvent) (object) jsonElement;

            var nestedTypes = typeof(TEvent).GetNestedTypes();

            if (nestedTypes.Length == 0) return jsonElement.ToObject<TEvent>();

            var nestedTypeName = jsonElement.GetProperty("type").GetString();
            Type type = nestedTypes.FirstOrDefault(t => t.Name == nestedTypeName);
            return type == null
                ? default
                : jsonElement.ToObject<TEvent>(type);
        }
    }
}