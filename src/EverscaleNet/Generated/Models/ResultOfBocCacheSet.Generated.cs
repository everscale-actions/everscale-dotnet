using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfBocCacheSet
    {
        /// <summary>
        /// <para>Reference to the cached BOC</para>
        /// </summary>
        [JsonPropertyName("boc_ref")]
        public string BocRef { get; set; }
    }
}