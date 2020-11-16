using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class MnemonicWordsRequest
    {
        [JsonPropertyName("dictionary"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public byte? Dictionary { get; set; }
    }
}