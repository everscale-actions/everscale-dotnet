namespace EverscaleNet.ClientGenerator.Models;

public class GenericArg {
	[JsonPropertyName("type")]
	public ApiType Type { get; set; }

	[JsonPropertyName("ref_name")]
	public string RefName { get; set; }
}
