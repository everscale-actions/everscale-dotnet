using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class DecodedOutput
    {
        [JsonPropertyName("out_messages"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JsonElement[] OutMessages { get; set; }
        [JsonPropertyName("output")]
        public JsonElement Output { get; set; }
    }
}