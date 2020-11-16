using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class DecodedMessageBody
    {
        [JsonPropertyName("body_type")]
        public MessageBodyType BodyType { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("value")]
        public JsonElement Value { get; set; }
        [JsonPropertyName("header")]
        public FunctionHeader Header { get; set; }
    }
}