using EverscaleNet.Client.Tests;
using EverscaleNet.TestsShared;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace EverscaleNet.ContractGenerator.Tests;

public class ContractGeneratorTests : IClassFixture<EverClientTestsFixture> {
	private readonly IEverClient _everClient;

	public ContractGeneratorTests(EverClientTestsFixture fixture, ITestOutputHelper outputHelper) {
		_everClient = fixture.CreateClient(outputHelper);
	}

	[Fact]
	public void HelloContainsSayHello() {
		AbiContract contract = ((Abi.Contract)TestsEnv.Packages.Hello.Abi).Value;
		contract.Functions.Length.Should().Be(4);
		contract.Functions.SingleOrDefault(f => f.Name == "");
	}
}
