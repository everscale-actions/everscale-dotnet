using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class AbiConfig
    {
        /// <summary>
        /// Workchain id that is used by default in DeploySet
        /// </summary>
        [JsonPropertyName("workchain")]
        public int? Workchain { get; set; }

        /// <summary>
        /// Message lifetime for contracts which ABI includes "expire" header. The default value is 40 sec.
        /// </summary>
        [JsonPropertyName("message_expiration_timeout")]
        public uint? MessageExpirationTimeout { get; set; }

        /// <summary>
        /// Factor that increases the expiration timeout for each retry The default value is 1.5
        /// </summary>
        [JsonPropertyName("message_expiration_timeout_grow_factor")]
        public float? MessageExpirationTimeoutGrowFactor { get; set; }
    }
}