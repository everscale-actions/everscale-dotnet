namespace EverscaleNet.ClientGenerator.Models;

public class EnumType {
	[JsonPropertyName("name")]
	public string Name { get; set; }
	[JsonPropertyName("type")]
	public ApiType Type { get; set; }

	[JsonPropertyName("struct_fields")]
	public EnumType[] StructFields { get; set; }

	[JsonPropertyName("summary")]
	public string Summary { get; set; }
	[JsonPropertyName("description")]
	public string Description { get; set; }

	[JsonPropertyName("ref_name")]
	public string RefName { get; set; }

	[JsonPropertyName("optional_inner")]
	public GenericArg OptionalInner { get; set; }

	[JsonPropertyName("number_type")]
	public NumberType? NumberType { get; set; }
	[JsonPropertyName("number_size")]
	public long NumberSize { get; set; }
	[JsonPropertyName("array_item")]
	public ArrayItem ArrayItem { get; set; }
}
