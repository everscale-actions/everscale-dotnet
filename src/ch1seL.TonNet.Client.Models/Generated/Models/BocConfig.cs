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