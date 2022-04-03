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
    public class ParamsOfCreateCryptoBox
    {
        /// <summary>
        /// Salt used for secret encryption. For example, a mobile device can use device ID as salt.
        /// </summary>
        [JsonPropertyName("secret_encryption_salt")]
        public string SecretEncryptionSalt { get; set; }

        /// <summary>
        /// Cryptobox secret
        /// </summary>
        [JsonPropertyName("secret")]
        public CryptoBoxSecret Secret { get; set; }
    }
}