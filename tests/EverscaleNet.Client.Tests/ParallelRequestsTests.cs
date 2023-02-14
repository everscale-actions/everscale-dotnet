using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace EverscaleNet.Client.Tests;

[CollectionDefinition(nameof(SystemTestCollectionDefinition), DisableParallelization = true)]
public class SystemTestCollectionDefinition { }

[Collection(nameof(SystemTestCollectionDefinition))]
public class ParallelRequestsTests : IClassFixture<EverClientTestsFixture> {
	private readonly IEverClient _everClient;

	public ParallelRequestsTests(EverClientTestsFixture fixture, ITestOutputHelper outputHelper) {
		_everClient = fixture.CreateClient(outputHelper);
	}

	[Fact(Timeout = 30000)]
	public async Task ParallelRunNotThrowExceptions() {
		const int parallelTasks = 100000;

		ParallelQuery<Task<KeyPair>> tasks = Enumerable
		                                     .Repeat((object)null, parallelTasks)
		                                     .AsParallel()
		                                     .Select(_ => _everClient.Crypto.GenerateRandomSignKeys());

		Func<Task> act = () => Task.WhenAll(tasks);

		await act.Should().NotThrowAsync();
	}
}
