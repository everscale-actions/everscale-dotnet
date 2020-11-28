using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// <para> [UNSTABLE](UNSTABLE.md) Debot Browser callbacks</para>
    /// <para> </para>
    /// <para> Called by debot engine to communicate with debot browser.</para>
    /// </summary>
    public abstract class ParamsOfAppDebotBrowser
    {
        /// <summary>
        ///  Print message to user. 
        /// </summary>
        [JsonDiscriminator("Log")]
        public class Log : ParamsOfAppDebotBrowser
        {
            /// <summary>
            ///  Print message to user. 
            /// </summary>
            [JsonPropertyName("msg")]
            public string Msg { get; set; }
        }

        /// <summary>
        ///  Switch debot to another context (menu).
        /// </summary>
        [JsonDiscriminator("Switch")]
        public class Switch : ParamsOfAppDebotBrowser
        {
            /// <summary>
            ///  Switch debot to another context (menu).
            /// </summary>
            [JsonPropertyName("context_id")]
            public byte ContextId { get; set; }
        }

        /// <summary>
        /// <para> Show action to the user.</para>
        /// <para> Called after `switch` for each action in context.</para>
        /// </summary>
        [JsonDiscriminator("ShowAction")]
        public class ShowAction : ParamsOfAppDebotBrowser
        {
            /// <summary>
            /// <para> Show action to the user.</para>
            /// <para> Called after `switch` for each action in context.</para>
            /// </summary>
            [JsonPropertyName("action")]
            public DebotAction Action { get; set; }
        }

        /// <summary>
        ///  Request user input. 
        /// </summary>
        [JsonDiscriminator("Input")]
        public class Input : ParamsOfAppDebotBrowser
        {
            /// <summary>
            ///  Request user input. 
            /// </summary>
            [JsonPropertyName("prompt")]
            public string Prompt { get; set; }
        }

        /// <summary>
        ///  Get signing box to sign data. Signing box returned is owned and disposed by debot engine
        /// </summary>
        [JsonDiscriminator("GetSigningBox")]
        public class GetSigningBox : ParamsOfAppDebotBrowser
        {
        }

        /// <summary>
        ///  Execute action of another debot.
        /// </summary>
        [JsonDiscriminator("InvokeDebot")]
        public class InvokeDebot : ParamsOfAppDebotBrowser
        {
            /// <summary>
            ///  Execute action of another debot.
            /// </summary>
            [JsonPropertyName("debot_addr")]
            public string DebotAddr { get; set; }

            /// <summary>
            ///  Execute action of another debot.
            /// </summary>
            [JsonPropertyName("action")]
            public DebotAction Action { get; set; }
        }
    }
}