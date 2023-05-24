using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class AbiConfig
    {
        /// <summary>
        /// <para>Workchain id that is used by default in DeploySet</para>
        /// </summary>
        [JsonPropertyName("workchain")]
        public int? Workchain { get; set; }

        /// <summary>
        /// <para>Message lifetime for contracts which ABI includes "expire" header.</para>
        /// <para>Must be specified in milliseconds. Default is 40000 (40 sec).</para>
        /// </summary>
        [JsonPropertyName("message_expiration_timeout")]
        public uint? MessageExpirationTimeout { get; set; }

        /// <summary>
        /// <para>Factor that increases the expiration timeout for each retry</para>
        /// <para>Default is 1.5</para>
        /// </summary>
        [JsonPropertyName("message_expiration_timeout_grow_factor")]
        public float? MessageExpirationTimeoutGrowFactor { get; set; }
    }
}