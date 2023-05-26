namespace EverscaleNet.Serialization;

/// <summary>
/// </summary>
public static class JsonSerializerExtensions {
	/// <summary>
	///     Short getting JsonElement property as object
	/// </summary>
	/// <param name="element"></param>
	/// <param name="property"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static T Get<T>([DisallowNull] this JsonElement? element, string property) {
		if (element == null) {
			throw new ArgumentNullException(nameof(element));
		}
		return element.Value.GetProperty(property).ToObject<T>();
	}

	/// <summary>
	///     Short getting JsonElement property as object
	/// </summary>
	/// <param name="element"></param>
	/// <param name="property"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static T Get<T>(this JsonElement element, string property) {
		return element.GetProperty(property).ToObject<T>();
	}

	/// <summary>
	///     Short getting JsonElement property as object
	/// </summary>
	/// <param name="element"></param>
	/// <param name="propertyName">Name of the property to find.</param>
	/// <param name="value">Receives the value of the located property.</param>
	/// <typeparam name="T"></typeparam>
	/// <returns>
	///     <see langword="true" /> if the property was found, <see langword="false" /> otherwise.
	/// </returns>
	public static bool TryGet<T>([DisallowNull] this JsonElement? element, string propertyName, out T value) {
		if (element == null) {
			throw new ArgumentNullException(nameof(element));
		}
		if (element.Value.TryGet(propertyName, out T result)) {
			value = result;
			return true;
		}
		value = default!;
		return false;
	}

	/// <summary>
	///     Short getting JsonElement property as object
	/// </summary>
	/// <param name="element"></param>
	/// <param name="propertyName">Name of the property to find.</param>
	/// <param name="value">Receives the value of the located property.</param>
	/// <typeparam name="T"></typeparam>
	/// <returns>
	///     <see langword="true" /> if the property was found, <see langword="false" /> otherwise.
	/// </returns>
	public static bool TryGet<T>(this JsonElement element, string propertyName, out T value) {
		if (element.TryGetProperty(propertyName, out JsonElement result)) {
			value = result.ToObject<T>();
			return true;
		}
		value = default!;
		return false;
	}

	/// <summary>
	///     Convert JsonElement to prototype
	/// </summary>
	/// <param name="element"></param>
	/// <param name="prototype"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	// ReSharper disable once UnusedParameter.Global
	public static T ToPrototype<T>(this JsonElement element, T prototype) {
		return ToObject<T>(element);
	}

	/// <summary>
	///     Convert JsonElement to prototype
	/// </summary>
	/// <param name="element"></param>
	/// <param name="prototype"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	// ReSharper disable once UnusedParameter.Global
	public static T ToPrototype<T>([DisallowNull] this JsonElement? element, T prototype) {
		if (element == null) {
			throw new ArgumentNullException(nameof(element));
		}
		return ToPrototype(element.Value, prototype);
	}

	/// <summary>
	/// </summary>
	/// <param name="element"></param>
	/// <param name="discriminatorType"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static T ToObject<T>([DisallowNull] this JsonElement? element, Type? discriminatorType = null) {
		if (element == null) {
			throw new ArgumentNullException(nameof(element));
		}
		return ToObject<T>(element.Value, discriminatorType);
	}

	/// <summary>
	///     Deserialize JsonElement to Object
	/// </summary>
	/// <param name="element"></param>
	/// <param name="discriminatorType"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static T ToObject<T>(this JsonElement element, Type? discriminatorType = null) {
		var bufferWriter = new ArrayBufferWriter<byte>();
		using (var writer = new Utf8JsonWriter(bufferWriter)) {
			element.WriteTo(writer);
		}
		return discriminatorType != null
			       ? (T)JsonSerializer.Deserialize(bufferWriter.WrittenSpan, discriminatorType)!
			       : JsonSerializer.Deserialize<T>(bufferWriter.WrittenSpan, JsonOptionsProvider.JsonSerializerOptions)!;
	}

	/// <summary>
	///     Serialize Object to JsonElement
	/// </summary>
	/// <param name="element"></param>
	/// <param name="discriminatorType"></param>
	/// <returns></returns>
	public static JsonElement ToJsonElement(this object element, Type? discriminatorType = null) {
		if (discriminatorType == null && !element.GetType().IsPrimitive && element.GetType().BaseType != null) {
			discriminatorType = element.GetType().BaseType;
		}

#if NET6_0_OR_GREATER
		return discriminatorType == null
			       ? JsonSerializer.SerializeToElement(element, JsonOptionsProvider.JsonSerializerOptions)
			       : JsonSerializer.SerializeToElement(element, discriminatorType, JsonOptionsProvider.JsonSerializerOptions);
#else
		string json = discriminatorType == null
			              ? JsonSerializer.Serialize(element, JsonOptionsProvider.JsonSerializerOptions)
			              : JsonSerializer.Serialize(element, discriminatorType, JsonOptionsProvider.JsonSerializerOptions);
		return JsonDocument.Parse(json).RootElement;
#endif
	}

	/// <summary>
	///     Serialize Object to JsonElement with generic discriminatorType
	/// </summary>
	/// <param name="element"></param>
	/// <returns></returns>
	public static JsonElement ToJsonElement<T>(this object element) {
		return ToJsonElement(element, typeof(T));
	}

	/// <summary>
	///     Deserialize prototype
	/// </summary>
	/// <param name="json"></param>
	/// <param name="prototype"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static T DeserializePrototype<T>(string json, T prototype) {
		return JsonSerializer.Deserialize<T>(json, JsonOptionsProvider.JsonSerializerOptions)!;
	}
}
