using Shouldly;

namespace EverscaleNet.Client.Tests.Modules;

public class ClientModuleTests : IClassFixture<EverClientTestsFixture> {
	private readonly IEverClient _everClient;

	public ClientModuleTests(EverClientTestsFixture fixture, ITestOutputHelper outputHelper) {
		_everClient = fixture.CreateClient(outputHelper);
	}

	[Fact]
	public async Task ReturnsApiReference() {
		ResultOfGetApiReference result = await _everClient.Client.GetApiReference(TestContext.Current.CancellationToken);

		result.Api.ShouldNotBeNull();
	}

	[Fact]
	public async Task ReturnsBuildInfo() {
		ResultOfBuildInfo result = await _everClient.Client.BuildInfo(TestContext.Current.CancellationToken);

		result.Dependencies.ShouldNotBeNull();
		// todo: 1.1.2+ returns  build_number = 0
		// result.BuildNumber.ShouldBePositive();
	}

	[Fact]
	public async Task ReturnsMatchedVersion() {
		ResultOfVersion result = await _everClient.Client.Version(TestContext.Current.CancellationToken);

		result.Version.ShouldBe(Static.SdkVersion);
	}

	[Fact]
	public async Task CheckDefaultClientConfig() {
		ClientConfig result = await _everClient.Client.Config(TestContext.Current.CancellationToken);

		result.Binding.Library.ShouldBe(Static.BindingName);
		result.Binding.Version.ShouldBe(Static.SdkVersion);
		result.Crypto.MnemonicDictionary.ShouldBe(MnemonicDictionary.English);
		result.Crypto.MnemonicWordCount.ShouldBe((byte)12);
		result.Proofs.CacheInLocalStorage.ShouldBe(true);
	}
}