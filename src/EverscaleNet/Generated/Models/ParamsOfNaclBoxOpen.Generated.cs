using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
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
        /// <para>Nonce</para>
        /// </summary>
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }

        /// <summary>
        /// <para>Sender's public key - unprefixed 0-padded to 64 symbols hex string</para>
        /// </summary>
        [JsonPropertyName("their_public")]
        public string TheirPublic { get; set; }

        /// <summary>
        /// <para>Receiver's private key - unprefixed 0-padded to 64 symbols hex string</para>
        /// </summary>
        [JsonPropertyName("secret")]
        public string Secret { get; set; }
    }
}