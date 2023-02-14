using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class DecodedMessageBody
    {
        /// <summary>
        /// <para>Type of the message body content.</para>
        /// </summary>
        [JsonPropertyName("body_type")]
        public MessageBodyType? BodyType { get; set; }

        /// <summary>
        /// <para>Function or event name.</para>
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// <para>Parameters or result value.</para>
        /// </summary>
        [JsonPropertyName("value")]
        public JsonElement? Value { get; set; }

        /// <summary>
        /// <para>Function header.</para>
        /// </summary>
        [JsonPropertyName("header")]
        public FunctionHeader Header { get; set; }
    }
}