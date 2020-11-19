using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace ch1seL.TonNet.Client.Tests
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class TonClientTestsFixture
        // todo: waiting for sdk release 1.2.0
        //: IDisposable
    {
        private ServiceProvider _serviceProvider;
        private ITonClient _tonClient;

        public ITonClient CreateClient(ITestOutputHelper output, bool localhostNode = false)
        {
            _serviceProvider = new ServiceCollection()
                .AddLogging(builder => builder.AddXUnit(output)
                    .SetMinimumLevel(LogLevel.Trace))
                .AddTonClient(config =>
                {
                    if (localhostNode)
                    {
                        // TON_SERVER_ADDRESS can be use for testing in github ci
                        config.ServerAddress = Environment.GetEnvironmentVariable("TON_SERVER_ADDRESS") ?? "http://localhost";
                    }
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