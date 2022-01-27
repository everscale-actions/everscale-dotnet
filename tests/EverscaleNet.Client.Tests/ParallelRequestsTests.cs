using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EverscaleNet.Abstract;
using EverscaleNet.Client.Models;
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
		var tasks = new List<Task>();

		Parallel.For(0, 10000,
		             _ => { tasks.Add(_everClient.Crypto.MnemonicFromRandom(new ParamsOfMnemonicFromRandom())); });

		Func<Task> act = () => Task.WhenAll(tasks);

		await act.Should().NotThrowAsync();
	}
}
