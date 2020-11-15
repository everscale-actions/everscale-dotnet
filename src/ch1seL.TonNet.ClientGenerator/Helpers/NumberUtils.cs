using System;
using System.Collections.Generic;
using System.Linq;
using ch1seL.TonNet.ClientGenerator.Models;

namespace ch1seL.TonNet.ClientGenerator.Helpers
{
    internal static class NumberUtils
    {
        public static Dictionary<string, string> MapNumericTypes(Module module)
        {
            return module.Types.Where(t => t.Type == TypeType.Number)
                .Select(t => new {typeName = NamingConventions.Formatter(t.Name), sharpType = ConvertToSharpNumeric(t.NumberType, t.NumberSize)})
                .ToDictionary(kv => kv.typeName, kv => kv.sharpType);
        }

        public static string ConvertToSharpNumeric(NumberType? numberType, long? size)
        {
            if (numberType == null) throw new ArgumentNullException(nameof(NumberType));
            if (size == null) throw new ArgumentNullException(nameof(NumberType));

            return numberType switch
            {
                NumberType.Float => size switch
                {
                    32 => "float",
                    64 => "double",
                    _ => "float"
                },
                NumberType.UInt => size switch
                {
                    8 => "byte",
                    16 => "ushort",
                    32 => "uint",
                    64 => "ulong",
                    _ => "uint"
                },
                NumberType.Int => size switch
                {
                    8 => "sbyte",
                    16 => "short",
                    32 => "int",
                    64 => "long",
                    _ => "int"
                },
                _ => throw new ArgumentOutOfRangeException(nameof(numberType), numberType, "Unknown number type")
            };
        }
    }
}