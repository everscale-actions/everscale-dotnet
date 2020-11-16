using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class ConvertPublicKeyToTonSafeFormatResponse
    {
        [JsonPropertyName("ton_public_key")]
        public string TonPublicKey { get; set; }
    }
}