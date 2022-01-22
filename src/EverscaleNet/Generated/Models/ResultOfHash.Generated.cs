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
    public class ResultOfHash
    {
        /// <summary>
        /// <para>Hash of input `data`.</para>
        /// <para>Encoded with 'hex'.</para>
        /// </summary>
        [JsonPropertyName("hash")]
        public string Hash { get; set; }
    }
}