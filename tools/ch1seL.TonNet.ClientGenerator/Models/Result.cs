using System.Text.Json.Serialization;

namespace ch1seL.TonNet.ClientGenerator.Models;

public class Result {
	[JsonPropertyName("type")]
	public ParamType Type { get; set; }
	[JsonPropertyName("generic_name")]
	public ResultGenericName GenericName { get; set; }
	[JsonPropertyName("generic_args")]
	public GenericArg[] GenericArgs { get; set; }
}