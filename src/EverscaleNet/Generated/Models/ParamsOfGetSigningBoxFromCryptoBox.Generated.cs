using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ParamsOfGetSigningBoxFromCryptoBox
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
        /// <para>Store derived secret for this lifetime (in ms). The timer starts after each signing box operation. Secrets will be deleted immediately after each signing box operation, if this value is not set.</para>
        /// </summary>
        [JsonPropertyName("secret_lifetime")]
        public uint? SecretLifetime { get; set; }
    }
}