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
    public class ParamsOfNaclSecretBoxOpen
    {
        /// <summary>
        /// <para>Data that must be decrypted.</para>
        /// <para>Encoded with `base64`.</para>
        /// </summary>
        [JsonPropertyName("encrypted")]
        public string Encrypted { get; set; }

        /// <summary>
        /// Nonce in `hex`
        /// </summary>
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }

        /// <summary>
        /// Public key - unprefixed 0-padded to 64 symbols hex string
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }
}