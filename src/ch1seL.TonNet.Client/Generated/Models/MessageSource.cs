using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public abstract class MessageSource
    {
        public class Encoded : MessageSource
        {
            [JsonPropertyName("message")]
            public string Message { get; set; }
            [JsonPropertyName("abi")]
            public Abi Abi { get; set; }
        }

        [JsonPropertyName("EncodingParams")]
        public EncodeMessageRequest EncodingParams { get; set; }
    }
}