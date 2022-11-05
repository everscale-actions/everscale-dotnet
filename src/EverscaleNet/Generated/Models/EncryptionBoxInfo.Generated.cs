using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Encryption box information.</para>
    /// </summary>
    public class EncryptionBoxInfo
    {
        /// <summary>
        /// <para>Derivation path, for instance "m/44'/396'/0'/0/0"</para>
        /// </summary>
        [JsonPropertyName("hdpath")]
        public string Hdpath { get; set; }

        /// <summary>
        /// <para>Cryptographic algorithm, used by this encryption box</para>
        /// </summary>
        [JsonPropertyName("algorithm")]
        public string Algorithm { get; set; }

        /// <summary>
        /// <para>Options, depends on algorithm and specific encryption box implementation</para>
        /// </summary>
        [JsonPropertyName("options")]
        public JsonElement? Options { get; set; }

        /// <summary>
        /// <para>Public information, depends on algorithm</para>
        /// </summary>
        [JsonPropertyName("public")]
        public JsonElement? Public { get; set; }
    }
}