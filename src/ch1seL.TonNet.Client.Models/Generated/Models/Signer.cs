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
        /// Creates an unsigned message.
        /// </summary>
        [JsonDiscriminator("None")]
        public class None : Signer
        {
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("External")]
        public class External : Signer
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("public_key")]
            public string PublicKey { get; set; }
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("Keys")]
        public class Keys : Signer
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("keys")]
            public KeyPair KeysAccessor { get; set; }
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("SigningBox")]
        public class SigningBox : Signer
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("handle")]
            public uint Handle { get; set; }
        }
    }
}