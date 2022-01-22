using System.Text.Json.Serialization;

namespace EverscaleNet.ClientGenerator.Models;

public class EverApi {
	[JsonPropertyName("version")]
	public string Version { get; set; }
	[JsonPropertyName("modules")]
	public Module[] Modules { get; set; }
}