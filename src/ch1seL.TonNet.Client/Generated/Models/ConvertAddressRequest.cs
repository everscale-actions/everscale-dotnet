using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class ConvertAddressRequest
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("output_format")]
        public AddressStringFormat OutputFormat { get; set; }
    }
}