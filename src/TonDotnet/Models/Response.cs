using System.Text.Json;

namespace TonDotnet.Models
{
    public class Response
    {
        public JsonElement Result { get; set; }

        public JsonElement Error { get; set; }
    }
}