using System.Text.Json.Serialization;

namespace EverscaleNet.ClientGenerator.Models;

public class OptionalInnerOptionalInner {
	[JsonPropertyName("type")]
	public PurpleType Type { get; set; }
	[JsonPropertyName("number_type")]
	public NumberType NumberType { get; set; }
	[JsonPropertyName("number_size")]
	public long NumberSize { get; set; }
}
