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
    [JsonDerivedType(typeof(ChaCha20), nameof(ChaCha20))]
    [JsonDerivedType(typeof(NaclBox), nameof(NaclBox))]
    [JsonDerivedType(typeof(NaclSecretBox), nameof(NaclSecretBox))]
#endif
    public abstract class BoxEncryptionAlgorithm
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
#if !NET7_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("ChaCha20")]
#endif
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
#if !NET7_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("NaclBox")]
#endif
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
#if !NET7_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("NaclSecretBox")]
#endif
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