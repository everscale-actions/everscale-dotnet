using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfGetEncryptionBoxFromCryptoBox
    {
        /// <summary>
        /// <para>Crypto Box Handle.</para>
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
        /// <para>Encryption algorithm.</para>
        /// </summary>
        [JsonPropertyName("algorithm")]
        public BoxEncryptionAlgorithm Algorithm { get; set; }

        /// <summary>
        /// <para>Store derived secret for encryption algorithm for this lifetime (in ms). The timer starts after each encryption box operation. Secrets will be deleted (overwritten with zeroes) after each encryption operation, if this value is not set.</para>
        /// </summary>
        [JsonPropertyName("secret_lifetime")]
        public uint? SecretLifetime { get; set; }
    }
}