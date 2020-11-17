using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client.Models
{
    /// <summary>
    /// Not described yet..
    /// </summary>
    public class NetworkConfig
    {
        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("server_address")]
        public string ServerAddress { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("network_retries_count"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public sbyte? NetworkRetriesCount { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("message_retries_count"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public sbyte? MessageRetriesCount { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("message_processing_timeout"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public uint? MessageProcessingTimeout { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("wait_for_timeout"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public uint? WaitForTimeout { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("out_of_sync_threshold"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public uint? OutOfSyncThreshold { get; set; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        [JsonPropertyName("access_key")]
        public string AccessKey { get; set; }
    }
}