using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class ChaCha20Request
    {
        /// <summary>
        ///  Source data to be encrypted or decrypted. Must be encoded with `base64`.
        /// </summary>
        [JsonPropertyName("data")]
        public string Data { get; set; }

        /// <summary>
        ///  256-bit key. Must be encoded with `hex`.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }

        /// <summary>
        ///  96-bit nonce. Must be encoded with `hex`.
        /// </summary>
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }
    }
}