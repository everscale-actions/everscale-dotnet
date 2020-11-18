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
    public class ParamsOfNaclSecretBox
    {
        /// <summary>
        ///  Data that must be encrypted. Encoded with `base64`.
        /// </summary>
        [JsonPropertyName("decrypted")]
        public string Decrypted { get; set; }

        /// <summary>
        ///  Nonce in `hex`
        /// </summary>
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }

        /// <summary>
        ///  Secret key - unprefixed 0-padded to 64 symbols hex string 
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }
    }
}