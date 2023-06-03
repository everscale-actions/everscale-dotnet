namespace EverscaleNet.ClientGenerator.Models;

public class ArrayItem {
	[JsonPropertyName("type")]
	public ApiType Type { get; set; }

	[JsonPropertyName("ref_name")]
	public string RefName { get; set; }

	[JsonPropertyName("optional_inner")]
	public GenericArg OptionalInner { get; set; }
}
