using EverscaleNet.Abstract;
using EverscaleNet.Client.Models;
using EverscaleNet.Models;
using Polly;
using Polly.Retry;
using Shouldly;

namespace TestingExample;

public class CalculatorInternalTests(IEverClient everClient, IEverPackageManager packageManager, IEverGiver giver)
	: IAsyncLifetime {
	private CalculatorInternal _calculator;
	private IMultisigAccount _multisig;

	public async ValueTask InitializeAsync() {
		_multisig = await CreateMultisig();
		_calculator = new CalculatorInternal(everClient, packageManager);
		await _calculator.Init(_multisig, new { owner_ = _multisig.Address });
		await _calculator.Deploy();
	}

	public async ValueTask DisposeAsync() {
		await _multisig.SubmitTransaction(giver.Address, 0, false, true, string.Empty);
	}

	private async Task<IMultisigAccount> CreateMultisig(decimal coins = 20m) {
		var multisig = new SafeMultisigAccount(everClient, packageManager);
		KeyPair keyPair = await everClient.Crypto.GenerateRandomSignKeys();
		await multisig.Init(keyPair);
		await giver.SendTransaction(multisig.Address, coins);
		await multisig.Deploy([keyPair.Public], 1, TimeSpan.FromHours(1));
		return multisig;
	}

	[Fact]
	public async Task BalancesIsGoodAfterDeployment() {
		decimal multisigBalance = await _multisig.GetBalance(TestContext.Current.CancellationToken);
		decimal calculatorBalance = await _calculator.GetBalance(TestContext.Current.CancellationToken);

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

		long result = await _calculator.GetSum(TestContext.Current.CancellationToken);
		decimal multisigBalance = await _multisig.GetBalance(TestContext.Current.CancellationToken);
		decimal calculatorBalance = await _calculator.GetBalance(TestContext.Current.CancellationToken);

		result.ShouldBe(55);
		calculatorBalance.ShouldBe(1M);
		multisigBalance.ShouldBeGreaterThan(8.5M);
	}

	[Fact]
	public async Task AnotherMultisigHasNoAccess() {
		IMultisigAccount anotherMultisig = await CreateMultisig();
		var calculatorWithAnotherMultisig = new CalculatorInternal(everClient, packageManager, _calculator.Address);
		await calculatorWithAnotherMultisig.Init(anotherMultisig, new { owner_ = _multisig.Address }, TestContext.Current.CancellationToken);

		await _calculator.Add(1, TestContext.Current.CancellationToken);
		Func<Task> act = () => calculatorWithAnotherMultisig.Add(2, TestContext.Current.CancellationToken);
		long result = await _calculator.GetSum(TestContext.Current.CancellationToken);

		var ex = await act.ShouldThrowAsync<EverClientException>();
		ex.Message.ShouldBe("Transaction aborted or failed");
		result.ShouldBe(1);
	}
}