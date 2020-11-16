using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class EncodeMessageRequest
    {
        [JsonPropertyName("abi")]
        public Abi Abi { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("deploy_set")]
        public DeploySet DeploySet { get; set; }
        [JsonPropertyName("call_set")]
        public CallSet CallSet { get; set; }
        [JsonPropertyName("signer")]
        public Signer Signer { get; set; }
        [JsonPropertyName("processing_try_index"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public byte? ProcessingTryIndex { get; set; }
    }
}