using EverscaleNet.Exceptions;
using EverscaleNet.TestSuite;
using Polly;
using Polly.Retry;

namespace TestingExample;

public class CalculatorInternalTests : IAsyncLifetime {
	private readonly IEverClient _everClient;
	private readonly IEverGiver _giver;
	private readonly IEverPackageManager _packageManager;
	private CalculatorInternalAccount _calculator;
	private IMultisigAccount _multisig;

	public CalculatorInternalTests(IEverClient everClient, IEverPackageManager packageManager, IEverGiver giver) {
		_everClient = everClient;
		_packageManager = packageManager;
		_giver = giver;
	}

	public async Task InitializeAsync() {
		_multisig = await CreateMultisig();
		_calculator = new CalculatorInternalAccount(_everClient, _packageManager, _multisig);
		await _calculator.Init(initialData: new { owner_ = _multisig.Address });
		await _calculator.Deploy();
	}

	public async Task DisposeAsync() {
		await _multisig.SendTransaction(_giver.Address, 0, true, 128, string.Empty);
	}

	private async Task<IMultisigAccount> CreateMultisig(decimal coins = 20m) {
		KeyPair keyPair = await _everClient.Crypto.GenerateRandomSignKeys();
		var multisig = new SafeMultisigAccount(_everClient, _packageManager, keyPair);
		await multisig.Init(keyPair.Public);
		await _giver.SendTransaction(multisig.Address, coins);
		await multisig.Deploy(new[] { keyPair.Public }, 1, TimeSpan.FromHours(1));
		return multisig;
	}

	[Fact]
	public async Task BalancesIsGoodAfterDeployment() {
		decimal multisigBalance = await _multisig.GetBalance();
		decimal calculatorBalance = await _calculator.GetBalance();

		multisigBalance.Should().BeGreaterThan(8.9M);
		calculatorBalance.Should().BeGreaterThan(0.99M);
	}

	[Fact]
	public async Task Add1TenTimes_Returns55() {
		const int maxRetryAttempts = 5;
		TimeSpan pauseBetweenFailures = TimeSpan.FromSeconds(1);

		await Parallel.ForEachAsync(
			Enumerable.Range(1, 10),
			async (i, token) => {
				AsyncRetryPolicy retryPolicy =
					Policy.Handle<NoOutMessagesException>()
					      .WaitAndRetryAsync(maxRetryAttempts, _ => pauseBetweenFailures);
				await retryPolicy.ExecuteAsync(async () => await _calculator.Add(i, token));
			});

		long result = await _calculator.GetSum();
		decimal multisigBalance = await _multisig.GetBalance();
		decimal calculatorBalance = await _calculator.GetBalance();

		result.Should().Be(55);
		calculatorBalance.Should().Be(1M);
		multisigBalance.Should().BeGreaterThan(8.5M);
	}

	[Fact]
	public async Task AnotherMultisigHasNoAccess() {
		IMultisigAccount anotherMultisig = await CreateMultisig();
		var calculatorWithAnotherMultisig = new CalculatorInternalAccount(_everClient, _packageManager, anotherMultisig, _calculator.Address);

		await _calculator.Add(1);
		Func<Task> act = () => calculatorWithAnotherMultisig.Add(2);
		long result = await _calculator.GetSum();

		await act.Should()
		         .ThrowAsync<EverClientException>()
		         .Where(e => e.Code == 103)
		         .WithMessage("Transaction aborted or failed");
		result.Should().Be(1);
	}
}
