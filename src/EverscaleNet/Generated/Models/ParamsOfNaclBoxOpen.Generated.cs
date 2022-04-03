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
    public class ParamsOfNaclBoxOpen
    {
        /// <summary>
        /// <para>Data that must be decrypted.</para>
        /// <para>Encoded with `base64`.</para>
        /// </summary>
        [JsonPropertyName("encrypted")]
        public string Encrypted { get; set; }

        /// <summary>
        /// Nonce
        /// </summary>
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }

        /// <summary>
        /// Sender's public key - unprefixed 0-padded to 64 symbols hex string
        /// </summary>
        [JsonPropertyName("their_public")]
        public string TheirPublic { get; set; }

        /// <summary>
        /// Receiver's private key - unprefixed 0-padded to 64 symbols hex string
        /// </summary>
        [JsonPropertyName("secret")]
        public string Secret { get; set; }
    }
}