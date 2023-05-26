using Type = EverscaleNet.ClientGenerator.Models.Type;

namespace EverscaleNet.ClientGenerator.Helpers;

internal static class NumberUtils {
	public static Dictionary<string, string> MapNumericTypes(IEnumerable<Module> module) {
		return module.SelectMany(m => m.Types).Where(t => t.Type == Type.Number)
		             .Select(t => new {
			             typeName = NamingConventions.Normalize(t.Name),
			             sharpType = ConvertToSharpNumeric(t.NumberType, t.NumberSize)
		             })
		             .ToDictionary(kv => kv.typeName, kv => kv.sharpType);
	}

	public static string ConvertToSharpNumeric(NumberType? numberType, long? size) {
		if (numberType == null) {
			throw new ArgumentNullException(nameof(numberType));
		}
		if (size == null) {
			throw new ArgumentNullException(nameof(size));
		}

		return numberType switch {
			NumberType.Float => size switch {
				32 => "float",
				64 => "double",
				_ => "float"
			},
			NumberType.UInt => size switch {
				8 => "byte",
				16 => "ushort",
				32 => "uint",
				64 => "ulong",
				_ => "uint"
			},
			NumberType.Int => size switch {
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
