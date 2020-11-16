using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class FactorizeRequest
    {
        [JsonPropertyName("composite")]
        public string Composite { get; set; }
    }
}