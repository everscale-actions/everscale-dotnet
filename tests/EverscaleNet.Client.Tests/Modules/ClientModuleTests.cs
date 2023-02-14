using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace EverscaleNet.Client.Tests.Modules;

public class ClientModuleTests : IClassFixture<EverClientTestsFixture> {
	private readonly IEverClient _everClient;

	public ClientModuleTests(EverClientTestsFixture fixture, ITestOutputHelper outputHelper) {
		_everClient = fixture.CreateClient(outputHelper);
	}

	[Fact]
	public async Task ReturnsApiReference() {
		ResultOfGetApiReference result = await _everClient.Client.GetApiReference();

		result.Api.Should().NotBeNull();
	}

	[Fact]
	public async Task ReturnsBuildInfo() {
		ResultOfBuildInfo result = await _everClient.Client.BuildInfo();

		result.Dependencies.Should().NotBeNull();
		// todo: 1.1.2+ returns  build_number = 0
		// result.BuildNumber.Should().BePositive();
	}

	[Fact]
	public async Task ReturnsMatchedVersion() {
		ResultOfVersion result = await _everClient.Client.Version();

		result.Version.Should().Be(Static.SdkVersion);
	}

	[Fact]
	public async Task CheckDefaultClientConfig() {
		ClientConfig result = await _everClient.Client.Config();

		result.Binding.Library.Should().Be(Static.BindingName);
		result.Binding.Version.Should().Be(Static.SdkVersion);
		result.Crypto.MnemonicDictionary.Should().Be(MnemonicDictionary.English);
		result.Crypto.MnemonicWordCount.Should().Be(12);
		result.Proofs.CacheInLocalStorage.Should().BeTrue();
	}
}
