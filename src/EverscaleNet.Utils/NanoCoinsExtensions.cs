using System.Globalization;
using System.Numerics;

namespace EverscaleNet.Utils;

/// <summary>
/// </summary>
public static class NanoCoinsExtensions {
	/// <summary>
	///     Convert Coins to Nano 1Coin = 1000000000Nano
	/// </summary>
	/// <param name="coins"></param>
	/// <param name="decimals"></param>
	/// <returns></returns>
	public static BigInteger CoinsToNano(this decimal coins, byte decimals = 9) {
		var str = coins.ToString($"F{decimals}", CultureInfo.InvariantCulture);
		int pointIndex = str.IndexOf('.');
		return pointIndex >= 0
			       ? BigInteger.Parse(str.Remove(pointIndex, 1), CultureInfo.InvariantCulture)
			       : BigInteger.Parse(str, CultureInfo.InvariantCulture);
	}

	/// <summary>
	///     Convert Nano to Coins 1Coin = 1000000000Nano
	/// </summary>
	/// <param name="nano"></param>
	/// <param name="decimals"></param>
	/// <returns></returns>
	public static decimal NanoToCoins(this BigInteger nano, byte decimals = 9) {
		var str = nano.ToString($"D{decimals}");
		int pointIndex = str.Length - decimals;
		string shiftedString = (pointIndex is 0 ? "0" : null) + str.Insert(pointIndex, ".");
		return decimal.Parse(shiftedString, CultureInfo.InvariantCulture);
	}

	/// <summary>
	///     Convert Nano to Coins 1Coin = 1000000000Nano
	/// </summary>
	/// <param name="nano"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static decimal NanoToCoins<T>(this T nano)
		where T : struct, IComparable, IComparable<T>,
		IConvertible, IEquatable<T>, IFormattable {
		return BigInteger.Parse(nano.ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture).NanoToCoins();
	}
}
