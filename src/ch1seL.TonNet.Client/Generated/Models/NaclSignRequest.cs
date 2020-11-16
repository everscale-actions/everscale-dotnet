using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class NaclSignRequest
    {
        [JsonPropertyName("unsigned")]
        public string Unsigned { get; set; }
        [JsonPropertyName("secret")]
        public string Secret { get; set; }
    }
}