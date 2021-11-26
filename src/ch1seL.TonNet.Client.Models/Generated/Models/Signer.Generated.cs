using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public abstract class Signer
    {
        /// <summary>
        /// <para>No keys are provided.</para>
        /// <para>Creates an unsigned message.</para>
        /// </summary>
        [JsonDiscriminator("None")]
        public class None : Signer
        {
        }

        /// <summary>
        /// Only public key is provided in unprefixed hex string format to generate unsigned message and `data_to_sign` which can be signed later.
        /// </summary>
        [JsonDiscriminator("External")]
        public class External : Signer
        {
            /// <summary>
            /// Only public key is provided in unprefixed hex string format to generate unsigned message and `data_to_sign` which can be signed later.
            /// </summary>
            [JsonPropertyName("public_key")]
            public string PublicKey { get; set; }
        }

        /// <summary>
        /// Key pair is provided for signing
        /// </summary>
        [JsonDiscriminator("Keys")]
        public class Keys : Signer
        {
            /// <summary>
            /// Key pair is provided for signing
            /// </summary>
            [JsonPropertyName("keys")]
            public KeyPair KeysAccessor { get; set; }
        }

        /// <summary>
        /// Signing Box interface is provided for signing, allows Dapps to sign messages using external APIs, such as HSM, cold wallet, etc.
        /// </summary>
        [JsonDiscriminator("SigningBox")]
        public class SigningBox : Signer
        {
            /// <summary>
            /// Signing Box interface is provided for signing, allows Dapps to sign messages using external APIs, such as HSM, cold wallet, etc.
            /// </summary>
            [JsonPropertyName("handle")]
            public uint Handle { get; set; }
        }
    }
}