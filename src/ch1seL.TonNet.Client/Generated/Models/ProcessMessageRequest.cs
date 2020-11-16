using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class ProcessMessageRequest
    {
        [JsonPropertyName("message_encode_params")]
        public EncodeMessageRequest MessageEncodeParams { get; set; }
        [JsonPropertyName("send_events")]
        public bool SendEvents { get; set; }
    }
}