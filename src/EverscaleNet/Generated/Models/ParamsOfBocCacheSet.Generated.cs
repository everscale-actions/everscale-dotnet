using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfBocCacheSet
    {
        /// <summary>
        /// <para>BOC encoded as base64 or BOC reference</para>
        /// </summary>
        [JsonPropertyName("boc")]
        public string Boc { get; set; }

        /// <summary>
        /// <para>Cache type</para>
        /// </summary>
        [JsonPropertyName("cache_type")]
        public BocCacheType CacheType { get; set; }
    }
}