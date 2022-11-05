using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfDecodeTvc
    {
        /// <summary>
        /// <para>Contract TVC image BOC encoded as base64 or BOC handle</para>
        /// </summary>
        [JsonPropertyName("tvc")]
        public string Tvc { get; set; }

        /// <summary>
        /// <para>Cache type to put the result. The BOC itself returned if no cache type provided.</para>
        /// </summary>
        [JsonPropertyName("boc_cache")]
        public BocCacheType BocCache { get; set; }
    }
}