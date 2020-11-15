using System.Text.Json.Serialization;

namespace ch1seL.TonNet.ClientGenerator.Models
{
    public class Module
    {
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("summary")] public string Summary { get; set; }
        [JsonPropertyName("description")] public string Description { get; set; }
        [JsonPropertyName("types")] public TypeElement[] Types { get; set; }
        [JsonPropertyName("functions")] public Function[] Functions { get; set; }
    }
}