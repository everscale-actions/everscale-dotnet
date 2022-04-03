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
    public class ParamsOfGetSigningBoxFromCryptoBox
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
        /// Store derived secret for this lifetime (in ms). The timer starts after each signing box operation. Secrets will be deleted immediately after each signing box operation, if this value is not set.
        /// </summary>
        [JsonPropertyName("secret_lifetime")]
        public uint? SecretLifetime { get; set; }
    }
}