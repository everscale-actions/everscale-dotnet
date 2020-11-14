using System.Text.Json;

namespace ch1seL.TonClientDotnet.Models
{
    public class ErrorResponse
    {
        // todo: decompose it 
        public JsonElement Error { get; set; }
    }
}