using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class EncodeAccountRequest
    {
        [JsonPropertyName("state_init")]
        public StateInitSource StateInit { get; set; }
        [JsonPropertyName("balance"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public BigInteger? Balance { get; set; }
        [JsonPropertyName("last_trans_lt"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public BigInteger? LastTransLt { get; set; }
        [JsonPropertyName("last_paid"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public uint? LastPaid { get; set; }
    }
}