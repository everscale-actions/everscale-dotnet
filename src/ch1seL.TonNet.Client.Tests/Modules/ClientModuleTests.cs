using System.Threading.Tasks;
using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Models;
using ch1seL.TonNet.TestsShared;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace ch1seL.TonNet.Client.Tests.Modules
{
    public class ClientModuleTests : IClassFixture<TonClientTestsFixture>
    {
        private readonly ITonClient _tonClient;

        public ClientModuleTests(TonClientTestsFixture fixture, ITestOutputHelper outputHelper)
        {
            _tonClient = fixture.CreateClient(outputHelper);
        }

        [Fact]
        public async Task ReturnsMatchedVersion()
        {
            ResultOfVersion result = await _tonClient.Client.Version();

            result.Version.Should().Be(TestsEnv.SdkVersion);
        }

        [Fact(Skip = "Release 1.1.2 has BuildNumber with 0")]
        public async Task ReturnsBuildInfo()
        {
            ResultOfBuildInfo result = await _tonClient.Client.BuildInfo();

            result.Dependencies.Should().NotBeNull();
            result.BuildNumber.Should().BePositive();
        }

        [Fact]
        public async Task ReturnsApiReference()
        {
            ResultOfGetApiReference result = await _tonClient.Client.GetApiReference();

            result.Api.Should().NotBeNull();
        }
    }
}