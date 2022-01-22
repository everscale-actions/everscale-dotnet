using System.Threading.Tasks;
using EverscaleNet.Abstract;
using EverscaleNet.Client.Models;
using EverscaleNet.TestsShared;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace EverscaleNet.Client.Tests.Modules;

public class ClientModuleTests : IClassFixture<EverClientTestsFixture> {
	public ClientModuleTests(EverClientTestsFixture fixture, ITestOutputHelper outputHelper) {
		_everClient = fixture.CreateClient(outputHelper);
	}

	private readonly IEverClient _everClient;

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

		result.Version.Should().Be(TestsEnv.SdkVersion);
	}
}