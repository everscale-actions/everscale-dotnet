using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfEncodeExternalInMessage
    {
        /// <summary>
        /// <para>Message BOC encoded with `base64`.</para>
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// <para>Message id.</para>
        /// </summary>
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; }
    }
}