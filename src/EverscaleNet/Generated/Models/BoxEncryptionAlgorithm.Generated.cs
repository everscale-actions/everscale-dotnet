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
    public abstract class BoxEncryptionAlgorithm
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("ChaCha20")]
        public ChaCha20ParamsCB ChaCha20 { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("NaclBox")]
        public NaclBoxParamsCB NaclBox { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("NaclSecretBox")]
        public NaclSecretBoxParamsCB NaclSecretBox { get; set; }
    }
}