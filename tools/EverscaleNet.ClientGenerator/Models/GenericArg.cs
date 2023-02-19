using System.Text.Json.Serialization;

namespace EverscaleNet.ClientGenerator.Models;

public class GenericArg {
	[JsonPropertyName("type")]
	public Type Type { get; set; }

	[JsonPropertyName("ref_name")]
	public string RefName { get; set; }
}
