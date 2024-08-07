using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class BocConfig
    {
        /// <summary>
        /// <para>Maximum BOC cache size in kilobytes.</para>
        /// <para>Default is 10 MB</para>
        /// </summary>
        [JsonPropertyName("cache_max_size")]
        public uint? CacheMaxSize { get; set; }
    }
}