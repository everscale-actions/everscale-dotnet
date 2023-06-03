namespace EverscaleNet.ClientGenerator.Models;

public class TypeElement {
	[JsonPropertyName("name")]
	public string Name { get; set; }
	[JsonPropertyName("type")]
	public ApiType Type { get; set; }

	[JsonPropertyName("struct_fields")]
	public StructField[] StructFields { get; set; }

	[JsonPropertyName("summary")]
	public string Summary { get; set; }
	[JsonPropertyName("description")]
	public string Description { get; set; }

	[JsonPropertyName("number_type")]
	public NumberType? NumberType { get; set; }

	[JsonPropertyName("number_size")]
	public long? NumberSize { get; set; }

	[JsonPropertyName("enum_types")]
	public EnumType[] EnumTypes { get; set; }

	[JsonPropertyName("enum_consts")]
	public EnumConst[] EnumConsts { get; set; }
}
