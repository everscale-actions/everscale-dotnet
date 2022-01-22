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
    public class ParamsOfEncodeTvc
    {
        /// <summary>
        /// Contract code BOC encoded as base64 or BOC handle
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }

        /// <summary>
        /// Contract data BOC encoded as base64 or BOC handle
        /// </summary>
        [JsonPropertyName("data")]
        public string Data { get; set; }

        /// <summary>
        /// Contract library BOC encoded as base64 or BOC handle
        /// </summary>
        [JsonPropertyName("library")]
        public string Library { get; set; }

        /// <summary>
        /// <para>`special.tick` field.</para>
        /// <para>Specifies the contract ability to handle tick transactions</para>
        /// </summary>
        [JsonPropertyName("tick")]
        public bool? Tick { get; set; }

        /// <summary>
        /// <para>`special.tock` field.</para>
        /// <para>Specifies the contract ability to handle tock transactions</para>
        /// </summary>
        [JsonPropertyName("tock")]
        public bool? Tock { get; set; }

        /// <summary>
        /// Is present and non-zero only in instances of large smart contracts
        /// </summary>
        [JsonPropertyName("split_depth")]
        public uint? SplitDepth { get; set; }

        /// <summary>
        /// Cache type to put the result. The BOC itself returned if no cache type provided.
        /// </summary>
        [JsonPropertyName("boc_cache")]
        public BocCacheType BocCache { get; set; }
    }
}