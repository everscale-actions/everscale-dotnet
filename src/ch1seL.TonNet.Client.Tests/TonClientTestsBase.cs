using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace ch1seL.TonNet.Client.Tests
{
    public abstract class TonClientTestsBase : IDisposable
    {
        private readonly TonClient _tonClient;

        protected TonClientTestsBase(ITestOutputHelper outputHelper)
        {
            IServiceCollection services = new ServiceCollection()
                .AddLogging(builder => builder.AddXUnit(outputHelper)
                    .AddFilter(level => level == LogLevel.Trace));

            _tonClient = new TonClient(services.BuildServiceProvider());
        }

        protected ITonClient TonClient => _tonClient;

        public void Dispose()
        {
            _tonClient?.Dispose();
        }
    }
}