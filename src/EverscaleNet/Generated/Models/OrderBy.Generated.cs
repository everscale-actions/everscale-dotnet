using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class OrderBy
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        [JsonPropertyName("path")]
        public string Path { get; set; }

        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        [JsonPropertyName("direction")]
        public SortDirection Direction { get; set; }
    }
}