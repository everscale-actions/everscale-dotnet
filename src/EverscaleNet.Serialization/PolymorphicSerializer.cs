﻿namespace EverscaleNet.Serialization;

/// <summary>
/// </summary>
public static class PolymorphicSerializer {
	/// <summary>
	///     Deserialize JsonElement to Polymorphic type
	/// </summary>
	/// <param name="jsonElement"></param>
	/// <typeparam name="TEvent"></typeparam>
	/// <returns></returns>
	public static TEvent Deserialize<TEvent>(JsonElement jsonElement) {
		if (typeof(TEvent) == typeof(JsonElement)) {
			return (TEvent)(object)jsonElement;
		}

		Type[] nestedTypes = typeof(TEvent).GetNestedTypes();

		if (nestedTypes.Length == 0) {
			return jsonElement.ToObject<TEvent>();
		}

		var nestedTypeName = jsonElement.Get<string>("type");
		Type type = nestedTypes.Single(t => t.Name == nestedTypeName);
		return jsonElement.ToObject<TEvent>(type);
	}
}
