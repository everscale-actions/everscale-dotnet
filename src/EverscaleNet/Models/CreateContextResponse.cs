using System.Text.Json.Serialization;
using EverscaleNet.Client.Models;

namespace EverscaleNet.Models;

/// <summary>
///     Result of context creation
/// </summary>
public class CreateContextResponse {
	/// <summary>
	///     Created context id or null if failed
	/// </summary>
	[JsonPropertyName("result")]
	public uint? ContextId { get; set; }

	/// <summary>
	///     Error if failed or null if context was created successfully
	/// </summary>
	[JsonPropertyName("error")]
	public ClientError? Error { get; set; }
}
