using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Signing box callbacks.</para>
    /// </summary>
#if NET6_0_OR_GREATER
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(GetPublicKey), nameof(GetPublicKey))]
    [JsonDerivedType(typeof(Sign), nameof(Sign))]
#endif
    public abstract class ParamsOfAppSigningBox
    {
        /// <summary>
        /// <para>Get signing box public key</para>
        /// </summary>
#if !NET6_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("GetPublicKey")]
#endif
        public class GetPublicKey : ParamsOfAppSigningBox
        {
        }

        /// <summary>
        /// <para>Sign data</para>
        /// </summary>
#if !NET6_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("Sign")]
#endif
        public class Sign : ParamsOfAppSigningBox
        {
            /// <summary>
            /// <para>Sign data</para>
            /// </summary>
            [JsonPropertyName("unsigned")]
            public string Unsigned { get; set; }
        }
    }
}