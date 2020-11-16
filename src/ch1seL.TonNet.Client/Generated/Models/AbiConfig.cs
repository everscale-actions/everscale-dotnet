using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class AbiConfig
    {
        [JsonPropertyName("workchain"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Workchain { get; set; }
        [JsonPropertyName("message_expiration_timeout"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public uint? MessageExpirationTimeout { get; set; }
        [JsonPropertyName("message_expiration_timeout_grow_factor"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public float? MessageExpirationTimeoutGrowFactor { get; set; }
    }
}