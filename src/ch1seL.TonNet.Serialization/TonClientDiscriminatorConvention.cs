using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Dahomey.Json;
using Dahomey.Json.Serialization.Conventions;

namespace ch1seL.TonNet.Serialization
{
    public class TonClientDiscriminatorConvention : IDiscriminatorConvention
    {
        private readonly Dictionary<Type, string> _discriminatorsByType = new Dictionary<Type, string>();
        private readonly JsonConverter<string> _jsonConverter;
        private readonly ReadOnlyMemory<byte> _memberName;
        private readonly JsonSerializerOptions _options;
        private readonly Dictionary<string, Type> _typesByDiscriminator = new Dictionary<string, Type>();

        public TonClientDiscriminatorConvention(JsonSerializerOptions options)
            : this(options, "type")
        {
        }

        private TonClientDiscriminatorConvention(JsonSerializerOptions options, string memberName)
        {
            _options = options;
            _memberName = (ReadOnlyMemory<byte>)Encoding.UTF8.GetBytes(memberName);
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

            var split = obj.Split('+');
            var discriminator = split.Length == 1 ? obj : split[1];

            _jsonConverter.Write(writer, discriminator, _options);
        }
    }
}