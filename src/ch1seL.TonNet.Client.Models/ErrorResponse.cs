using System.Text.Json.Serialization;
using ch1seL.TonNet.Client.Models;

namespace ch1seL.TonNet.Client; 

public class ErrorResponse {
	[JsonPropertyName("error")]
	public ClientError Error { get; set; }
}