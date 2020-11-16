using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class AttachSignatureResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; }
    }
}