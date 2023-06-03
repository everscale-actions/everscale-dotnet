namespace EverscaleNet.ClientGenerator.Models;

public class Result {
	[JsonPropertyName("type")]
	public ApiType Type { get; set; }
	[JsonPropertyName("generic_name")]
	public ResultGenericName GenericName { get; set; }
	[JsonPropertyName("generic_args")]
	public GenericArg[] GenericArgs { get; set; }
}
