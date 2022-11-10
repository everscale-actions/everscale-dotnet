using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>[UNSTABLE](UNSTABLE.md) Returning values from Debot Browser callbacks.</para>
    /// </summary>
#if NET6_0_OR_GREATER
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(Input), nameof(Input))]
    [JsonDerivedType(typeof(GetSigningBox), nameof(GetSigningBox))]
    [JsonDerivedType(typeof(InvokeDebot), nameof(InvokeDebot))]
    [JsonDerivedType(typeof(Approve), nameof(Approve))]
#endif
    public abstract class ResultOfAppDebotBrowser
    {
        /// <summary>
        /// <para>Result of user input.</para>
        /// </summary>
#if !NET6_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("Input")]
#endif
        public class Input : ResultOfAppDebotBrowser
        {
            /// <summary>
            /// <para>Result of user input.</para>
            /// </summary>
            [JsonPropertyName("value")]
            public string Value { get; set; }
        }

        /// <summary>
        /// <para>Result of getting signing box.</para>
        /// </summary>
#if !NET6_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("GetSigningBox")]
#endif
        public class GetSigningBox : ResultOfAppDebotBrowser
        {
            /// <summary>
            /// <para>Result of getting signing box.</para>
            /// </summary>
            [JsonPropertyName("signing_box")]
            public uint SigningBox { get; set; }
        }

        /// <summary>
        /// <para>Result of debot invoking.</para>
        /// </summary>
#if !NET6_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("InvokeDebot")]
#endif
        public class InvokeDebot : ResultOfAppDebotBrowser
        {
        }

        /// <summary>
        /// <para>Result of `approve` callback.</para>
        /// </summary>
#if !NET6_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("Approve")]
#endif
        public class Approve : ResultOfAppDebotBrowser
        {
            /// <summary>
            /// <para>Result of `approve` callback.</para>
            /// </summary>
            [JsonPropertyName("approved")]
            public bool Approved { get; set; }
        }
    }
}