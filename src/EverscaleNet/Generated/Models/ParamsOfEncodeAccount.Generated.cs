using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfEncodeAccount
    {
        /// <summary>
        /// <para>Source of the account state init.</para>
        /// </summary>
        [JsonPropertyName("state_init")]
        public StateInitSource StateInit { get; set; }

        /// <summary>
        /// <para>Initial balance.</para>
        /// </summary>
        [JsonPropertyName("balance")]
        public BigInteger? Balance { get; set; }

        /// <summary>
        /// <para>Initial value for the `last_trans_lt`.</para>
        /// </summary>
        [JsonPropertyName("last_trans_lt")]
        public BigInteger? LastTransLt { get; set; }

        /// <summary>
        /// <para>Initial value for the `last_paid`.</para>
        /// </summary>
        [JsonPropertyName("last_paid")]
        public uint? LastPaid { get; set; }

        /// <summary>
        /// <para>Cache type to put the result.</para>
        /// <para>The BOC itself returned if no cache type provided</para>
        /// </summary>
        [JsonPropertyName("boc_cache")]
        public BocCacheType BocCache { get; set; }
    }
}