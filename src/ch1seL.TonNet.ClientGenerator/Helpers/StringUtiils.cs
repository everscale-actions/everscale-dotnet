using System;

namespace ch1seL.TonNet.ClientGenerator.Helpers
{
    internal static class StringUtils
    {
        public static string ToCamelCase(string str)
        {
            return $"{char.ToUpper(str[0])}{str.Substring(1)}";
        }

        public static string TypeFromRef(string refName)
        {
            if (!refName.Contains("."))
            {
                throw new ArgumentException("Should contains . separator", refName);
            }
                
            return NamingConventions.CommonFormatterFormatter(refName.Split(".")[1]);
        }
    }
}