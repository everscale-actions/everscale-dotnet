using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class AbiEvent
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("inputs")]
        public JsonElement[] Inputs { get; set; }
        [JsonPropertyName("id"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public uint? Id { get; set; }
    }
}