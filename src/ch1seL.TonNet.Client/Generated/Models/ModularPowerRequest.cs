using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class ModularPowerRequest
    {
        [JsonPropertyName("base")]
        public string Base { get; set; }
        [JsonPropertyName("exponent")]
        public string Exponent { get; set; }
        [JsonPropertyName("modulus")]
        public string Modulus { get; set; }
    }
}