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
    public abstract class ResultOfAppSigningBox
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("GetPublicKey")]
        public class GetPublicKey : ResultOfAppSigningBox
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
        [JsonDiscriminator("Sign")]
        public class Sign : ResultOfAppSigningBox
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("signature")]
            public string Signature { get; set; }
        }
    }
}