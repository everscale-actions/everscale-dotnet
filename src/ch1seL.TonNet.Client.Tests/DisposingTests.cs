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

        [Fact]
        public void TonClientDisposing()
        {
            var act = new Func<Task>(async () =>
            {
                var tonClient = new TonClient(_serviceProvider);
                
                await tonClient.Client.GetApiReference();

                tonClient.Dispose();
            });

            act.Should().NotThrow();
        }
    }
}