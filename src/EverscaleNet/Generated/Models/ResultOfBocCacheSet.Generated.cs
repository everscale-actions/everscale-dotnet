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
    public class ResultOfBocCacheSet
    {
        /// <summary>
        /// Reference to the cached BOC
        /// </summary>
        [JsonPropertyName("boc_ref")]
        public string BocRef { get; set; }
    }
}