using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(Encoded), nameof(Encoded))]
    public abstract class MessageSource
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        public class Encoded : MessageSource
        {
            /// <summary>
            /// <para>Not described yet..</para>
            /// </summary>
            [JsonPropertyName("message")]
            public string Message { get; set; }

            /// <summary>
            /// <para>Not described yet..</para>
            /// </summary>
            [JsonPropertyName("abi")]
            public Abi Abi { get; set; }
        }

        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        [JsonPropertyName("EncodingParams")]
        public ParamsOfEncodeMessage EncodingParams { get; set; }
    }
}