using System;
using System.Buffers;
using System.Text.Json;

namespace EverscaleNet.Serialization;

/// <summary>
/// </summary>
public static class JsonExtensions {
	/// <summary>
	///     Short getting JsonElement property as object
	/// </summary>
	/// <param name="element"></param>
	/// <param name="property"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static T Get<T>(this JsonElement? element, string property) {
		return element!.Value.GetProperty(property).ToObject<T>();
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
	///     Convert JsonElement to prototype
	/// </summary>
	/// <param name="element"></param>
	/// <param name="prototype"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	// ReSharper disable once UnusedParameter.Global
	public static T ToAnonymous<T>(this JsonElement element, T prototype) {
		var bufferWriter = new ArrayBufferWriter<byte>();
		using (var writer = new Utf8JsonWriter(bufferWriter)) {
			element.WriteTo(writer);
		}
		return JsonSerializer.Deserialize<T>(bufferWriter.WrittenSpan, JsonOptionsProvider.JsonSerializerOptions)!;
	}

	/// <summary>
	/// </summary>
	/// <param name="element"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static T ToObject<T>(this JsonElement? element) {
		var bufferWriter = new ArrayBufferWriter<byte>();
		using (var writer = new Utf8JsonWriter(bufferWriter)) {
			element!.Value.WriteTo(writer);
		}
		return JsonSerializer.Deserialize<T>(bufferWriter.WrittenSpan, JsonOptionsProvider.JsonSerializerOptions)!;
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

		return (discriminatorType != null
			        ? (T)JsonSerializer.Deserialize(bufferWriter.WrittenSpan, discriminatorType)!
			        : JsonSerializer.Deserialize<T>(bufferWriter.WrittenSpan, JsonOptionsProvider.JsonSerializerOptions))!;
	}

	/// <summary>
	///     Serialize Object to JsonElement
	/// </summary>
	/// <param name="element"></param>
	/// <returns></returns>
	public static JsonElement ToJsonElement(this object element) {
		return JsonDocument.Parse(JsonSerializer.Serialize(element, JsonOptionsProvider.JsonSerializerOptions)).RootElement;
	}
}
