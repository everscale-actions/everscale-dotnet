using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(ChaCha20), nameof(ChaCha20))]
    [JsonDerivedType(typeof(NaclBox), nameof(NaclBox))]
    [JsonDerivedType(typeof(NaclSecretBox), nameof(NaclSecretBox))]
    public abstract class BoxEncryptionAlgorithm
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        public class ChaCha20 : BoxEncryptionAlgorithm
        {
            /// <summary>
            /// <para>Not described yet..</para>
            /// </summary>
            [JsonPropertyName("value")]
            public ChaCha20ParamsCB Value { get; set; }
        }

        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        public class NaclBox : BoxEncryptionAlgorithm
        {
            /// <summary>
            /// <para>Not described yet..</para>
            /// </summary>
            [JsonPropertyName("value")]
            public NaclBoxParamsCB Value { get; set; }
        }

        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        public class NaclSecretBox : BoxEncryptionAlgorithm
        {
            /// <summary>
            /// <para>Not described yet..</para>
            /// </summary>
            [JsonPropertyName("value")]
            public NaclSecretBoxParamsCB Value { get; set; }
        }
    }
}