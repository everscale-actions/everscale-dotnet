using System.Text.Json.Serialization;

namespace EverscaleNet.Models;

public class BindingConfig {
	[JsonPropertyName("library")]
	public string Library => Static.BindingName;

	[JsonPropertyName("version")]
	public string Version => Static.SdkVersion;
}
