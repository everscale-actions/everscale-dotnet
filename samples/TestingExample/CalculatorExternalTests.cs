namespace TestingExample;

public class CalculatorExternalTests : IAsyncLifetime {
	private readonly IEverClient _everClient;
	private readonly IEverGiver _giver;
	private readonly IEverPackageManager _packageManager;
	private CalculatorExternal _calculator;
	private KeyPair _keyPair;

	public CalculatorExternalTests(IEverClient everClient, IEverPackageManager packageManager, IEverGiver giver) {
		_everClient = everClient;
		_packageManager = packageManager;
		_giver = giver;
	}

	public async Task InitializeAsync() {
		_keyPair = await _everClient.Crypto.GenerateRandomSignKeys();
		_calculator = new CalculatorExternal(_everClient, _packageManager);
		await _calculator.Init(_keyPair);
		await _giver.SendTransaction(_calculator.Address, 10m);
		await _calculator.Deploy();
	}

	public Task DisposeAsync() {
		return Task.CompletedTask;
	}

	[Fact]
	public async Task Add1_Returns1() {
		await _calculator.Add(1);

		long result = await _calculator.GetSum();

		result.Should().Be(1);
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
			Enumerable.Range(1, 10),
			async (i, token) => await _calculator.Add(i, token));

		long result = await _calculator.GetSum();

		result.Should().Be(55);
	}

	[Fact]
	public async Task Subtract100_ReturnsMinus100() {
		await _calculator.Subtract(100);

		long result = await _calculator.GetSum();

		result.Should().Be(-100);
	}

	[Fact]
	public async Task AnotherPubkeyHasNoAccess() {
		KeyPair keyPair = await _everClient.Crypto.GenerateRandomSignKeys();
		var calculatorAccount = new CalculatorExternal(_everClient, _packageManager);
		await calculatorAccount.Init(_keyPair.Public);
		await calculatorAccount.Init(keyPair); // Reinit with another signer

		Func<Task> act = () => calculatorAccount.Add(1);

		await act.Should()
		         .ThrowAsync<EverClientException>()
		         .Where(e => ((JsonElement)e.Data["exit_code"]).GetInt32() == 101);
	}
}
