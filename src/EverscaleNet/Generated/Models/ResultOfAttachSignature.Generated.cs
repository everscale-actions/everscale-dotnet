using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EverscaleNet.Client.Models
{
    /// <summary>
    /// <para>Not described yet..</para>
    /// </summary>
    public class ResultOfAttachSignature
    {
        /// <summary>
        /// <para>Signed message BOC</para>
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// <para>Message ID</para>
        /// </summary>
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; }
    }
}