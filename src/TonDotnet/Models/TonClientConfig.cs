using System.Text.Json.Serialization;

namespace TonDotnet.Models
{
    public class TonClientConfig
    {
        [JsonPropertyName("base_url")] public string BaseUrl { get; set; }

        [JsonPropertyName("message_retries_count")]
        public sbyte MessageRetriesCount { get; set; }

        [JsonPropertyName("message_expiration_timeout")]
        public int MessageExpirationTimeout { get; set; }

        [JsonPropertyName("message_expiration_timeout_grow_factor")]
        public float MessageExpirationTimeoutGrowFactor { get; set; }

        [JsonPropertyName("message_processing_timeout")]
        public int MessageProcessingTimeout { get; set; }

        [JsonPropertyName("message_processing_timeout_grow_factor")]
        public float MessageProcessingTimeoutGrowFactor { get; set; }

        [JsonPropertyName("wait_for_timeout")] public int WaitForTimeout { get; set; }

        [JsonPropertyName("access_key")] public string AccessKey { get; set; }
    }
}