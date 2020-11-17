using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ch1seL.TonNet.RustClient.Models
{
    public class RustClientError
    {
        [JsonPropertyName("string")] public string Source { get; set; }

        [JsonPropertyName("code")] public int Code { get; set; }

        [JsonPropertyName("message")] public string Message { get; set; }

        [JsonPropertyName("data")] public Dictionary<string, object> Data { get; set; }

        [JsonPropertyName("coreVersion")] public string CoreVersion { get; set; }
    }
}