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
    public class ParamsOfAbiEncodeBoc
    {
        /// <summary>
        /// Parameters to encode into BOC
        /// </summary>
        [JsonPropertyName("params")]
        public AbiParam[] @params { get; set; }

        /// <summary>
        /// Parameters and values as a JSON structure
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