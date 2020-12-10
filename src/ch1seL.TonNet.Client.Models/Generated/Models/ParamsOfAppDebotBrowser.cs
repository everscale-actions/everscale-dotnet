using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Called by debot engine to communicate with debot browser.
    /// </summary>
    public abstract class ParamsOfAppDebotBrowser
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("Log")]
        public class Log : ParamsOfAppDebotBrowser
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("msg")]
            public string Msg { get; set; }
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("Switch")]
        public class Switch : ParamsOfAppDebotBrowser
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("context_id")]
            public byte ContextId { get; set; }
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("ShowAction")]
        public class ShowAction : ParamsOfAppDebotBrowser
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("action")]
            public DebotAction Action { get; set; }
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("Input")]
        public class Input : ParamsOfAppDebotBrowser
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("prompt")]
            public string Prompt { get; set; }
        }

        /// <summary>
        /// Signing box returned is owned and disposed by debot engine
        /// </summary>
        [JsonDiscriminator("GetSigningBox")]
        public class GetSigningBox : ParamsOfAppDebotBrowser
        {
        }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonDiscriminator("InvokeDebot")]
        public class InvokeDebot : ParamsOfAppDebotBrowser
        {
            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("debot_addr")]
            public string DebotAddr { get; set; }

            /// <summary>
            /// Not described yet..
            /// </summary>
            [JsonPropertyName("action")]
            public DebotAction Action { get; set; }
        }
    }
}