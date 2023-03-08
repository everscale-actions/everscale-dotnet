namespace TestingExample;

public class BomberTests : IClassFixture<FixtureWrapper>, IAsyncLifetime {
	private const decimal TopUpCoins = 2M;
	private const decimal SendEverCoins = 1M;
	private readonly IEverTestsFixture _fixture;
	private readonly ITestOutputHelper _output;
	private BomberAccount _bomberAccount;
	private decimal _bomberDeployFees;
	private SinkAccount _sinkAccount;
	private decimal _sinkDeployFees;

	public BomberTests(FixtureWrapper fixtureWrapper, ITestOutputHelper output) {
		_fixture = fixtureWrapper.GetFixture();
		_output = output;
	}

	public async Task InitializeAsync() {
		await _fixture.Init(_output);
		KeyPair keyPair = await _fixture.Client.Crypto.GenerateRandomSignKeys();
		_bomberAccount = new BomberAccount(_fixture.Client, _fixture.PackageManager, keyPair);
		_sinkAccount = new SinkAccount(_fixture.Client, _fixture.PackageManager, keyPair);
		await Task.WhenAll(
			_bomberAccount.InitByPublicKey(keyPair.Public),
			_sinkAccount.InitByPublicKey(keyPair.Public)
		);
		await Task.WhenAll(
			_fixture.Giver.SendTransaction(_bomberAccount.Address, TopUpCoins),
			_fixture.Giver.SendTransaction(_sinkAccount.Address, TopUpCoins)
		);
		Task<ResultOfProcessMessage> bomberDeployTask = _bomberAccount.Deploy();
		Task<ResultOfProcessMessage> sinkDeployTask = _sinkAccount.Deploy();
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

		bomberBalance.Should().Be(TopUpCoins - _bomberDeployFees);
		sinkBalance.Should().Be(TopUpCoins - _sinkDeployFees);
	}

	[Fact]
	public async Task TestSend0Test() {
		decimal sinkBalanceBefore = await _sinkAccount.GetBalance();

		ResultOfProcessMessage result = await _bomberAccount.TestSend0(_sinkAccount.Address);
		decimal sinkBalanceAfter = await _sinkAccount.GetBalance();
		decimal sinkBalanceDiff = sinkBalanceAfter - sinkBalanceBefore;

		result.Fees.TotalAccountFees.NanoToCoins().Should().BeLessThan(0.011M);
		sinkBalanceDiff.Should().BeInRange(SendEverCoins - 0.006M, SendEverCoins);
	}

	[Fact]
	public async Task TestSend1Test() {
		decimal sinkBalanceBefore = await _sinkAccount.GetBalance();

		ResultOfProcessMessage result = await _bomberAccount.TestSend1(_sinkAccount.Address);
		decimal sinkBalanceAfter = await _sinkAccount.GetBalance();
		decimal sinkBalanceDiff = sinkBalanceAfter - sinkBalanceBefore;

		result.Fees.TotalAccountFees.NanoToCoins().Should().BeLessThan(0.01M);
		sinkBalanceDiff.Should().BeInRange(SendEverCoins - 0.005M, SendEverCoins);
	}

	[Fact]
	public async Task TestSend128Test() {
		decimal sinkBalanceBefore = await _sinkAccount.GetBalance();

		await _bomberAccount.TestSend128(_sinkAccount.Address);

		(decimal bomberBalanceAfter, decimal sinkBalanceAfter) = await GetBalances();
		decimal sinkBalanceDiff = sinkBalanceAfter - sinkBalanceBefore;

		bomberBalanceAfter.Should().Be(0M);
		sinkBalanceDiff.Should().BeInRange(TopUpCoins - 0.04M, TopUpCoins);
	}

	[Fact]
	public async Task TestSend160Test() {
		decimal sinkBalanceBefore = await _sinkAccount.GetBalance();

		await _bomberAccount.TestSend160(_sinkAccount.Address);
		Task<AccountType?> bomberAccountTypeTask = _bomberAccount.GetAccountType();
		Task<decimal> sinkBalanceAfterTask = _sinkAccount.GetBalance();
		await Task.WhenAll(bomberAccountTypeTask, sinkBalanceAfterTask);

		bomberAccountTypeTask.Result.Should().Be(AccountType.NonExist);
		(sinkBalanceAfterTask.Result - sinkBalanceBefore).Should().BeInRange(TopUpCoins - 0.04M, TopUpCoins);
	}

	private async Task<(decimal bomber, decimal sink)> GetBalances() {
		Task<decimal> bomberBalanceTask = _bomberAccount.GetBalance();
		Task<decimal> sinkBalanceTask = _sinkAccount.GetBalance();

		await Task.WhenAll(bomberBalanceTask, sinkBalanceTask);

		return (bomberBalanceTask.Result, sinkBalanceTask.Result);
	}
}
