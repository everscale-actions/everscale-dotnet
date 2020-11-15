using System.Text.RegularExpressions;

namespace ch1seL.TonNet.ClientGenerator.Helpers
{
    public static class NamingConventions
    {
        private static readonly Regex ResultOfReplacer = new(@"ResultOf(?'name'\w+)", RegexOptions.Compiled);
        private static readonly Regex ParamsOfReplacer = new(@"ParamsOf(?'name'\w+)", RegexOptions.Compiled);
        private static readonly Regex SnackCaseRegex = new(@"(_|^)(?'firstChar'\w)", RegexOptions.Compiled);

        public static string CommonFormatterFormatter(string name)
        {
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
            return SnackCaseRegex.Replace($"{moduleName}_event", FirstCharToUpper);
        }

        public static string EscapeReserved(string name)
        {
            return name switch
            {
                "params" => $"@{name}",
                _ => name
            };
        }
    }
}