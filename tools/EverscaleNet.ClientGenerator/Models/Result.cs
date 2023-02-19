using System.Text.Json.Serialization;

namespace EverscaleNet.ClientGenerator.Models;

public class Result {
	[JsonPropertyName("type")]
	public Type Type { get; set; }
	[JsonPropertyName("generic_name")]
	public ResultGenericName GenericName { get; set; }
	[JsonPropertyName("generic_args")]
	public GenericArg[] GenericArgs { get; set; }
}
