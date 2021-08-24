using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using Xunit;
using Xunit.Abstractions;

namespace ch1seL.TonNet.Client.Tests
{
    public class DisposingTests : IDisposable
    {
        private readonly ServiceProvider _serviceProvider;

        public DisposingTests(ITestOutputHelper output)
        {
            Logger logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.TestOutput(output)
                .CreateLogger();

            _serviceProvider = new ServiceCollection()
                .AddLogging(builder => builder.AddSerilog(logger))
                .BuildServiceProvider();
        }

        public void Dispose()
        {
            _serviceProvider?.Dispose();
        }

        [Fact]
        public async Task TonClientDisposing()
        {
            var act = new Func<Task>(async () =>
            {
                var tonClient = new TonClient(_serviceProvider);

                await tonClient.Client.GetApiReference();

                tonClient.Dispose();
            });

            await act.Should().NotThrowAsync();
        }
    }
}