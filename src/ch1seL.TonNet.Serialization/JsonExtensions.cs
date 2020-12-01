using System;
using System.Buffers;
using System.Text.Json;

namespace ch1seL.TonNet.Serialization
{
    public static class JsonExtensions
    {
        public static T Get<T>(this JsonElement? element, string property)
        {
            return element!.Value.GetProperty(property).ToObject<T>();
        }

        public static T Get<T>(this JsonElement element, string property)
        {
            return element.GetProperty(property).ToObject<T>();
        }

        public static T ToAnonymous<T>(this JsonElement element, T protorype)
        {
            var bufferWriter = new ArrayBufferWriter<byte>();
            using (var writer = new Utf8JsonWriter(bufferWriter))
            {
                element.WriteTo(writer);
            }

            return JsonSerializer.Deserialize<T>(bufferWriter.WrittenSpan, JsonOptionsProvider.JsonSerializerOptions);
        }

        public static T ToObject<T>(this JsonElement? element)
        {
            var bufferWriter = new ArrayBufferWriter<byte>();
            using (var writer = new Utf8JsonWriter(bufferWriter))
            {
                element!.Value.WriteTo(writer);
            }

            return JsonSerializer.Deserialize<T>(bufferWriter.WrittenSpan, JsonOptionsProvider.JsonSerializerOptions);
        }

        public static T ToObject<T>(this JsonElement element, Type discriminatorType = null)
        {
            var bufferWriter = new ArrayBufferWriter<byte>();
            using (var writer = new Utf8JsonWriter(bufferWriter))
            {
                element.WriteTo(writer);
            }

            return discriminatorType != null
                ? (T) JsonSerializer.Deserialize(bufferWriter.WrittenSpan, discriminatorType)
                : JsonSerializer.Deserialize<T>(bufferWriter.WrittenSpan, JsonOptionsProvider.JsonSerializerOptions);
        }

        public static JsonElement ToJsonElement(this object element)
        {
            return JsonDocument.Parse(JsonSerializer.Serialize(element, JsonOptionsProvider.JsonSerializerOptions)).RootElement;
        }

        public static T ToObject<T>(this JsonDocument document)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));
            return document.RootElement.ToObject<T>();
        }
    }
}