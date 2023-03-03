using System.Text.Json.Serialization;

namespace EverscaleNet.Models;

/// <summary>
///     Its own class so that the user does not have the opportunity to override binding info
/// </summary>
public class BindingConfig {
	/// <summary>
	///     Binding library
	/// </summary>
	[JsonPropertyName("library")]
	public string Library => Static.BindingName;

	/// <summary>
	///     Binding version
	/// </summary>
	[JsonPropertyName("version")]
	public string Version => Static.SdkVersion;
}
