using System.Text.RegularExpressions;

namespace EverscaleNet.ClientGenerator.Helpers;

public static class NamingConventions {
	private static readonly Regex SnackCaseRegex = new(@"([_\s]|^)(?'firstChar'\w)", RegexOptions.Compiled);
	private static readonly Regex FieldRegex = new(@"(^)(?'firstChar'\w)", RegexOptions.Compiled);

	public static string Normalize(string name) {
		if (name.Contains('.')) {
			name = name.Split(".")[1];
		}

		name = StringUtils.EscapeReserved(name);
		// I prefer Request, Response postfix instead ParamsOf and ResultOf prefixes but well, then maybe for now just leave it.
		// name = ResultOfReplacer.Replace(name, match => $"{match.Groups["name"].Value}Response");
		// name = ParamsOfReplacer.Replace(name, match => $"{match.Groups["name"].Value}Request");
		name = SnackCaseRegex.Replace(name, FirstCharToUpper);

		return name;
	}

	public static string ToFieldName(string name) {
		name = Normalize(name);

		return FieldRegex.Replace(name, match => $"_{match.Groups["firstChar"].Value.ToLowerInvariant()}");
	}

	public static string ToVarName(string name) {
		name = Normalize(name);

		return FieldRegex.Replace(name, match => $"{match.Groups["firstChar"].Value.ToLowerInvariant()}");
	}

	public static string ToInterfaceName(string name) {
		name = Normalize(name);

		return $"I{name}";
	}

	public static string EventFormatter(string moduleName) {
		return Normalize($"{moduleName}_event");
	}

	private static string FirstCharToUpper(Match match) {
		return $"{match.Groups["firstChar"].Value.ToUpperInvariant()}";
	}
}
