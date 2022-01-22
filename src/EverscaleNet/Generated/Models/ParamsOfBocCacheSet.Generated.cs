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
    public class ParamsOfBocCacheSet
    {
        /// <summary>
        /// BOC encoded as base64 or BOC reference
        /// </summary>
        [JsonPropertyName("boc")]
        public string Boc { get; set; }

        /// <summary>
        /// Cache type
        /// </summary>
        [JsonPropertyName("cache_type")]
        public BocCacheType CacheType { get; set; }
    }
}