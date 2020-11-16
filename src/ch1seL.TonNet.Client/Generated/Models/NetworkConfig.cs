using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    public class NetworkConfig
    {
        [JsonPropertyName("server_address")]
        public string ServerAddress { get; set; }
        [JsonPropertyName("network_retries_count"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public sbyte? NetworkRetriesCount { get; set; }
        [JsonPropertyName("message_retries_count"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public sbyte? MessageRetriesCount { get; set; }
        [JsonPropertyName("message_processing_timeout"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public uint? MessageProcessingTimeout { get; set; }
        [JsonPropertyName("wait_for_timeout"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public uint? WaitForTimeout { get; set; }
        [JsonPropertyName("out_of_sync_threshold"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public uint? OutOfSyncThreshold { get; set; }
        [JsonPropertyName("access_key")]
        public string AccessKey { get; set; }
    }
}