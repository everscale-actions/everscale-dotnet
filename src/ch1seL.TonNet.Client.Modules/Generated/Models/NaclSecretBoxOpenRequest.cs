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
    public class NaclSecretBoxOpenRequest
    {
        /// <summary>
        ///  Data that must be decrypted. Encoded with `base64`.
        /// </summary>
        [JsonPropertyName("encrypted")]
        public string Encrypted { get; set; }

        /// <summary>
        ///  Nonce in `hex`
        /// </summary>
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }

        /// <summary>
        ///  Public key - unprefixed 0-padded to 64 symbols hex string 
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }
}