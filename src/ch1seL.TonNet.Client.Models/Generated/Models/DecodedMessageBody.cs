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
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("body_type")]
        public MessageBodyType BodyType { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("value")]
        public JsonElement? Value { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("header")]
        public FunctionHeader Header { get; set; }
    }
}