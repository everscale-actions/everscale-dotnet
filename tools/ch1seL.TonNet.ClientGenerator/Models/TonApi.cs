using System.Text.Json.Serialization;

namespace ch1seL.TonNet.ClientGenerator.Models
{
    public class TonApi
    {
        [JsonPropertyName("version")] public string Version { get; set; }
        [JsonPropertyName("modules")] public Module[] Modules { get; set; }
    }
}