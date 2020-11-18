using System;
using System.Threading.Tasks;
using ch1seL.TonNet.Client.Models;
using FluentAssertions;
using Xunit;

namespace ch1seL.TonNet.Client.Tests
{
    public class DisposingTests
    {
        [Fact(Timeout = 1000)]
        public void TonClintDisposingWell()
        {
            var act = new Func<Task>(async () =>
            {
                var tonClient = new TonClient();
                ResultOfGetApiReference res = await tonClient.Client.GetApiReference();

                tonClient.Dispose();
            });

            act.Should().NotThrow();
        }
    }
}