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
    public class ResultOfAttachSignature
    {
        /// <summary>
        /// Signed message BOC
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// Message ID
        /// </summary>
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; }
    }
}