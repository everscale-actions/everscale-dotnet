using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Returning values from signing box callbacks.</para>
    /// </summary>
#if NET6_0_OR_GREATER
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(GetPublicKey), nameof(GetPublicKey))]
    [JsonDerivedType(typeof(Sign), nameof(Sign))]
#endif
    public abstract class ResultOfAppSigningBox
    {
        /// <summary>
        /// <para>Result of getting public key</para>
        /// </summary>
#if !NET6_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("GetPublicKey")]
#endif
        public class GetPublicKey : ResultOfAppSigningBox
        {
            /// <summary>
            /// <para>Signing box public key</para>
            /// </summary>
            [JsonPropertyName("public_key")]
            public string PublicKey { get; set; }
        }

        /// <summary>
        /// <para>Result of signing data</para>
        /// </summary>
#if !NET6_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("Sign")]
#endif
        public class Sign : ResultOfAppSigningBox
        {
            /// <summary>
            /// <para>Data signature encoded as hex</para>
            /// </summary>
            [JsonPropertyName("signature")]
            public string Signature { get; set; }
        }
    }
}