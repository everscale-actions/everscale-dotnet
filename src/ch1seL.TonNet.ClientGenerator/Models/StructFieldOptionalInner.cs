using System.Text.Json.Serialization;

namespace ch1seL.TonNet.ClientGenerator.Models
{
    public class StructFieldOptionalInner
    {
        [JsonPropertyName("type")] public PurpleType Type { get; set; }

        [JsonPropertyName("ref_name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string RefName { get; set; }

        [JsonPropertyName("number_type")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public NumberType? NumberType { get; set; }

        [JsonPropertyName("number_size")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? NumberSize { get; set; }

        [JsonPropertyName("array_item")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public GenericArg ArrayItem { get; set; }

        [JsonPropertyName("optional_inner")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public OptionalInnerOptionalInner OptionalInner { get; set; }
    }
}