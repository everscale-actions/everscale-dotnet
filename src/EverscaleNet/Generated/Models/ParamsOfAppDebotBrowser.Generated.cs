using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>[UNSTABLE](UNSTABLE.md) Debot Browser callbacks</para>
    /// <para>Called by debot engine to communicate with debot browser.</para>
    /// </summary>
#if NET7_0_OR_GREATER
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
#endif
    public abstract class ParamsOfAppDebotBrowser
    {
        /// <summary>
        /// <para>Print message to user.</para>
        /// </summary>
#if !NET7_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("Log")]
#endif
        public class Log : ParamsOfAppDebotBrowser
        {
            /// <summary>
            /// <para>Print message to user.</para>
            /// </summary>
            [JsonPropertyName("msg")]
            public string Msg { get; set; }
        }

        /// <summary>
        /// <para>Switch debot to another context (menu).</para>
        /// </summary>
#if !NET7_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("Switch")]
#endif
        public class Switch : ParamsOfAppDebotBrowser
        {
            /// <summary>
            /// <para>Switch debot to another context (menu).</para>
            /// </summary>
            [JsonPropertyName("context_id")]
            public byte ContextId { get; set; }
        }

        /// <summary>
        /// <para>Notify browser that all context actions are shown.</para>
        /// </summary>
#if !NET7_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("SwitchCompleted")]
#endif
        public class SwitchCompleted : ParamsOfAppDebotBrowser
        {
        }

        /// <summary>
        /// <para>Show action to the user. Called after `switch` for each action in context.</para>
        /// </summary>
#if !NET7_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("ShowAction")]
#endif
        public class ShowAction : ParamsOfAppDebotBrowser
        {
            /// <summary>
            /// <para>Show action to the user. Called after `switch` for each action in context.</para>
            /// </summary>
            [JsonPropertyName("action")]
            public DebotAction Action { get; set; }
        }

        /// <summary>
        /// <para>Request user input.</para>
        /// </summary>
#if !NET7_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("Input")]
#endif
        public class Input : ParamsOfAppDebotBrowser
        {
            /// <summary>
            /// <para>Request user input.</para>
            /// </summary>
            [JsonPropertyName("prompt")]
            public string Prompt { get; set; }
        }

        /// <summary>
        /// <para>Get signing box to sign data.</para>
        /// <para>Signing box returned is owned and disposed by debot engine</para>
        /// </summary>
#if !NET7_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("GetSigningBox")]
#endif
        public class GetSigningBox : ParamsOfAppDebotBrowser
        {
        }

        /// <summary>
        /// <para>Execute action of another debot.</para>
        /// </summary>
#if !NET7_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("InvokeDebot")]
#endif
        public class InvokeDebot : ParamsOfAppDebotBrowser
        {
            /// <summary>
            /// <para>Execute action of another debot.</para>
            /// </summary>
            [JsonPropertyName("debot_addr")]
            public string DebotAddr { get; set; }

            /// <summary>
            /// <para>Execute action of another debot.</para>
            /// </summary>
            [JsonPropertyName("action")]
            public DebotAction Action { get; set; }
        }

        /// <summary>
        /// <para>Used by Debot to call DInterface implemented by Debot Browser.</para>
        /// </summary>
#if !NET7_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("Send")]
#endif
        public class Send : ParamsOfAppDebotBrowser
        {
            /// <summary>
            /// <para>Used by Debot to call DInterface implemented by Debot Browser.</para>
            /// </summary>
            [JsonPropertyName("message")]
            public string Message { get; set; }
        }

        /// <summary>
        /// <para>Requests permission from DeBot Browser to execute DeBot operation.</para>
        /// </summary>
#if !NET7_0_OR_GREATER
        [Dahomey.Json.Attributes.JsonDiscriminator("Approve")]
#endif
        public class Approve : ParamsOfAppDebotBrowser
        {
            /// <summary>
            /// <para>Requests permission from DeBot Browser to execute DeBot operation.</para>
            /// </summary>
            [JsonPropertyName("activity")]
            public DebotActivity Activity { get; set; }
        }
    }
}