using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// [UNSTABLE](UNSTABLE.md) Returning values from Debot Browser callbacks.
    /// </summary>
    public abstract class ResultOfAppDebotBrowser
    {
        /// <summary>
        /// Result of user input.
        /// </summary>
        [JsonDiscriminator("Input")]
        public class Input : ResultOfAppDebotBrowser
        {
            /// <summary>
            /// Result of user input.
            /// </summary>
            [JsonPropertyName("value")]
            public string Value { get; set; }
        }

        /// <summary>
        /// Result of getting signing box.
        /// </summary>
        [JsonDiscriminator("GetSigningBox")]
        public class GetSigningBox : ResultOfAppDebotBrowser
        {
            /// <summary>
            /// Result of getting signing box.
            /// </summary>
            [JsonPropertyName("signing_box")]
            public uint SigningBox { get; set; }
        }

        /// <summary>
        /// Result of debot invoking.
        /// </summary>
        [JsonDiscriminator("InvokeDebot")]
        public class InvokeDebot : ResultOfAppDebotBrowser
        {
        }
    }
}