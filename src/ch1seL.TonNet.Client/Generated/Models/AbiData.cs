using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class AbiData
    {
        [JsonPropertyName("key")]
        public BigInteger Key { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("components"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JsonElement[] Components { get; set; }
    }
}