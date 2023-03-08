using System.Text.Json;

namespace TestingExample;

public class CalculatorTests : IClassFixture<FixtureWrapper>, IAsyncLifetime {
	private readonly IEverTestsFixture _fixture;
	private readonly ITestOutputHelper _output;
	private CalculatorAccount _calculator;
	private KeyPair _keyPair;

	public CalculatorTests(FixtureWrapper fixtureWrapper, ITestOutputHelper output) {
		_fixture = fixtureWrapper.GetFixture();
		_output = output;
	}

	public async Task InitializeAsync() {
		await _fixture.Init(_output);
		_keyPair = await _fixture.Client.Crypto.GenerateRandomSignKeys();
		_calculator = new CalculatorAccount(_fixture.Client, _fixture.PackageManager, _keyPair);
		await _calculator.InitByPublicKey(_keyPair.Public);
		await _fixture.Giver.SendTransaction(_calculator.Address, 0.1M);
		await _calculator.Deploy();
	}

	public Task DisposeAsync() {
		return Task.CompletedTask;
	}

	[Fact]
	public async Task Add100_Returns100() {
		await _calculator.Add(100);

		long result = await _calculator.GetSum();

		result.Should().Be(100);
	}

	[Fact]
	public async Task Add1Then2_Returns3() {
		await _calculator.Add(100);
		await _calculator.Add(200);

		long result = await _calculator.GetSum();

		result.Should().Be(300);
	}

	[Fact]
	public async Task ParallelAdd10_Returns10() {
		await Parallel.ForEachAsync(
			Enumerable.Range(0, 9),
			async (i, token) => await _calculator.Add(i, token));

		long result = await _calculator.GetSum();

		result.Should().Be(36);
	}

	[Fact]
	public async Task Subtract100_ReturnsMinus100() {
		await _calculator.Subtract(100);

		long result = await _calculator.GetSum();

		result.Should().Be(-100);
	}

	[Fact]
	public async Task AnotherPubkeyHasNoAccess() {
		KeyPair keyPair = await _fixture.Client.Crypto.GenerateRandomSignKeys();
		var accumulatorAccount = new CalculatorAccount(_fixture.Client, _fixture.PackageManager, keyPair);
		await accumulatorAccount.InitByPublicKey(_keyPair.Public);

		Func<Task> act = () => accumulatorAccount.Add(1);

		await act.Should()
		         .ThrowAsync<EverClientException>()
		         .Where(exceptionExpression: e => ((JsonElement)e.Data["exit_code"]).GetInt32() == 101);
	}
}
