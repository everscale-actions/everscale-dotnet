using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace ch1seL.TonNet.Client.Tests
{
    public abstract class TonClientTestsBase:IDisposable
    {
        protected TonClient TonClient { get; }
        
        protected TonClientTestsBase(ITestOutputHelper outputHelper)
        {
            IServiceCollection services = new ServiceCollection()
                .AddLogging(builder => builder.AddXUnit(outputHelper));

            TonClient = new TonClient(services.BuildServiceProvider());
        }

        public void Dispose()
        {
            TonClient?.Dispose();
        }
    }
}