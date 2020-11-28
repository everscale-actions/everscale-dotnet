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
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("Error")]
        public class Error : AppRequestResult
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("text")]
            public string Text { get; set; }
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("Ok")]
        public class Ok : AppRequestResult
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("result")]
            public JsonElement? Result { get; set; }
        }
    }
}