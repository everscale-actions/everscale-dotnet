using System.Linq;

namespace EverscaleNet.ClientGenerator.Helpers;

internal static class StringUtils {
	public static string GetGenericParametersDeclaration(params string[] genericParameters) {
		genericParameters = genericParameters.Where(t => t != null).ToArray();

		return genericParameters.Length != 0
			       ? $"<{string.Join(", ", genericParameters.Where(t => t != null))}>"
			       : null;
	}

	public static string EscapeReserved(string name) {
		return name switch {
			"params" => $"@{name}",
			_ => name
		};
	}
}