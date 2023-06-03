namespace EverscaleNet.ClientGenerator.Models;

public class EnumConst {
	[JsonPropertyName("name")]
	public string Name { get; set; }
	[JsonPropertyName("type")]
	public ApiType Type { get; set; }
	[JsonPropertyName("value")]
	public string Value { get; set; }
	[JsonPropertyName("summary")]
	public string Summary { get; set; }
	[JsonPropertyName("description")]
	public string Description { get; set; }
}
