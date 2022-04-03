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
    public abstract class EncryptionAlgorithm
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("AES")]
        public AesParamsEB AES { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("ChaCha20")]
        public ChaCha20ParamsEB ChaCha20 { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("NaclBox")]
        public NaclBoxParamsEB NaclBox { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("NaclSecretBox")]
        public NaclSecretBoxParamsEB NaclSecretBox { get; set; }
    }
}