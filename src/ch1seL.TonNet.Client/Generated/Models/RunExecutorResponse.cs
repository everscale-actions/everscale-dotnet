using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class RunExecutorResponse
    {
        [JsonPropertyName("transaction")]
        public JsonElement Transaction { get; set; }
        [JsonPropertyName("out_messages")]
        public string[] OutMessages { get; set; }
        [JsonPropertyName("decoded")]
        public DecodedOutput Decoded { get; set; }
        [JsonPropertyName("account")]
        public string Account { get; set; }
        [JsonPropertyName("fees")]
        public TransactionFees Fees { get; set; }
    }
}