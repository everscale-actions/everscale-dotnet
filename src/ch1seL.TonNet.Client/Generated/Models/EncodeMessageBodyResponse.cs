using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class EncodeMessageBodyResponse
    {
        [JsonPropertyName("body")]
        public string Body { get; set; }
        [JsonPropertyName("data_to_sign")]
        public string DataToSign { get; set; }
    }
}