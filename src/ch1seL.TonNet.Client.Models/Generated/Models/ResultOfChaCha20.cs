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
    public class ResultOfChaCha20
    {
        /// <summary>
        /// Encoded with `base64`.
        /// </summary>
        [JsonPropertyName("data")]
        public string Data { get; set; }
    }
}