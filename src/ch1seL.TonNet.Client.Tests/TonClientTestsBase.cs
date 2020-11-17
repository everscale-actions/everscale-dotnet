using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace ch1seL.TonNet.Client.Tests
{
    public abstract class TonClientTestsBase : IDisposable
    {
        protected TonClientTestsBase(ITestOutputHelper outputHelper)
        {
            IServiceCollection services = new ServiceCollection()
                .AddLogging(builder => builder.AddXUnit(outputHelper)
                    .AddFilter(level => level == LogLevel.Trace));

            TonClient = new TonClient(services.BuildServiceProvider());
        }

        protected TonClient TonClient { get; }

        public void Dispose()
        {
            TonClient?.Dispose();
        }
    }
}