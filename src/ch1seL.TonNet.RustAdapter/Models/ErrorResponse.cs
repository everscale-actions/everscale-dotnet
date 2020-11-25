using System.Text.Json.Serialization;

namespace ch1seL.TonNet.RustAdapter.Models
{
    public class ErrorResponse
    {
        [JsonPropertyName("error")] public RustClientError Error { get; set; }
    }
}