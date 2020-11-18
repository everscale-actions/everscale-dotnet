using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class AbiConfig
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("workchain"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Workchain { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("message_expiration_timeout"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public uint? MessageExpirationTimeout { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("message_expiration_timeout_grow_factor"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public float? MessageExpirationTimeoutGrowFactor { get; set; }
    }
}