using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class EncodeMessageResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("data_to_sign")]
        public string DataToSign { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; }
    }
}