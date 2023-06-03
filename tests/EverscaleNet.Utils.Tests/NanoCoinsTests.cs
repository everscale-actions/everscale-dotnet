namespace EverscaleNet.Utils.Tests;

public class NanoCoinsTests {
	public static IEnumerable<object[]> NanoToCoinsTestData =>
		new List<object[]> {
			new object[] { 0, 0 },
			new object[] { 1, .000_000_001 },
			new object[] { 100, .000_000_1 },
			new object[] { 100_000_000, 0.1 },
			new object[] { 100_000_000_000, 100 },
			new object[] { 12_000_000_000_000, 12_000 },
			new object[] { 12_000_000_000_000_000_000, 12_000_000_000 },
			new object[] { 12_000_000_000_000_000_000_000_000m, 12_000_000_000_000_000 },
			new object[] { 1, 0.000_1, 4 },
			new object[] { 1, 0.1, 1 },
			new object[] { 1, 1, 0 }
		};

	public static IEnumerable<object[]> CoinsToNanoTestData =>
		new List<object[]> {
			new object[] { .000_000_1m, 100 },
			new object[] { 0.1, 100_000_000 },
			new object[] { 100, 100_000_000_000 },
			new object[] { 12_000, 12_000_000_000_000 },
			new object[] { 0, 0, 4 },
			new object[] { 1, 1, 0 },
			new object[] { 1, 1_000_000_000_000_000_000, 18 },
			new object[] { 1, 10_000, 4 }
		};

	[Theory]
	[MemberData(nameof(NanoToCoinsTestData))]
	public void NanoToCoins(BigInteger nano, decimal expectedCoins, byte decimals = 9) {
		decimal coins = nano.NanoToCoins(decimals);
		coins.Should().Be(expectedCoins);
	}

	[Theory]
	[MemberData(nameof(CoinsToNanoTestData))]
	public void CoinsToNano(decimal coins, BigInteger expectedNano, byte decimals = 9) {
		BigInteger nano = coins.CoinsToNano(decimals);
		nano.Should().Be(expectedNano);
	}
}
