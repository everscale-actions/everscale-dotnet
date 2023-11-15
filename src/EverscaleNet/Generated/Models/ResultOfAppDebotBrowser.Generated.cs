using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>[UNSTABLE](UNSTABLE.md) [DEPRECATED](DEPRECATED.md) Returning values from Debot Browser callbacks.</para>
    /// </summary>
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(Input), nameof(Input))]
    [JsonDerivedType(typeof(GetSigningBox), nameof(GetSigningBox))]
    [JsonDerivedType(typeof(InvokeDebot), nameof(InvokeDebot))]
    [JsonDerivedType(typeof(Approve), nameof(Approve))]
    public abstract class ResultOfAppDebotBrowser
    {
        /// <summary>
        /// <para>Result of user input.</para>
        /// </summary>
        public class Input : ResultOfAppDebotBrowser
        {
            /// <summary>
            /// <para>String entered by user.</para>
            /// </summary>
            [JsonPropertyName("value")]
            public string Value { get; set; }
        }

        /// <summary>
        /// <para>Result of getting signing box.</para>
        /// </summary>
        public class GetSigningBox : ResultOfAppDebotBrowser
        {
            /// <summary>
            /// <para>Signing box for signing data requested by debot engine.</para>
            /// </summary>
            [JsonPropertyName("signing_box")]
            public uint SigningBox { get; set; }
        }

        /// <summary>
        /// <para>Result of debot invoking.</para>
        /// </summary>
        public class InvokeDebot : ResultOfAppDebotBrowser
        {
        }

        /// <summary>
        /// <para>Result of `approve` callback.</para>
        /// </summary>
        public class Approve : ResultOfAppDebotBrowser
        {
            /// <summary>
            /// <para>Indicates whether the DeBot is allowed to perform the specified operation.</para>
            /// </summary>
            [JsonPropertyName("approved")]
            public bool Approved { get; set; }
        }
    }
}