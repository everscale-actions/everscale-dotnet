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
    [JsonDerivedType(typeof(Encoded), nameof(Encoded))]
#endif
    public abstract class MessageSource
    {
        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
#if !NET6_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("Encoded")]
#endif
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