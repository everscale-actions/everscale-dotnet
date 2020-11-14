using System.Text.Json.Serialization;

namespace ch1seL.TonClientDotnet.Models
{
    public class CreateContextResult
    {
        [JsonPropertyName("result")] public uint ContextNumber { get; set; }
        [JsonPropertyName("error")] public string Error { get; set; }
    }
}