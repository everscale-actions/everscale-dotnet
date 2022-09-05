namespace EverscaleNet.Utils;

/// <summary>
/// </summary>
public static class DecimalExtensions {
	/// <summary>
	///     Convert Coins to Nano 1Coin = 1000000000Nano
	/// </summary>
	/// <param name="coins"></param>
	/// <returns></returns>
	public static decimal CoinsToNano(this decimal coins) {
		return coins * 1_000_000_000m;
	}

	/// <summary>
	///     Convert Nano to Coins 1Coin = 1000000000Nano
	/// </summary>
	/// <param name="nano"></param>
	/// <returns></returns>
	public static decimal NanoToCoins(this decimal nano) {
		return nano / 1_000_000_000m;
	}

	/// <summary>
	///     Convert Nano to Coins 1Coin = 1000000000Nano
	/// </summary>
	/// <param name="nano"></param>
	/// <returns></returns>
	public static decimal NanoToCoins(this ulong nano) {
		return nano / 1_000_000_000m;
	}
}
