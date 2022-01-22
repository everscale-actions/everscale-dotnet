using System.Text.Json.Serialization;

namespace EverscaleNet.ClientGenerator.Models;

public class Param {
	[JsonPropertyName("name")]
	public Name Name { get; set; }
	[JsonPropertyName("type")]
	public ParamType Type { get; set; }

	[JsonPropertyName("generic_name")]
	public ParamGenericName? GenericName { get; set; }

	[JsonPropertyName("generic_args")]
	public GenericArg[] GenericArgs { get; set; }

	[JsonPropertyName("summary")]
	public object Summary { get; set; }
	[JsonPropertyName("description")]
	public object Description { get; set; }

	[JsonPropertyName("ref_name")]
	public string RefName { get; set; }
}