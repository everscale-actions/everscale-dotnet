using System;
using System.Globalization;
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
	///     Convert balance in DEC to decimal of coins
	/// </summary>
	/// <param name="balance"></param>
	/// <returns></returns>
	public static decimal DecBalanceToCoins(this string balance) {
		decimal dec = decimal.Parse(balance, NumberStyles.Number);
		return dec / 1_000_000_000m;
	}

	/// <summary>
	///     Convert string to hex
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public static string ToHexString(this string input) {
		return BitConverter.ToString(Encoding.Default.GetBytes(input)).Replace("-", string.Empty);
	}
}
