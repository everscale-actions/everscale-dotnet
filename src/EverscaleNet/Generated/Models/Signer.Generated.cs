using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
#if NET6_0_OR_GREATER
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(None), nameof(None))]
    [JsonDerivedType(typeof(External), nameof(External))]
    [JsonDerivedType(typeof(Keys), nameof(Keys))]
    [JsonDerivedType(typeof(SigningBox), nameof(SigningBox))]
#endif
    public abstract class Signer
    {
        /// <summary>
        /// <para>No keys are provided.</para>
        /// <para>Creates an unsigned message.</para>
        /// </summary>
#if !NET6_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("None")]
#endif
        public class None : Signer
        {
        }

        /// <summary>
        /// <para>Only public key is provided in unprefixed hex string format to generate unsigned message and `data_to_sign` which can be signed later.</para>
        /// </summary>
#if !NET6_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("External")]
#endif
        public class External : Signer
        {
            /// <summary>
            /// <para>Only public key is provided in unprefixed hex string format to generate unsigned message and `data_to_sign` which can be signed later.</para>
            /// </summary>
            [JsonPropertyName("public_key")]
            public string PublicKey { get; set; }
        }

        /// <summary>
        /// <para>Key pair is provided for signing</para>
        /// </summary>
#if !NET6_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("Keys")]
#endif
        public class Keys : Signer
        {
            /// <summary>
            /// <para>Key pair is provided for signing</para>
            /// </summary>
            [JsonPropertyName("keys")]
            public KeyPair KeysAccessor { get; set; }
        }

        /// <summary>
        /// <para>Signing Box interface is provided for signing, allows Dapps to sign messages using external APIs, such as HSM, cold wallet, etc.</para>
        /// </summary>
#if !NET6_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("SigningBox")]
#endif
        public class SigningBox : Signer
        {
            /// <summary>
            /// <para>Signing Box interface is provided for signing, allows Dapps to sign messages using external APIs, such as HSM, cold wallet, etc.</para>
            /// </summary>
            [JsonPropertyName("handle")]
            public uint Handle { get; set; }
        }
    }
}