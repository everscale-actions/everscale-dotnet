using System.Text.Json.Serialization;

namespace ch1seL.TonNet.ClientGenerator.Models;

public class StructFieldOptionalInner {
	[JsonPropertyName("type")]
	public PurpleType Type { get; set; }

	[JsonPropertyName("ref_name")]
	public string RefName { get; set; }

	[JsonPropertyName("number_type")]
	public NumberType? NumberType { get; set; }

	[JsonPropertyName("number_size")]
	public long? NumberSize { get; set; }

	[JsonPropertyName("array_item")]
	public GenericArg ArrayItem { get; set; }

	[JsonPropertyName("optional_inner")]
	public OptionalInnerOptionalInner OptionalInner { get; set; }
}