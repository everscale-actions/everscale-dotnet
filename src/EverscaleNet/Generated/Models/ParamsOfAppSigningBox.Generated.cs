using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Signing box callbacks.</para>
    /// </summary>
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(GetPublicKey), nameof(GetPublicKey))]
    [JsonDerivedType(typeof(Sign), nameof(Sign))]
    public abstract class ParamsOfAppSigningBox
    {
        /// <summary>
        /// <para>Get signing box public key</para>
        /// </summary>
        public class GetPublicKey : ParamsOfAppSigningBox
        {
        }

        /// <summary>
        /// <para>Sign data</para>
        /// </summary>
        public class Sign : ParamsOfAppSigningBox
        {
            /// <summary>
            /// <para>Data to sign encoded as base64</para>
            /// </summary>
            [JsonPropertyName("unsigned")]
            public string Unsigned { get; set; }
        }
    }
}