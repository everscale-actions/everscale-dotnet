namespace EverscaleNet.Serialization;

/// <summary>
///     Register discriminator and converters
/// </summary>
public static class JsonOptionsProvider {
	/// <summary>
	///     JsonSerializerOptions instance
	/// </summary>
	public static readonly JsonSerializerOptions JsonSerializerOptions = CreateJsonSerializerOptions();

	private static JsonSerializerOptions CreateJsonSerializerOptions() {
		var options = new JsonSerializerOptions {
			MaxDepth = int.MaxValue,
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
		};

		options.Converters.Add(new BigIntegerConverter());

		return options;
	}
}
