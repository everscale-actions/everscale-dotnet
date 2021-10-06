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
    public class ParamsOfUpdateInitialData
    {
        /// <summary>
        /// Contract ABI
        /// </summary>
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }

        /// <summary>
        /// Data BOC or BOC handle
        /// </summary>
        [JsonPropertyName("data")]
        public string Data { get; set; }

        /// <summary>
        /// <para>List of initial values for contract's static variables.</para>
        /// <para>`abi` parameter should be provided to set initial data</para>
        /// </summary>
        [JsonPropertyName("initial_data")]
        public JsonElement? InitialData { get; set; }

        /// <summary>
        /// Initial account owner's public key to set into account data
        /// </summary>
        [JsonPropertyName("initial_pubkey")]
        public string InitialPubkey { get; set; }

        /// <summary>
        /// Cache type to put the result. The BOC itself returned if no cache type provided.
        /// </summary>
        [JsonPropertyName("boc_cache")]
        public BocCacheType BocCache { get; set; }
    }
}