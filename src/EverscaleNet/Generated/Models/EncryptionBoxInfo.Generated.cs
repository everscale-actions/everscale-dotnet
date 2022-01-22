using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// Encryption box information
    /// </summary>
    public class EncryptionBoxInfo
    {
        /// <summary>
        /// Derivation path, for instance "m/44'/396'/0'/0/0"
        /// </summary>
        [JsonPropertyName("hdpath")]
        public string Hdpath { get; set; }

        /// <summary>
        /// Cryptographic algorithm, used by this encryption box
        /// </summary>
        [JsonPropertyName("algorithm")]
        public string Algorithm { get; set; }

        /// <summary>
        /// Options, depends on algorithm and specific encryption box implementation
        /// </summary>
        [JsonPropertyName("options")]
        public JsonElement? Options { get; set; }

        /// <summary>
        /// Public information, depends on algorithm
        /// </summary>
        [JsonPropertyName("public")]
        public JsonElement? Public { get; set; }
    }
}