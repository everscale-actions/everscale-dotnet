using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using ch1seL.TonNet.Client.Models;
using Dahomey.Json;
using Dahomey.Json.Serialization.Conventions;

namespace ch1seL.TonNet.Serialization
{
    public static class JsonOptionsProvider
    {
        public static readonly JsonSerializerOptions JsonSerializerOptions = CreateJsonSerializerOptions();

        private static JsonSerializerOptions CreateJsonSerializerOptions()
        {
            var options = new JsonSerializerOptions
            {
                MaxDepth = int.MaxValue,
                Converters = { new JsonStringEnumConverter() },
#if NET5_0_OR_GREATER
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
#else
                IgnoreNullValues = true
#endif
            };

            options.SetupExtensions();
            DiscriminatorConventionRegistry registry = options.GetDiscriminatorConventionRegistry();
            registry.ClearConventions();
            registry.RegisterConvention(new TonClientDiscriminatorConvention(options));
            registry.DiscriminatorPolicy = DiscriminatorPolicy.Always;

            var nestedTypes = typeof(Abi.Contract).Assembly.GetTypes().Where(t => t.IsNestedPublic);

            // register all nested types from models
            foreach (Type type in nestedTypes) registry.RegisterType(type);

            return options;
        }
    }
}