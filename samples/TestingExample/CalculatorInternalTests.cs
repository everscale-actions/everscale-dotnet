using EverscaleNet.Abstract;
using EverscaleNet.Client.Models;
using EverscaleNet.Models;
using Polly;
using Polly.Retry;
using Shouldly;

namespace TestingExample;

public class CalculatorInternalTests : IAsyncLifetime {
	private readonly IEverClient _everClient;
	private readonly IEverGiver _giver;
	private readonly IEverPackageManager _packageManager;
	private CalculatorInternal _calculator;
	private IMultisigAccount _multisig;

	public CalculatorInternalTests(IEverClient everClient, IEverPackageManager packageManager, IEverGiver giver) {
		_everClient = everClient;
		_packageManager = packageManager;
		_giver = giver;
	}

	public async Task InitializeAsync() {
		_multisig = await CreateMultisig();
		_calculator = new CalculatorInternal(_everClient, _packageManager);
		await _calculator.Init(_multisig, new { owner_ = _multisig.Address });
		await _calculator.Deploy();
	}

	public async Task DisposeAsync() {
		await _multisig.SubmitTransaction(_giver.Address, 0, false, true, string.Empty);
	}

	private async Task<IMultisigAccount> CreateMultisig(decimal coins = 20m) {
		var multisig = new SafeMultisigAccount(_everClient, _packageManager);
		KeyPair keyPair = await _everClient.Crypto.GenerateRandomSignKeys();
		await multisig.Init(keyPair);
		await _giver.SendTransaction(multisig.Address, coins);
		await multisig.Deploy([keyPair.Public], 1, TimeSpan.FromHours(1));
		return multisig;
	}

	[Fact]
	public async Task BalancesIsGoodAfterDeployment() {
		decimal multisigBalance = await _multisig.GetBalance();
		decimal calculatorBalance = await _calculator.GetBalance();

		multisigBalance.ShouldBeGreaterThan(8.9M);
		calculatorBalance.ShouldBeGreaterThan(0.99M);
	}

	[Fact]
	public async Task Add1TenTimes_Returns55() {
		const int maxRetryAttempts = 10;
		TimeSpan pauseBetweenFailures = TimeSpan.FromSeconds(1);

		await Parallel.ForEachAsync(
			Enumerable.Range(1, 10),
			async (i, token) =>
			{
				AsyncRetryPolicy retryPolicy =
					Policy.Handle<NoOutMessagesException>()
						.WaitAndRetryAsync(maxRetryAttempts, _ => pauseBetweenFailures);
				await retryPolicy.ExecuteAsync(async () => await _calculator.Add(i, token));
			});

		long result = await _calculator.GetSum();
		decimal multisigBalance = await _multisig.GetBalance();
		decimal calculatorBalance = await _calculator.GetBalance();

		result.ShouldBe(55);
		calculatorBalance.ShouldBe(1M);
		multisigBalance.ShouldBeGreaterThan(8.5M);
	}

	[Fact]
	public async Task AnotherMultisigHasNoAccess() {
		IMultisigAccount anotherMultisig = await CreateMultisig();
		var calculatorWithAnotherMultisig = new CalculatorInternal(_everClient, _packageManager, _calculator.Address);
		await calculatorWithAnotherMultisig.Init(anotherMultisig, new { owner_ = _multisig.Address });

		await _calculator.Add(1);
		Func<Task> act = () => calculatorWithAnotherMultisig.Add(2);
		long result = await _calculator.GetSum();

		var ex = await act.ShouldThrowAsync<EverClientException>();
		ex.Message.ShouldBe("Transaction aborted or failed");
		result.ShouldBe(1);
	}
}