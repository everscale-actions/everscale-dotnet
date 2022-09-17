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
        [JsonDiscriminator("ChaCha20")]
        public class ChaCha20 : BoxEncryptionAlgorithm
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("value")]
            public ChaCha20ParamsCB Value { get; set; }
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("NaclBox")]
        public class NaclBox : BoxEncryptionAlgorithm
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("value")]
            public NaclBoxParamsCB Value { get; set; }
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("NaclSecretBox")]
        public class NaclSecretBox : BoxEncryptionAlgorithm
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("value")]
            public NaclSecretBoxParamsCB Value { get; set; }
        }
    }
}