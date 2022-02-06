namespace TestingExample;

public class BomberTests : IClassFixture<FixtureWrapper>, IAsyncLifetime {
	private const decimal TopUpCoins = 2M;
	private const decimal SendEverCoins = 1M;
	private readonly IEverTestsFixture _fixture;
	private readonly ITestOutputHelper _output;
	private BomberContract _bomberContract;
	private decimal _bomberDeployFees;
	private SinkContract _sinkContract;
	private decimal _sinkDeployFees;

	public BomberTests(FixtureWrapper fixtureWrapper, ITestOutputHelper output) {
		_fixture = fixtureWrapper.GetFixture();
		_output = output;
	}

	public async Task InitializeAsync() {
		await _fixture.Init(_output);
		KeyPair keyPair = await _fixture.Client.Crypto.GenerateRandomSignKeys();
		_bomberContract = new BomberContract(_fixture.Client, _fixture.PackageManager, keyPair);
		_sinkContract = new SinkContract(_fixture.Client, _fixture.PackageManager, keyPair);
		await Task.WhenAll(_bomberContract.Init(), _sinkContract.Init());
		await Task.WhenAll(_fixture.Giver.SendTransaction(_bomberContract.Address, TopUpCoins), _fixture.Giver.SendTransaction(_sinkContract.Address, TopUpCoins));
		Task<TransactionFees> bomberDeployTask = _bomberContract.Deploy();
		Task<TransactionFees> sinkDeployTask = _sinkContract.Deploy();
		await Task.WhenAll(bomberDeployTask, sinkDeployTask);
		_bomberDeployFees = bomberDeployTask.Result!.TotalAccountFees.NanoToCoins();
		_sinkDeployFees = sinkDeployTask.Result!.TotalAccountFees.NanoToCoins();
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
		decimal sinkBalanceBefore = await _sinkContract.GetBalance();

		ProcessAndWaitInternalMessagesResult result = await _bomberContract.TestSend0(_sinkContract.Address);
		decimal sinkBalanceAfter = await _sinkContract.GetBalance();
		decimal sinkBalanceDiff = sinkBalanceAfter - sinkBalanceBefore;

		result.ChildValue.NanoToCoins().Should().BeLessThan(SendEverCoins);
		sinkBalanceDiff.Should().BeInRange(SendEverCoins - 0.01M, SendEverCoins);
	}

	[Fact]
	public async Task TestSend1Test() {
		decimal sinkBalanceBefore = await _sinkContract.GetBalance();

		ProcessAndWaitInternalMessagesResult result = await _bomberContract.TestSend1(_sinkContract.Address);
		decimal sinkBalanceAfter = await _sinkContract.GetBalance();

		result.ChildValue.NanoToCoins().Should().Be(SendEverCoins);
		(sinkBalanceAfter - sinkBalanceBefore).Should().BeInRange(SendEverCoins - 0.01M, SendEverCoins);
	}

	[Fact]
	public async Task TestSend128Test() {
		decimal sinkBalanceBefore = await _sinkContract.GetBalance();

		await _bomberContract.TestSend128(_sinkContract.Address);

		(decimal bomberBalanceAfter, decimal sinkBalanceAfter) = await GetBalances();

		bomberBalanceAfter.Should().Be(0M);
		(sinkBalanceAfter - sinkBalanceBefore).Should().BeInRange(TopUpCoins - 0.1M, TopUpCoins);
	}

	[Fact]
	public async Task TestSend160Test() {
		decimal sinkBalanceBefore = await _sinkContract.GetBalance();

		await _bomberContract.TestSend160(_sinkContract.Address);
		Task<AccountType?> bomberAccountTypeTask = _bomberContract.GetAccountType();
		Task<decimal> sinkBalanceAfterTask = _sinkContract.GetBalance();
		await Task.WhenAll(bomberAccountTypeTask, sinkBalanceAfterTask);

		bomberAccountTypeTask.Result.Should().Be(AccountType.NonExist);
		(sinkBalanceAfterTask.Result - sinkBalanceBefore).Should().BeInRange(TopUpCoins - 0.1M, TopUpCoins);
	}

	private async Task<(decimal bomber, decimal sink)> GetBalances() {
		Task<decimal> bomberBalanceTask = _bomberContract.GetBalance();
		Task<decimal> sinkBalanceTask = _sinkContract.GetBalance();

		await Task.WhenAll(bomberBalanceTask, sinkBalanceTask);

		return (bomberBalanceTask.Result, sinkBalanceTask.Result);
	}
}
