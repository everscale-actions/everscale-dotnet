using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class FunctionHeader
    {
        [JsonPropertyName("expire"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public uint? Expire { get; set; }
        [JsonPropertyName("time"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public BigInteger? Time { get; set; }
        [JsonPropertyName("pubkey")]
        public string Pubkey { get; set; }
    }
}