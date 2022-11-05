using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfNaclBox
    {
        /// <summary>
        /// <para>Data that must be encrypted encoded in `base64`.</para>
        /// </summary>
        [JsonPropertyName("decrypted")]
        public string Decrypted { get; set; }

        /// <summary>
        /// <para>Nonce, encoded in `hex`</para>
        /// </summary>
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }

        /// <summary>
        /// <para>Receiver's public key - unprefixed 0-padded to 64 symbols hex string</para>
        /// </summary>
        [JsonPropertyName("their_public")]
        public string TheirPublic { get; set; }

        /// <summary>
        /// <para>Sender's private key - unprefixed 0-padded to 64 symbols hex string</para>
        /// </summary>
        [JsonPropertyName("secret")]
        public string Secret { get; set; }
    }
}