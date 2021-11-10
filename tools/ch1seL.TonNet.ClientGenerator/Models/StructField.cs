using System.Text.Json.Serialization;

namespace ch1seL.TonNet.ClientGenerator.Models
{
    public class StructField
    {
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("type")] public PurpleType Type { get; set; }

        [JsonPropertyName("number_type")] public NumberType? NumberType { get; set; }

        [JsonPropertyName("number_size")] public long? NumberSize { get; set; }

        [JsonPropertyName("summary")] public string Summary { get; set; }
        [JsonPropertyName("description")] public string Description { get; set; }

        [JsonPropertyName("ref_name")] public string RefName { get; set; }

        [JsonPropertyName("optional_inner")] public StructFieldOptionalInner OptionalInner { get; set; }

        [JsonPropertyName("array_item")] public ArrayItem ArrayItem { get; set; }
    }
}