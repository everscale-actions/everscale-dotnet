using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class AttachSignatureToMessageBodyRequest
    {
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }
        [JsonPropertyName("public_key")]
        public string PublicKey { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("signature")]
        public string Signature { get; set; }
    }
}