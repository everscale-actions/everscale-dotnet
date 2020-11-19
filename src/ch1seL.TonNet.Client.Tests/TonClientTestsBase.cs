using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace ch1seL.TonNet.Client.Tests
{
    public class TonClientTestsFixture : IDisposable
    {
        private ServiceProvider _serviceProvider;
        private ITonClient _tonClient;

        public ITonClient CreateClient(ITestOutputHelper output, bool localhostNode=false)
        {
            _serviceProvider = new ServiceCollection()
                .AddLogging(builder => builder.AddXUnit(output)
                    .AddFilter(level => level == LogLevel.Trace))
                .AddTonClient(config =>
                {
                    if (localhostNode) config.ServerAddress = "http://localhost";
                })
                .BuildServiceProvider();

            _tonClient = _serviceProvider.GetRequiredService<ITonClient>();
            return _tonClient;
        }

        public void Dispose()
        {
            _serviceProvider?.Dispose();
        }
    }
}