using System.Text.Json.Serialization;

namespace ch1seL.TonNet.Client
{
    public class CreateContextResponse : ErrorResponse
    {
        [JsonPropertyName("result")] public uint? ContextId { get; set; }
    }
}