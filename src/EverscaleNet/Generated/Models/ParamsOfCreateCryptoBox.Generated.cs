using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfCreateCryptoBox
    {
        /// <summary>
        /// <para>Salt used for secret encryption. For example, a mobile device can use device ID as salt.</para>
        /// </summary>
        [JsonPropertyName("secret_encryption_salt")]
        public string SecretEncryptionSalt { get; set; }

        /// <summary>
        /// <para>Cryptobox secret</para>
        /// </summary>
        [JsonPropertyName("secret")]
        public CryptoBoxSecret Secret { get; set; }
    }
}