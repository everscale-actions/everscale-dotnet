namespace EverscaleNet.Serialization;

/// <inheritdoc />
public class BigIntegerConverter : JsonConverter<BigInteger> {
	/// <inheritdoc />
	public override BigInteger Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
		if (reader.TokenType is not (JsonTokenType.Number or JsonTokenType.String)) {
			throw new JsonException($"Found token {reader.TokenType} but expected token {JsonTokenType.Number} or {JsonTokenType.String}");
		}
		using JsonDocument doc = JsonDocument.ParseValue(ref reader);
		return BigInteger.Parse(doc.RootElement.GetRawText(), NumberFormatInfo.InvariantInfo);
	}

	/// <inheritdoc />
	public override void Write(Utf8JsonWriter writer, BigInteger value, JsonSerializerOptions options) {
		writer.WriteStringValue(value.ToString(NumberFormatInfo.InvariantInfo));
	}
}
