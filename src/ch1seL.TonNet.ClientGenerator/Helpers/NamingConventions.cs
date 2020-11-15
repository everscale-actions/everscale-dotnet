using System.Text.RegularExpressions;

namespace ch1seL.TonNet.ClientGenerator.Helpers
{
    public static class NamingConventions
    {
        private static readonly Regex ResultOfReplacer = new(@"ResultOf(?'name'\w+)", RegexOptions.Compiled);
        private static readonly Regex ParamsOfReplacer = new(@"ParamsOf(?'name'\w+)", RegexOptions.Compiled);
        private static readonly Regex SnackCaseRegex = new(@"([_\s]|^)(?'firstChar'\w)", RegexOptions.Compiled);

        public static string Formatter(string name)
        {
            if (name.Contains(".")) name = name.Split(".")[1];

            name = StringUtils.EscapeReserved(name);
            name = ResultOfReplacer.Replace(name, match => $"{match.Groups["name"].Value}Response");
            name = ParamsOfReplacer.Replace(name, match => $"{match.Groups["name"].Value}Request");
            name = SnackCaseRegex.Replace(name, FirstCharToUpper);

            return name;
        }

        private static string FirstCharToUpper(Match match)
        {
            return $"{match.Groups["firstChar"].Value.ToUpperInvariant()}";
        }

        public static string EventFormatter(string moduleName)
        {
            return Formatter($"{moduleName}_event");
        }
    }
}