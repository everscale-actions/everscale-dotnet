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
    [JsonDerivedType(typeof(Message), nameof(Message))]
    [JsonDerivedType(typeof(StateInit), nameof(StateInit))]
    [JsonDerivedType(typeof(Tvc), nameof(Tvc))]
    public abstract class StateInitSource
    {
        /// <summary>
        /// <para>Deploy message.</para>
        /// </summary>
        public class Message : StateInitSource
        {
            /// <summary>
            /// <para>Not described yet..</para>
            /// </summary>
            [JsonPropertyName("source")]
            public MessageSource Source { get; set; }
        }

        /// <summary>
        /// <para>State init data.</para>
        /// </summary>
        public class StateInit : StateInitSource
        {
            /// <summary>
            /// <para>Code BOC.</para>
            /// </summary>
            [JsonPropertyName("code")]
            public string Code { get; set; }

            /// <summary>
            /// <para>Data BOC.</para>
            /// </summary>
            [JsonPropertyName("data")]
            public string Data { get; set; }

            /// <summary>
            /// <para>Library BOC.</para>
            /// </summary>
            [JsonPropertyName("library")]
            public string Library { get; set; }
        }

        /// <summary>
        /// <para>Content of the TVC file.</para>
        /// <para>Encoded in `base64`.</para>
        /// </summary>
        public class Tvc : StateInitSource
        {
            /// <summary>
            /// <para>Not described yet..</para>
            /// </summary>
            [JsonPropertyName("tvc")]
            public string TvcAccessor { get; set; }

            /// <summary>
            /// <para>Not described yet..</para>
            /// </summary>
            [JsonPropertyName("public_key")]
            public string PublicKey { get; set; }

            /// <summary>
            /// <para>Not described yet..</para>
            /// </summary>
            [JsonPropertyName("init_params")]
            public StateInitParams InitParams { get; set; }
        }
    }
}