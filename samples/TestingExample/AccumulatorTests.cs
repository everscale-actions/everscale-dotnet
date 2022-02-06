namespace TestingExample;

public class AccumulatorTests : IClassFixture<FixtureWrapper>, IAsyncLifetime {
	private readonly IEverTestsFixture _fixture;
	private readonly ITestOutputHelper _output;
	private AccumulatorContract _accumulatorContract;
	private KeyPair _keyPair;

	public AccumulatorTests(FixtureWrapper fixtureWrapper, ITestOutputHelper output) {
		_fixture = fixtureWrapper.GetFixture();
		_output = output;
	}

	public async Task InitializeAsync() {
		await _fixture.Init(_output);
		_keyPair = await _fixture.Client.Crypto.GenerateRandomSignKeys();
		_accumulatorContract = new AccumulatorContract(_fixture.Client, _fixture.PackageManager, _keyPair);
		await _accumulatorContract.Init(_keyPair.Public);
		await _fixture.Giver.SendTransaction(_accumulatorContract.Address, 0.1M);
		await _accumulatorContract.Deploy();
	}

	public Task DisposeAsync() {
		return Task.CompletedTask;
	}

	[Fact]
	public async Task Add100_Returns100() {
		await _accumulatorContract.Add(100, _keyPair);

		long result = await _accumulatorContract.GetSum();

		result.Should().Be(100);
	}

	[Fact]
	public async Task Add1Then2_Returns3() {
		await _accumulatorContract.Add(100, _keyPair);
		await _accumulatorContract.Add(200, _keyPair);

		long result = await _accumulatorContract.GetSum();

		result.Should().Be(300);
	}

	[Fact]
	public async Task ParallelAdd10_Returns10() {
		await Parallel.ForEachAsync(
			Enumerable.Range(0, 9),
			async (i, token) => await _accumulatorContract.Add(i, _keyPair, token));

		long result = await _accumulatorContract.GetSum();

		result.Should().Be(36);
	}

	[Fact]
	public async Task Subtract100_ReturnsMinus100() {
		await _accumulatorContract.Subtract(100, _keyPair);

		long result = await _accumulatorContract.GetSum();

		result.Should().Be(-100);
	}
}
