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
        [JsonDiscriminator("AES")]
        public class AES : EncryptionAlgorithm
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("value")]
            public AesParamsEB Value { get; set; }
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("ChaCha20")]
        public class ChaCha20 : EncryptionAlgorithm
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("value")]
            public ChaCha20ParamsEB Value { get; set; }
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("NaclBox")]
        public class NaclBox : EncryptionAlgorithm
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("value")]
            public NaclBoxParamsEB Value { get; set; }
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("NaclSecretBox")]
        public class NaclSecretBox : EncryptionAlgorithm
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("value")]
            public NaclSecretBoxParamsEB Value { get; set; }
        }
    }
}