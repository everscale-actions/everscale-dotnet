using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
#if NET7_0_OR_GREATER
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(Error), nameof(Error))]
    [JsonDerivedType(typeof(Ok), nameof(Ok))]
#endif
    public abstract class AppRequestResult
    {
        /// <summary>
        /// <para>Error occurred during request processing</para>
        /// </summary>
#if !NET7_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("Error")]
#endif
        public class Error : AppRequestResult
        {
            /// <summary>
            /// <para>Error occurred during request processing</para>
            /// </summary>
            [JsonPropertyName("text")]
            public string Text { get; set; }
        }

        /// <summary>
        /// <para>Request processed successfully</para>
        /// </summary>
#if !NET7_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("Ok")]
#endif
        public class Ok : AppRequestResult
        {
            /// <summary>
            /// <para>Request processed successfully</para>
            /// </summary>
            [JsonPropertyName("result")]
            public JsonElement? Result { get; set; }
        }
    }
}