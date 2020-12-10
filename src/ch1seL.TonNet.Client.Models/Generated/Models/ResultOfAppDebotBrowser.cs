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
    public abstract class ResultOfAppDebotBrowser
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("Input")]
        public class Input : ResultOfAppDebotBrowser
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("value")]
            public string Value { get; set; }
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("GetSigningBox")]
        public class GetSigningBox : ResultOfAppDebotBrowser
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("signing_box")]
            public uint SigningBox { get; set; }
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("InvokeDebot")]
        public class InvokeDebot : ResultOfAppDebotBrowser
        {
        }
    }
}