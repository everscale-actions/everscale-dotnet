using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class NaclBoxRequest
    {
        [JsonPropertyName("decrypted")]
        public string Decrypted { get; set; }
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }
        [JsonPropertyName("their_public")]
        public string TheirPublic { get; set; }
        [JsonPropertyName("secret")]
        public string Secret { get; set; }
    }
}