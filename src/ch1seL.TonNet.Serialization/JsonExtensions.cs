using System;
using System.Buffers;
using System.Text.Json;

namespace ch1seL.TonNet.Serialization
{
    public static class JsonExtensions
    {
        public static T ToObject<T>(this JsonElement element)
        {
            var bufferWriter = new ArrayBufferWriter<byte>();
            using (var writer = new Utf8JsonWriter(bufferWriter))
            {
                element.WriteTo(writer);
            }

            return JsonSerializer.Deserialize<T>(bufferWriter.WrittenSpan, JsonOptionsProvider.JsonSerializerOptions);
        }

        public static JsonElement ToJsonElement(this object element)
        {
            return JsonDocument.Parse(JsonSerializer.Serialize(element)).RootElement;
        }

        public static T ToObject<T>(this JsonDocument document)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));
            return document.RootElement.ToObject<T>();
        }
    }
}