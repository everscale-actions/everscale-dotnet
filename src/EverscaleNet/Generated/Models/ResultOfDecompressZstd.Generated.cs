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
    public class ResultOfDecompressZstd
    {
        /// <summary>
        /// <para>Decompressed data.</para>
        /// <para>Must be encoded as base64.</para>
        /// </summary>
        [JsonPropertyName("decompressed")]
        public string Decompressed { get; set; }
    }
}