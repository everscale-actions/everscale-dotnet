using System.Text.Json.Serialization;

namespace ch1seL.TonNet.ClientGenerator.Models
{
    public class TypeElement
    {
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("type")] public TypeType Type { get; set; }

        [JsonPropertyName("struct_fields")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public StructField[] StructFields { get; set; }

        [JsonPropertyName("summary")] public string Summary { get; set; }
        [JsonPropertyName("description")] public string Description { get; set; }

        [JsonPropertyName("number_type")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public NumberType? NumberType { get; set; }

        [JsonPropertyName("number_size")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? NumberSize { get; set; }

        [JsonPropertyName("enum_types")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public EnumType[] EnumTypes { get; set; }

        [JsonPropertyName("enum_consts")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public EnumConst[] EnumConsts { get; set; }
    }
}