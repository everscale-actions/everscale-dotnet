using System;
using ch1seL.TonNet.Client;
using ch1seL.TonNet.Client.Models;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class AddTonClientServiceCollectionExtensions
    {
        public static IServiceCollection AddTonClient(this IServiceCollection serviceCollection, Action<NetworkConfig> configureOptions = null)
        {
            return serviceCollection
                .AddTransient<ITonClient, TonClient>()
                .Configure<NetworkConfig>(options => configureOptions?.Invoke(options));
        }
    }
}