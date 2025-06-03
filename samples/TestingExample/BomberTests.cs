using EverscaleNet.Abstract;
using EverscaleNet.Client.Models;
using EverscaleNet.Models;
using EverscaleNet.Utils;
using Shouldly;

namespace TestingExample;

public class BomberTests(IEverClient everClient, IEverPackageManager packageManager, IEverGiver giver) : IAsyncLifetime {
	private const decimal TopUpCoins = 2M;
	private const decimal SendEverCoins = 1M;

	private readonly IEverClient _everClient = everClient;
	private readonly IEverGiver _giver = giver;
	private readonly IEverPackageManager _packageManager = packageManager;
	private Bomber _bomber;
	private decimal _bomberDeployFees;
	private Sink _sink;
	private decimal _sinkDeployFees;

	public async Task InitializeAsync() {
		KeyPair keyPair = await _everClient.Crypto.GenerateRandomSignKeys();
		_bomber = new Bomber(_everClient, _packageManager);
		_sink = new Sink(_everClient, _packageManager);
		await Task.WhenAll(
			_bomber.Init(keyPair),
			_sink.Init(keyPair)
		);
		await Task.WhenAll(
			_giver.SendTransaction(_bomber.Address, TopUpCoins),
			_giver.SendTransaction(_sink.Address, TopUpCoins)
		);
		var bomberDeployTask = _bomber.Deploy();
		var sinkDeployTask = _sink.Deploy();
		await Task.WhenAll(bomberDeployTask, sinkDeployTask);
		_bomberDeployFees = bomberDeployTask.Result!.Fees.TotalAccountFees.NanoToCoins();
		_sinkDeployFees = sinkDeployTask.Result!.Fees.TotalAccountFees.NanoToCoins();
	}

	public Task DisposeAsync() {
		return Task.CompletedTask;
	}

	[Fact]
	public async Task DeployFeesTest() {
		(decimal bomberBalance, decimal sinkBalance) = await GetBalances();

		bomberBalance.ShouldBe(TopUpCoins - _bomberDeployFees);
		sinkBalance.ShouldBe(TopUpCoins - _sinkDeployFees);
	}

	[Fact]
	public async Task TestSend0Test() {
		decimal sinkBalanceBefore = await _sink.GetBalance();

		ResultOfProcessMessage result = await _bomber.TestSend0(_sink.Address);
		decimal sinkBalanceAfter = await _sink.GetBalance();
		decimal sinkBalanceDiff = sinkBalanceAfter - sinkBalanceBefore;

		result.Fees.TotalAccountFees.NanoToCoins().ShouldBeLessThan(0.011M);
		sinkBalanceDiff.ShouldBeInRange(SendEverCoins - 0.006M, SendEverCoins);
	}

	[Fact]
	public async Task TestSend1Test() {
		decimal sinkBalanceBefore = await _sink.GetBalance();

		ResultOfProcessMessage result = await _bomber.TestSend1(_sink.Address);
		decimal sinkBalanceAfter = await _sink.GetBalance();
		decimal sinkBalanceDiff = sinkBalanceAfter - sinkBalanceBefore;

		result.Fees.TotalAccountFees.NanoToCoins().ShouldBeLessThan(0.01M);
		sinkBalanceDiff.ShouldBeInRange(SendEverCoins - 0.005M, SendEverCoins);
	}

	[Fact]
	public async Task TestSend128Test() {
		decimal sinkBalanceBefore = await _sink.GetBalance();

		await _bomber.TestSend128(_sink.Address);

		(decimal bomberBalanceAfter, decimal sinkBalanceAfter) = await GetBalances();
		decimal sinkBalanceDiff = sinkBalanceAfter - sinkBalanceBefore;

		bomberBalanceAfter.ShouldBe(0M);
		sinkBalanceDiff.ShouldBeInRange(TopUpCoins - 0.04M, TopUpCoins);
	}

	[Fact]
	public async Task TestSend160Test() {
		decimal sinkBalanceBefore = await _sink.GetBalance();

		await _bomber.TestSend160(_sink.Address);
		var bomberAccountTypeTask = _bomber.GetAccountType();
		var sinkBalanceAfterTask = _sink.GetBalance();
		await Task.WhenAll(bomberAccountTypeTask, sinkBalanceAfterTask);
		AccountType bomberAccountType = await bomberAccountTypeTask;
		decimal sinkBalanceAfter = await sinkBalanceAfterTask;

		bomberAccountType.ShouldBe(AccountType.NonExist);
		(sinkBalanceAfter - sinkBalanceBefore).ShouldBeInRange(TopUpCoins - 0.04M, TopUpCoins);
	}

	private async Task<(decimal bomber, decimal sink)> GetBalances() {
		var bomberBalanceTask = _bomber.GetBalance();
		var sinkBalanceTask = _sink.GetBalance();

		await Task.WhenAll(bomberBalanceTask, sinkBalanceTask);

		return (bomberBalanceTask.Result, sinkBalanceTask.Result);
	}
}