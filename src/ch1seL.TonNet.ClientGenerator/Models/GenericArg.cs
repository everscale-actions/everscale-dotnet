using System.Text.Json.Serialization;

namespace ch1seL.TonNet.ClientGenerator.Models
{
    public class GenericArg
    {
        [JsonPropertyName("type")] public GenericArgType Type { get; set; }

        [JsonPropertyName("ref_name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string RefName { get; set; }
    }
}