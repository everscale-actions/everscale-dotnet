using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Models;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace ch1seL.TonNet.Client.Tests
{
    public class ParallelRequestsTests : IClassFixture<TonClientTestsFixture>
    {
        private readonly ITonClient _tonClient;

        public ParallelRequestsTests(TonClientTestsFixture fixture, ITestOutputHelper outputHelper)
        {
            _tonClient = fixture.CreateClient(outputHelper);
        }


        [Fact(Timeout = 10000)]
        public async Task ParallelRunNotThrowExceptions()
        {
            var tasks = new List<Task>();

            Parallel.For(0, 10000,
                _ => { tasks.Add(_tonClient.Crypto.MnemonicFromRandom(new ParamsOfMnemonicFromRandom())); });

            Func<Task> act = async () => { await Task.WhenAll(tasks); };

            await act.Should().NotThrowAsync();
        }
    }
}