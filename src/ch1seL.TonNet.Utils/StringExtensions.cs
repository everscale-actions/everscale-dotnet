using System;

namespace ch1seL.TonNet.Utils
{
    public static class StringExtensions
    {
        public static ulong HexToDec(this string hexValue)
        {
            return Convert.ToUInt64(hexValue, 16);
        }
    }
}