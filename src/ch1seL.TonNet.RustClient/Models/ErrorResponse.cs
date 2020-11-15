using System.Text.Json;

namespace ch1seL.TonNet.RustClient.Models
{
    public class ErrorResponse
    {
        // todo: decompose it 
        public JsonElement Error { get; set; }
    }
}