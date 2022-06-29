using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace EverscaleNet.Client.Tests;

public class ParallelRequestsTests : IClassFixture<EverClientTestsFixture> {
	public ParallelRequestsTests(EverClientTestsFixture fixture, ITestOutputHelper outputHelper) {
		_everClient = fixture.CreateClient(outputHelper);
	}

	private readonly IEverClient _everClient;

	[Fact(Timeout = 30000)]
	public async Task ParallelRunNotThrowExceptions() {
		ParallelQuery<Task<ResultOfMnemonicFromRandom>> tasks = Enumerable
		                                                        .Repeat((object)null, 10000)
		                                                        .AsParallel()
		                                                        .Select(_ => _everClient.Crypto.MnemonicFromRandom(new ParamsOfMnemonicFromRandom()));
		
		Func<Task> act = () => Task.WhenAll(tasks);

		await act.Should().NotThrowAsync();
	}
}
