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
        ///  No keys are provided. Creates an unsigned message. 
        /// </summary>
        public class None : Signer
        {
        }

        /// <summary>
        /// <para> Only public key is provided in unprefixed hex string format to generate unsigned message </para>
        /// <para> and `data_to_sign` which can be signed later.  </para>
        /// </summary>
        public class External : Signer
        {
            /// <summary>
            /// <para> Only public key is provided in unprefixed hex string format to generate unsigned message </para>
            /// <para> and `data_to_sign` which can be signed later.  </para>
            /// </summary>
            [JsonPropertyName("public_key")]
            public string PublicKey { get; set; }
        }

        /// <summary>
        ///  Key pair is provided for signing
        /// </summary>
        public class Keys : Signer
        {
            /// <summary>
            ///  Key pair is provided for signing
            /// </summary>
            [JsonPropertyName("keys")]
            public KeyPair KeysAccessor { get; set; }
        }

        /// <summary>
        /// <para> Signing Box interface is provided for signing, allows Dapps to sign messages using external APIs,</para>
        /// <para> such as HSM, cold wallet, etc.</para>
        /// </summary>
        public class SigningBox : Signer
        {
            /// <summary>
            /// <para> Signing Box interface is provided for signing, allows Dapps to sign messages using external APIs,</para>
            /// <para> such as HSM, cold wallet, etc.</para>
            /// </summary>
            [JsonPropertyName("handle")]
            public uint Handle { get; set; }
        }
    }
}