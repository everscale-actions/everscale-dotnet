using Dahomey.Json.Attributes;
using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class ResultOfEncodeExternalInMessage
    {
        /// <summary>
        /// Message BOC encoded with `base64`.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// Message id.
        /// </summary>
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; }
    }
}