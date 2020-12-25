using System.Text.Json.Serialization;
using ch1seL.TonNet.Client;
using ch1seL.TonNet.Client.Models;

namespace ch1seL.TonNet.RustAdapter.Models
{
    public class ErrorResponse
    {
        [JsonPropertyName("error")] public ClientError Error { get; set; }
    }
}