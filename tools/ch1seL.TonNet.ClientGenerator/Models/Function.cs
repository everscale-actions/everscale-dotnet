using System.Text.Json.Serialization;

namespace ch1seL.TonNet.ClientGenerator.Models;

public class Function {
	[JsonPropertyName("name")]
	public string Name { get; set; }
	[JsonPropertyName("summary")]
	public string Summary { get; set; }
	[JsonPropertyName("description")]
	public string Description { get; set; }
	[JsonPropertyName("params")]
	public Param[] Params { get; set; }
	[JsonPropertyName("result")]
	public Result Result { get; set; }
	[JsonPropertyName("errors")]
	public object Errors { get; set; }
}