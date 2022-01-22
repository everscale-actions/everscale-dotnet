using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// Returning values from signing box callbacks.
    /// </summary>
    public abstract class ResultOfAppSigningBox
    {
        /// <summary>
        /// Result of getting public key
        /// </summary>
        [JsonDiscriminator("GetPublicKey")]
        public class GetPublicKey : ResultOfAppSigningBox
        {
            /// <summary>
            /// Result of getting public key
            /// </summary>
            [JsonPropertyName("public_key")]
            public string PublicKey { get; set; }
        }

        /// <summary>
        /// Result of signing data
        /// </summary>
        [JsonDiscriminator("Sign")]
        public class Sign : ResultOfAppSigningBox
        {
            /// <summary>
            /// Result of signing data
            /// </summary>
            [JsonPropertyName("signature")]
            public string Signature { get; set; }
        }
    }
}