using System.Text.Json.Serialization;

namespace EverscaleNet.ClientGenerator.Models;

public class EnumConst {
	[JsonPropertyName("name")]
	public string Name { get; set; }
	[JsonPropertyName("type")]
	public GenericArgType Type { get; set; }
	[JsonPropertyName("value")]
	public string Value { get; set; }
	[JsonPropertyName("summary")]
	public string Summary { get; set; }
	[JsonPropertyName("description")]
	public string Description { get; set; }
}
