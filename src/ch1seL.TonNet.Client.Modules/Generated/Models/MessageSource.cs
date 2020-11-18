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
    public abstract class MessageSource
    {
        [JsonDiscriminator("Encoded")]
        /// <summary>
        /// Not described yet..
        /// </summary>
        public class Encoded : MessageSource
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("message")]
            public string Message { get; set; }

            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("abi")]
            public Abi Abi { get; set; }
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("EncodingParams")]
        public ParamsOfEncodeMessage EncodingParams { get; set; }
    }
}