using EverscaleNet.WebClient.PackageManager;
using Microsoft.Extensions.Options;

namespace TestingExample;

public class CalculatorWithMultisigTests : IClassFixture<FixtureWrapper>, IAsyncLifetime {
	private readonly IEverTestsFixture _fixture;
	private readonly ITestOutputHelper _output;
	private SafeMultisigAccount _multisig;
	private readonly HttpClient _httpClient;
	private CalculatorAccount _calculator;
	private WebPackageManager _multisigPackage;
	private const string MultisigPackageUrl = "https://raw.githubusercontent.com/EverSurf/contracts/414557cbb62e6bd69b4793db005799dfb4e59793/multisig2/build/";

	public CalculatorWithMultisigTests(FixtureWrapper fixtureWrapper, ITestOutputHelper output) {
		_fixture = fixtureWrapper.GetFixture();
		_output = output;
		_httpClient = new HttpClient();
	}

	public async Task InitializeAsync() {
		await _fixture.Init(_output);
		_multisigPackage = new WebPackageManager(_httpClient, new OptionsWrapper<WebPackageManagerOptions>(new WebPackageManagerOptions { PackagesPath = MultisigPackageUrl }));
		_multisig = await CreateMultisig();
		_calculator = new CalculatorAccount(_fixture.Client, _fixture.PackageManager, _multisig);
		await _calculator.Init(new { owner_ = _multisig.Address });
		await _calculator.Deploy();
	}

	private async Task<SafeMultisigAccount> CreateMultisig() {
		KeyPair keyPair = await _fixture.Client.Crypto.GenerateRandomSignKeys();
		var multisig = new SafeMultisigAccount(_fixture.Client, _multisigPackage, keyPair);
		await multisig.InitByPublicKey(keyPair.Public);
		await _fixture.Giver.SendTransaction(multisig.Address, 10M);
		await multisig.Deploy(new[] { keyPair.Public }, 1, TimeSpan.FromHours(1));
		return multisig;
	}

	public Task DisposeAsync() {
		_httpClient?.Dispose();
		return Task.CompletedTask;
	}

	[Fact]
	public async Task EnsureThatBalancesIsGoodAfterDeployment() {
		decimal multisigBalance = await _multisig.GetBalance();
		decimal accumulatorBalance = await _calculator.GetBalance();

		multisigBalance.Should().BeGreaterThan(8.9M);
		accumulatorBalance.Should().BeGreaterThan(0.99M);
	}

	[Fact]
	public async Task Add1Then2_Returns3() {
		await _calculator.Add(1);
		await _calculator.Add(2);

		long result = await _calculator.GetSum();

		result.Should().Be(3);
	}

	[Fact]
	public async Task AnotherMultisigHasNoAccess() {
		SafeMultisigAccount anotherMultisig = await CreateMultisig();
		var accumulatorWithAnotherMultisig = new CalculatorAccount(_fixture.Client, _fixture.PackageManager, anotherMultisig, _calculator.Address);

		await _calculator.Add(1);
		await accumulatorWithAnotherMultisig.Add(2);
		long result = await _calculator.GetSum();

		result.Should().Be(1);
	}
}
