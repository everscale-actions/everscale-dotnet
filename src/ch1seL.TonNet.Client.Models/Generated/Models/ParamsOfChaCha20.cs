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
    public class ParamsOfChaCha20
    {
        /// <summary>
        /// Must be encoded with `base64`.
        /// </summary>
        [JsonPropertyName("data")]
        public string Data { get; set; }

        /// <summary>
        /// Must be encoded with `hex`.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }

        /// <summary>
        /// Must be encoded with `hex`.
        /// </summary>
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }
    }
}