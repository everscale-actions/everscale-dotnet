using System.Globalization;
using System.Numerics;
using System.Text;

namespace EverscaleNet.Utils;

/// <summary>
/// </summary>
public static class StringExtensions {
	/// <summary>
	///     Convert hex string to ulong
	/// </summary>
	/// <param name="hexValue"></param>
	/// <returns></returns>
	public static ulong HexToDec(this string hexValue) {
		return Convert.ToUInt64(hexValue, 16);
	}

	/// <summary>
	///     Convert string to hex
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public static string ToHexString(this string input) {
		return BitConverter.ToString(Encoding.Default.GetBytes(input)).Replace("-", string.Empty);
	}

	/// <summary>
	///     Convert decimal nano to coins
	/// </summary>
	/// <param name="nano"></param>
	/// <returns></returns>
	public static decimal NanoToCoins(this string nano) {
		BigInteger nanoDecimal = BigInteger.Parse(nano, NumberStyles.Integer);
		return nanoDecimal.NanoToCoins();
	}
}
