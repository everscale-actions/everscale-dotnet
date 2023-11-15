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
    [JsonDerivedType(typeof(Error), nameof(Error))]
    [JsonDerivedType(typeof(Ok), nameof(Ok))]
    public abstract class AppRequestResult
    {
        /// <summary>
        /// <para>Error occurred during request processing</para>
        /// </summary>
        public class Error : AppRequestResult
        {
            /// <summary>
            /// <para>Error description</para>
            /// </summary>
            [JsonPropertyName("text")]
            public string Text { get; set; }
        }

        /// <summary>
        /// <para>Request processed successfully</para>
        /// </summary>
        public class Ok : AppRequestResult
        {
            /// <summary>
            /// <para>Request processing result</para>
            /// </summary>
            [JsonPropertyName("result")]
            public JsonElement? Result { get; set; }
        }
    }
}