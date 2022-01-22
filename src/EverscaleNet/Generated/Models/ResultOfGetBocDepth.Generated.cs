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
    public class ResultOfGetBocDepth
    {
        /// <summary>
        /// BOC root cell depth
        /// </summary>
        [JsonPropertyName("depth")]
        public uint Depth { get; set; }
    }
}