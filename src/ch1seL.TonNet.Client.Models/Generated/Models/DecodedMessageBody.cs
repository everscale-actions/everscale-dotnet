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
    public class DecodedMessageBody
    {
        /// <summary>
        ///  Type of the message body content.
        /// </summary>
        [JsonPropertyName("body_type")]
        public MessageBodyType BodyType { get; set; }

        /// <summary>
        ///  Function or event name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        ///  Parameters or result value.
        /// </summary>
        [JsonPropertyName("value")]
        public JsonElement? Value { get; set; }

        /// <summary>
        ///  Function header.
        /// </summary>
        [JsonPropertyName("header")]
        public FunctionHeader Header { get; set; }
    }
}