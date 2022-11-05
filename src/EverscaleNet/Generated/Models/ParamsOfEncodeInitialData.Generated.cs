using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfEncodeInitialData
    {
        /// <summary>
        /// <para>Contract ABI</para>
        /// </summary>
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }

        /// <summary>
        /// <para>List of initial values for contract's static variables.</para>
        /// <para>`abi` parameter should be provided to set initial data</para>
        /// </summary>
        [JsonPropertyName("initial_data")]
        public JsonElement? InitialData { get; set; }

        /// <summary>
        /// <para>Initial account owner's public key to set into account data</para>
        /// </summary>
        [JsonPropertyName("initial_pubkey")]
        public string InitialPubkey { get; set; }

        /// <summary>
        /// <para>Cache type to put the result. The BOC itself returned if no cache type provided.</para>
        /// </summary>
        [JsonPropertyName("boc_cache")]
        public BocCacheType BocCache { get; set; }
    }
}