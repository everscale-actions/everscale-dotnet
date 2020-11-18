using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var options = new JsonSerializerOptions {WriteIndented = true};

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

    public class TonClientDiscriminatorConvention : IDiscriminatorConvention
    {
        private readonly Dictionary<Type, string> _discriminatorsByType = new();
        private readonly JsonConverter<string> _jsonConverter;
        private readonly ReadOnlyMemory<byte> _memberName;
        private readonly JsonSerializerOptions _options;
        private readonly Dictionary<string, Type> _typesByDiscriminator = new();

        public TonClientDiscriminatorConvention(JsonSerializerOptions options)
            : this(options, "type")
        {
        }

        public TonClientDiscriminatorConvention(JsonSerializerOptions options, string memberName)
        {
            _options = options;
            _memberName = (ReadOnlyMemory<byte>) Encoding.UTF8.GetBytes(memberName);
            _jsonConverter = options.GetConverter<string>();
        }

        public ReadOnlySpan<byte> MemberName => _memberName.Span;

        public bool TryRegisterType(Type type)
        {
            var typeName = type.ToString();
            var split = typeName.Split('+');
            if (split.Length != 2) return false;

            var discriminator = type.ToString();
            _discriminatorsByType[type] = discriminator;
            _typesByDiscriminator.Add(discriminator, type);
            return true;
        }

        public Type ReadDiscriminator(ref Utf8JsonReader reader)
        {
            var key = _jsonConverter.Read(ref reader, typeof(string), _options);
            if (key == null)
                throw new JsonException("Null discriminator");
            if (!_typesByDiscriminator.TryGetValue(key, out Type type))
                throw new JsonException($"Unknown type discriminator: {key}");
            return type;
        }

        public void WriteDiscriminator(Utf8JsonWriter writer, Type actualType)
        {
            if (!_discriminatorsByType.TryGetValue(actualType, out var obj))
                throw new JsonException($"Unknown discriminator for type: {actualType}");

            var split = obj.Split("+");
            var discriminator = split.Length == 1 ? obj : split[1];

            _jsonConverter.Write(writer, discriminator, _options);
        }
    }


    internal class ByTypeNameEqualityComparer : EqualityComparer<Type>
    {
        public static readonly ByTypeNameEqualityComparer Instance = new();

        public override bool Equals(Type x, Type y)
        {
            return x?.FullName?.Split('+')[1] == y?.FullName?.Split('+')[1];
        }

        public override int GetHashCode(Type obj)
        {
            return obj.FullName?.Split('+')[1].GetHashCode() ?? 0;
        }
    }
}