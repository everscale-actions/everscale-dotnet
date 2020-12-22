using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// <para>[UNSTABLE](UNSTABLE.md) Debot Browser callbacks</para>
    /// <para>Called by debot engine to communicate with debot browser.</para>
    /// </summary>
    public abstract class ParamsOfAppDebotBrowser
    {
        /// <summary>
        /// Print message to user.
        /// </summary>
        [JsonDiscriminator("Log")]
        public class Log : ParamsOfAppDebotBrowser
        {
            /// <summary>
            /// Print message to user.
            /// </summary>
            [JsonPropertyName("msg")]
            public string Msg { get; set; }
        }

        /// <summary>
        /// Switch debot to another context (menu).
        /// </summary>
        [JsonDiscriminator("Switch")]
        public class Switch : ParamsOfAppDebotBrowser
        {
            /// <summary>
            /// Switch debot to another context (menu).
            /// </summary>
            [JsonPropertyName("context_id")]
            public byte ContextId { get; set; }
        }

        /// <summary>
        /// Notify browser that all context actions are shown.
        /// </summary>
        [JsonDiscriminator("SwitchCompleted")]
        public class SwitchCompleted : ParamsOfAppDebotBrowser
        {
        }

        /// <summary>
        /// Show action to the user. Called after `switch` for each action in context.
        /// </summary>
        [JsonDiscriminator("ShowAction")]
        public class ShowAction : ParamsOfAppDebotBrowser
        {
            /// <summary>
            /// Show action to the user. Called after `switch` for each action in context.
            /// </summary>
            [JsonPropertyName("action")]
            public DebotAction Action { get; set; }
        }

        /// <summary>
        /// Request user input.
        /// </summary>
        [JsonDiscriminator("Input")]
        public class Input : ParamsOfAppDebotBrowser
        {
            /// <summary>
            /// Request user input.
            /// </summary>
            [JsonPropertyName("prompt")]
            public string Prompt { get; set; }
        }

        /// <summary>
        /// <para>Get signing box to sign data.</para>
        /// <para>Signing box returned is owned and disposed by debot engine</para>
        /// </summary>
        [JsonDiscriminator("GetSigningBox")]
        public class GetSigningBox : ParamsOfAppDebotBrowser
        {
        }

        /// <summary>
        /// Execute action of another debot.
        /// </summary>
        [JsonDiscriminator("InvokeDebot")]
        public class InvokeDebot : ParamsOfAppDebotBrowser
        {
            /// <summary>
            /// Execute action of another debot.
            /// </summary>
            [JsonPropertyName("debot_addr")]
            public string DebotAddr { get; set; }

            /// <summary>
            /// Execute action of another debot.
            /// </summary>
            [JsonPropertyName("action")]
            public DebotAction Action { get; set; }
        }
    }
}