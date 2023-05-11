using System.Text.Json;
using System.Text.Json.Serialization;
#if !NET6_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Linq;
using EverscaleNet.Client.Models;
using Dahomey.Json;
using Dahomey.Json.Attributes;
using Dahomey.Json.Serialization.Conventions;
#endif

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
#if NETSTANDARD2_1 || NET5_0_OR_GREATER
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
#else
			IgnoreNullValues = true
#endif
		};

#if !NET6_0_OR_GREATER
		IEnumerable<Type> nestedTypes = typeof(Abi.Contract).Assembly.GetTypes().Where(t => t.IsNestedPublic);
		options.SetupExtensions();
		DiscriminatorConventionRegistry registry = options.GetDiscriminatorConventionRegistry();
		registry.ClearConventions();
		registry.RegisterConvention(new EverClientDiscriminatorConvention(options));
		registry.DiscriminatorPolicy = DiscriminatorPolicy.Always;

		// register all nested types from models
		foreach (Type type in nestedTypes) {
			registry.RegisterType(type);
		}
#endif

		return options;
	}
}
