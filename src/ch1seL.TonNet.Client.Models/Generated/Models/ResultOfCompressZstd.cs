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
    public class ResultOfCompressZstd
    {
        /// <summary>
        /// <para>Compressed data.</para>
        /// <para>Must be encoded as base64.</para>
        /// </summary>
        [JsonPropertyName("compressed")]
        public string Compressed { get; set; }
    }
}