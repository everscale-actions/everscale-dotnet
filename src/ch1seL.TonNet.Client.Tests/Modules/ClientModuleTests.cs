using System.Threading.Tasks;
using ch1seL.TonNet.Client.Models;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace ch1seL.TonNet.Client.Tests.Modules
{
    public class ClientModuleTests : TonClientTestsBase
    {
        public ClientModuleTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Fact]
        public async Task ReturnsMatchedVersion()
        {
            ResultOfVersion result = await TonClient.Client.Version();

            result.Version.Should().MatchRegex(@"\d\.\d\.\d");
        }

        [Fact]
        public async Task ReturnsBuildInfo()
        {
            ResultOfBuildInfo result = await TonClient.Client.BuildInfo();

            result.Dependencies.Should().NotBeNull();
            result.BuildNumber.Should().BePositive();
        }

        [Fact]
        public async Task ReturnsApiReference()
        {
            ResultOfGetApiReference result = await TonClient.Client.GetApiReference();

            result.Api.Should().NotBeNull();
        }
    }
}