using System.Text.Json.Serialization;

namespace ch1seL.TonNet.ClientGenerator.Models
{
    public class Param
    {
        [JsonPropertyName("name")] public Name Name { get; set; }
        [JsonPropertyName("type")] public ParamType Type { get; set; }

        [JsonPropertyName("generic_name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ParamGenericName? GenericName { get; set; }

        [JsonPropertyName("generic_args")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public GenericArg[] GenericArgs { get; set; }

        [JsonPropertyName("summary")] public object Summary { get; set; }
        [JsonPropertyName("description")] public object Description { get; set; }

        [JsonPropertyName("ref_name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string RefName { get; set; }
    }
}