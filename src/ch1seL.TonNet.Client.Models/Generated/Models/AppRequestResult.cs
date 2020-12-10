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
    public abstract class AppRequestResult
    {
        /// <summary>
        /// Error occured during request processing
        /// </summary>
        [JsonDiscriminator("Error")]
        public class Error : AppRequestResult
        {
            /// <summary>
            /// Error occured during request processing
            /// </summary>
            [JsonPropertyName("text")]
            public string Text { get; set; }
        }

        /// <summary>
        /// Request processed successfully
        /// </summary>
        [JsonDiscriminator("Ok")]
        public class Ok : AppRequestResult
        {
            /// <summary>
            /// Request processed successfully
            /// </summary>
            [JsonPropertyName("result")]
            public JsonElement? Result { get; set; }
        }
    }
}