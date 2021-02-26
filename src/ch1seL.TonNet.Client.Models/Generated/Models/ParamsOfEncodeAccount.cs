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
    public class ParamsOfEncodeAccount
    {
        /// <summary>
        /// Source of the account state init.
        /// </summary>
        [JsonPropertyName("state_init")]
        public StateInitSource StateInit { get; set; }

        /// <summary>
        /// Initial balance.
        /// </summary>
        [JsonPropertyName("balance")]
        public ulong? Balance { get; set; }

        /// <summary>
        /// Initial value for the `last_trans_lt`.
        /// </summary>
        [JsonPropertyName("last_trans_lt")]
        public ulong? LastTransLt { get; set; }

        /// <summary>
        /// Initial value for the `last_paid`.
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