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
    public class OrderBy
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("path")]
        public string Path { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("direction")]
        public SortDirection Direction { get; set; }
    }
}