using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfGetBocDepth
    {
        /// <summary>
        /// <para>BOC root cell depth</para>
        /// </summary>
        [JsonPropertyName("depth")]
        public uint Depth { get; set; }
    }
}