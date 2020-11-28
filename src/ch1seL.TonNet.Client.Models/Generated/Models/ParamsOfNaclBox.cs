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
    public class ParamsOfNaclBox
    {
        /// <summary>
        ///  Data that must be encrypted encoded in `base64`.
        /// </summary>
        [JsonPropertyName("decrypted")]
        public string Decrypted { get; set; }

        /// <summary>
        ///  Nonce, encoded in `hex`
        /// </summary>
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }

        /// <summary>
        ///  Receiver's public key - unprefixed 0-padded to 64 symbols hex string
        /// </summary>
        [JsonPropertyName("their_public")]
        public string TheirPublic { get; set; }

        /// <summary>
        ///  Sender's private key - unprefixed 0-padded to 64 symbols hex string
        /// </summary>
        [JsonPropertyName("secret")]
        public string Secret { get; set; }
    }
}