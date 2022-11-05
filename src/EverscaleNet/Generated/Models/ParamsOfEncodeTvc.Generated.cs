using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfEncodeTvc
    {
        /// <summary>
        /// <para>Contract code BOC encoded as base64 or BOC handle</para>
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }

        /// <summary>
        /// <para>Contract data BOC encoded as base64 or BOC handle</para>
        /// </summary>
        [JsonPropertyName("data")]
        public string Data { get; set; }

        /// <summary>
        /// <para>Contract library BOC encoded as base64 or BOC handle</para>
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
        /// <para>Is present and non-zero only in instances of large smart contracts</para>
        /// </summary>
        [JsonPropertyName("split_depth")]
        public uint? SplitDepth { get; set; }

        /// <summary>
        /// <para>Cache type to put the result. The BOC itself returned if no cache type provided.</para>
        /// </summary>
        [JsonPropertyName("boc_cache")]
        public BocCacheType BocCache { get; set; }
    }
}