using System;
using System.Globalization;
using System.Text;

namespace EverscaleNet.Utils;

public static class StringExtensions {
	public static ulong HexToDec(this string hexValue) {
		return Convert.ToUInt64(hexValue, 16);
	}

	public static decimal HexToBalance(this string balance) {
		decimal bigInt = decimal.Parse(balance, NumberStyles.HexNumber);
		return bigInt / 1_000_000_000m;
	}

	public static decimal DecToBalance(this string balance) {
		decimal bigInt = decimal.Parse(balance, NumberStyles.Number);
		return bigInt / 1_000_000_000m;
	}

	public static string ToHexString(this string input) {
		return BitConverter.ToString(Encoding.Default.GetBytes(input)).Replace("-", string.Empty);
	}
}
