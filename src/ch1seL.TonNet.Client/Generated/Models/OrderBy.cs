using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class OrderBy
    {
        [JsonPropertyName("path")]
        public string Path { get; set; }
        [JsonPropertyName("direction")]
        public SortDirection Direction { get; set; }
    }
}