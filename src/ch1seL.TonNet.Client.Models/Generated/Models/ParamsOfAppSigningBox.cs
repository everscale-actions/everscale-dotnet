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
    public abstract class ParamsOfAppSigningBox
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("GetPublicKey")]
        public class GetPublicKey : ParamsOfAppSigningBox
        {
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("Sign")]
        public class Sign : ParamsOfAppSigningBox
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("unsigned")]
            public string Unsigned { get; set; }
        }
    }
}