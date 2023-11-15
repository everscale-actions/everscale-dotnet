using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>[UNSTABLE](UNSTABLE.md) [DEPRECATED](DEPRECATED.md) Debot Browser callbacks</para>
    /// <para>Called by debot engine to communicate with debot browser.</para>
    /// </summary>
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(Log), nameof(Log))]
    [JsonDerivedType(typeof(Switch), nameof(Switch))]
    [JsonDerivedType(typeof(SwitchCompleted), nameof(SwitchCompleted))]
    [JsonDerivedType(typeof(ShowAction), nameof(ShowAction))]
    [JsonDerivedType(typeof(Input), nameof(Input))]
    [JsonDerivedType(typeof(GetSigningBox), nameof(GetSigningBox))]
    [JsonDerivedType(typeof(InvokeDebot), nameof(InvokeDebot))]
    [JsonDerivedType(typeof(Send), nameof(Send))]
    [JsonDerivedType(typeof(Approve), nameof(Approve))]
    public abstract class ParamsOfAppDebotBrowser
    {
        /// <summary>
        /// <para>Print message to user.</para>
        /// </summary>
        public class Log : ParamsOfAppDebotBrowser
        {
            /// <summary>
            /// <para>A string that must be printed to user.</para>
            /// </summary>
            [JsonPropertyName("msg")]
            public string Msg { get; set; }
        }

        /// <summary>
        /// <para>Switch debot to another context (menu).</para>
        /// </summary>
        public class Switch : ParamsOfAppDebotBrowser
        {
            /// <summary>
            /// <para>Debot context ID to which debot is switched.</para>
            /// </summary>
            [JsonPropertyName("context_id")]
            public byte ContextId { get; set; }
        }

        /// <summary>
        /// <para>Notify browser that all context actions are shown.</para>
        /// </summary>
        public class SwitchCompleted : ParamsOfAppDebotBrowser
        {
        }

        /// <summary>
        /// <para>Show action to the user. Called after `switch` for each action in context.</para>
        /// </summary>
        public class ShowAction : ParamsOfAppDebotBrowser
        {
            /// <summary>
            /// <para>Debot action that must be shown to user as menu item. At least `description` property must be shown from [DebotAction] structure.</para>
            /// </summary>
            [JsonPropertyName("action")]
            public DebotAction Action { get; set; }
        }

        /// <summary>
        /// <para>Request user input.</para>
        /// </summary>
        public class Input : ParamsOfAppDebotBrowser
        {
            /// <summary>
            /// <para>A prompt string that must be printed to user before input request.</para>
            /// </summary>
            [JsonPropertyName("prompt")]
            public string Prompt { get; set; }
        }

        /// <summary>
        /// <para>Get signing box to sign data.</para>
        /// <para>Signing box returned is owned and disposed by debot engine</para>
        /// </summary>
        public class GetSigningBox : ParamsOfAppDebotBrowser
        {
        }

        /// <summary>
        /// <para>Execute action of another debot.</para>
        /// </summary>
        public class InvokeDebot : ParamsOfAppDebotBrowser
        {
            /// <summary>
            /// <para>Address of debot in blockchain.</para>
            /// </summary>
            [JsonPropertyName("debot_addr")]
            public string DebotAddr { get; set; }

            /// <summary>
            /// <para>Debot action to execute.</para>
            /// </summary>
            [JsonPropertyName("action")]
            public DebotAction Action { get; set; }
        }

        /// <summary>
        /// <para>Used by Debot to call DInterface implemented by Debot Browser.</para>
        /// </summary>
        public class Send : ParamsOfAppDebotBrowser
        {
            /// <summary>
            /// <para>Internal message to DInterface address.</para>
            /// </summary>
            [JsonPropertyName("message")]
            public string Message { get; set; }
        }

        /// <summary>
        /// <para>Requests permission from DeBot Browser to execute DeBot operation.</para>
        /// </summary>
        public class Approve : ParamsOfAppDebotBrowser
        {
            /// <summary>
            /// <para>DeBot activity details.</para>
            /// </summary>
            [JsonPropertyName("activity")]
            public DebotActivity Activity { get; set; }
        }
    }
}