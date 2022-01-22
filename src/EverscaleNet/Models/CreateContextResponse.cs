using System.Text.Json.Serialization;
using EverscaleNet.Client.Models;

namespace EverscaleNet.Models;

/// <summary>
///     Result of context creation
/// </summary>
public class CreateContextResponse {
	[JsonPropertyName("result")]
	public uint? ContextId { get; set; }

	[JsonPropertyName("error")]
	public ClientError Error { get; set; }
}