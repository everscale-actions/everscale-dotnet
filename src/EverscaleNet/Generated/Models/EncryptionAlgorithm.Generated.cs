using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
#if NET7_0_OR_GREATER
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(AES), nameof(AES))]
    [JsonDerivedType(typeof(ChaCha20), nameof(ChaCha20))]
    [JsonDerivedType(typeof(NaclBox), nameof(NaclBox))]
    [JsonDerivedType(typeof(NaclSecretBox), nameof(NaclSecretBox))]
#endif
    public abstract class EncryptionAlgorithm
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
#if !NET7_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("AES")]
#endif
        public class AES : EncryptionAlgorithm
        {
            /// <summary>
            /// <para>Not described yet..</para>
            /// </summary>
            [JsonPropertyName("value")]
            public AesParamsEB Value { get; set; }
        }

        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
#if !NET7_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("ChaCha20")]
#endif
        public class ChaCha20 : EncryptionAlgorithm
        {
            /// <summary>
            /// <para>Not described yet..</para>
            /// </summary>
            [JsonPropertyName("value")]
            public ChaCha20ParamsEB Value { get; set; }
        }

        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
#if !NET7_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("NaclBox")]
#endif
        public class NaclBox : EncryptionAlgorithm
        {
            /// <summary>
            /// <para>Not described yet..</para>
            /// </summary>
            [JsonPropertyName("value")]
            public NaclBoxParamsEB Value { get; set; }
        }

        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
#if !NET7_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("NaclSecretBox")]
#endif
        public class NaclSecretBox : EncryptionAlgorithm
        {
            /// <summary>
            /// <para>Not described yet..</para>
            /// </summary>
            [JsonPropertyName("value")]
            public NaclSecretBoxParamsEB Value { get; set; }
        }
    }
}