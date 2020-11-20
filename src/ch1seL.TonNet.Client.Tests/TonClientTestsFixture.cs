using System.Collections.Generic;
using ch1seL.TonNet.TestsShared;
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
        private readonly List<ServiceProvider> _serviceProviders = new();

        public ITonClient CreateClient(ITestOutputHelper output, bool localhostNode = false)
        {
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddLogging(builder => builder.AddXUnit(output)
                    .SetMinimumLevel(LogLevel.Trace))
                .AddTonClient(config =>
                {
                    if (localhostNode)
                    {
                        config.ServerAddress = TestsEnvironment.TonNetworkAddress;
                    }
                })
                .BuildServiceProvider();
            
            _serviceProviders.Add(serviceProvider);
            return serviceProvider.GetRequiredService<ITonClient>();
        }

        public void Dispose()
        {
            _serviceProviders?.ForEach(sp=>sp?.Dispose());
        }
    }
}