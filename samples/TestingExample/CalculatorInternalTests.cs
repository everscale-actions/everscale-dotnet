using EverscaleNet.Exceptions;
using EverscaleNet.WebClient.PackageManager;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;

namespace TestingExample;

public class CalculatorInternalTests : IClassFixture<FixtureWrapper>, IAsyncLifetime {
	private const string MultisigPackageUrl = "https://raw.githubusercontent.com/EverSurf/contracts/414557cbb62e6bd69b4793db005799dfb4e59793/multisig2/build/";
	private readonly IEverTestsFixture _fixture;
	private readonly HttpClient _httpClient;
	private readonly ITestOutputHelper _output;
	private CalculatorInternalAccount _calculator;
	private SafeMultisigAccount _multisig;
	private WebPackageManager _multisigPackage;

	public CalculatorInternalTests(FixtureWrapper fixtureWrapper, ITestOutputHelper output) {
		_fixture = fixtureWrapper.GetFixture();
		_output = output;
		_httpClient = new HttpClient();
	}

	public async Task InitializeAsync() {
		await _fixture.Init(_output);
		_multisigPackage = new WebPackageManager(_httpClient, new OptionsWrapper<PackageManagerOptions>(new PackageManagerOptions { PackagesPath = MultisigPackageUrl }));
		_multisig = await CreateMultisig();
		_calculator = new CalculatorInternalAccount(_fixture.Client, _fixture.PackageManager, _multisig);
		await _calculator.Init(new { owner_ = _multisig.Address });
		await _calculator.Deploy();
	}

	public async Task DisposeAsync() {
		await _multisig.SendTransaction(_fixture.Giver.Address, 0, true, 128, string.Empty);
		_httpClient?.Dispose();
	}

	private async Task<SafeMultisigAccount> CreateMultisig() {
		KeyPair keyPair = await _fixture.Client.Crypto.GenerateRandomSignKeys();
		var multisig = new SafeMultisigAccount(_fixture.Client, _multisigPackage, keyPair);
		await multisig.InitByPublicKey(keyPair.Public);
		await _fixture.Giver.SendTransaction(multisig.Address, 20M);
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
		SafeMultisigAccount anotherMultisig = await CreateMultisig();
		var calculatorWithAnotherMultisig = new CalculatorInternalAccount(_fixture.Client, _fixture.PackageManager, anotherMultisig, _calculator.Address);

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
