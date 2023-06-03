namespace EverscaleNet.ClientGenerator.Helpers;

internal static class ResultExtensions {
	public static string GetMethodReturnType(this Result result) {
		if (result.GenericName != ResultGenericName.ClientResult) {
			throw new ArgumentOutOfRangeException(nameof(result.GenericName), result.GenericName, "Unsupported method result generic name");
		}
		if (result.Type != ApiType.Generic) {
			throw new ArgumentOutOfRangeException(nameof(Result.Type), result.Type, "Unsupported method result type");
		}
		if (result.GenericArgs.Length != 1) {
			throw new ArgumentException("Result should contains only one generic argument");
		}

		GenericArg genericArg = result.GenericArgs[0];

		return genericArg.Type switch {
			ApiType.None => null,
			ApiType.Ref => NamingConventions.Normalize(genericArg.RefName),
			_ => throw new ArgumentOutOfRangeException(nameof(ApiType), genericArg.Type, "Unsupported generic type")
		};
	}
}
