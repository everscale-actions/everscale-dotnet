using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfVersion
    {
        /// <summary>
        /// <para>Core Library version</para>
        /// </summary>
        [JsonPropertyName("version")]
        public string Version { get; set; }
    }
}