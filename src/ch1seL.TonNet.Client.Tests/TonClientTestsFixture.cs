using System;
using System.Collections.Generic;
using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Models;
using ch1seL.TonNet.TestsShared;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using Xunit.Abstractions;

namespace ch1seL.TonNet.Client.Tests
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class TonClientTestsFixture : IDisposable
    {
        private readonly List<ServiceProvider> _serviceProviders = new List<ServiceProvider>();

        public void Dispose()
        {
            _serviceProviders?.ForEach(sp => sp?.Dispose());
        }

        protected internal ITonClient CreateClient(ITestOutputHelper output, bool useNodeSe = false, Action<TonClientOptions> configureOptions = null)
        {
            Logger logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.TestOutput(output)
                .CreateLogger();

            ServiceProvider serviceProvider = new ServiceCollection()
                .AddLogging(builder => builder.AddSerilog(logger))
                .AddTonClient(config =>
                {
                    //as default tests don't use any server by some integration tests require Node SE
                    //if useNodeSe is true we use http://localhost or TON_NETWORK_ADDRESS env if provided
                    config.Network = new NetworkConfig {ServerAddress = useNodeSe ? TestsEnv.TonNetworkAddress : null};

                    configureOptions?.Invoke(config);
                })
                .BuildServiceProvider();

            _serviceProviders.Add(serviceProvider);
            return serviceProvider.GetRequiredService<ITonClient>();
        }
    }
}