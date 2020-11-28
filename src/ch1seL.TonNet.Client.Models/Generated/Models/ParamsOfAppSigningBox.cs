using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    ///  Signing box callbacks.
    /// </summary>
    public abstract class ParamsOfAppSigningBox
    {
        /// <summary>
        ///  Get signing box public key
        /// </summary>
        [JsonDiscriminator("GetPublicKey")]
        public class GetPublicKey : ParamsOfAppSigningBox
        {
        }

        /// <summary>
        ///  Sign data
        /// </summary>
        [JsonDiscriminator("Sign")]
        public class Sign : ParamsOfAppSigningBox
        {
            /// <summary>
            ///  Sign data
            /// </summary>
            [JsonPropertyName("unsigned")]
            public string Unsigned { get; set; }
        }
    }
}