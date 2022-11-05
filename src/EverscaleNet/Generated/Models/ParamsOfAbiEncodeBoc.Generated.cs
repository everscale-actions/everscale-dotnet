using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfAbiEncodeBoc
    {
        /// <summary>
        /// <para>Parameters to encode into BOC</para>
        /// </summary>
        [JsonPropertyName("params")]
        public AbiParam[] @params { get; set; }

        /// <summary>
        /// <para>Parameters and values as a JSON structure</para>
        /// </summary>
        [JsonPropertyName("data")]
        public JsonElement? Data { get; set; }

        /// <summary>
        /// <para>Cache type to put the result.</para>
        /// <para>The BOC itself returned if no cache type provided</para>
        /// </summary>
        [JsonPropertyName("boc_cache")]
        public BocCacheType BocCache { get; set; }
    }
}