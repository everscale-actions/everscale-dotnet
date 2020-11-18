using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace ch1seL.TonNet.Client.Tests
{
    public class DisposingTests : IDisposable
    {
        private readonly ServiceProvider _serviceProvider;

        public DisposingTests(ITestOutputHelper testOutputHelper)
        {
            _serviceProvider = new ServiceCollection()
                .AddLogging(builder => builder.AddXUnit(testOutputHelper).SetMinimumLevel(LogLevel.Trace))
                .BuildServiceProvider();
        }

        public void Dispose()
        {
            _serviceProvider?.Dispose();
        }

        [Fact(Timeout = 1000, Skip = "needs investigation")]
        public void TonClientDisposingWell()
        {
            var act = new Func<Task>(async () =>
            {
                var tonClient = new TonClient(_serviceProvider);
                await tonClient.Client.GetApiReference();

                // this call resolve problem lol
                // await Task.Delay(TimeSpan.FromMilliseconds(1)); 

                tonClient.Dispose();
            });

            act.Should().NotThrow();
        }
    }
}