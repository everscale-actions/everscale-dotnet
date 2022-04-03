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
    public class ParamsOfGetEncryptionBoxFromCryptoBox
    {
        /// <summary>
        /// Crypto Box Handle.
        /// </summary>
        [JsonPropertyName("handle")]
        public uint Handle { get; set; }

        /// <summary>
        /// <para>HD key derivation path.</para>
        /// <para>By default, Everscale HD path is used.</para>
        /// </summary>
        [JsonPropertyName("hdpath")]
        public string Hdpath { get; set; }

        /// <summary>
        /// Encryption algorithm.
        /// </summary>
        [JsonPropertyName("algorithm")]
        public BoxEncryptionAlgorithm Algorithm { get; set; }

        /// <summary>
        /// Store derived secret for encryption algorithm for this lifetime (in ms). The timer starts after each encryption box operation. Secrets will be deleted (overwritten with zeroes) after each encryption operation, if this value is not set.
        /// </summary>
        [JsonPropertyName("secret_lifetime")]
        public uint? SecretLifetime { get; set; }
    }
}