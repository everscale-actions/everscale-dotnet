using System;
using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client;
using ch1seL.TonNet.Client.Models;
using ch1seL.TonNet.Client.PackageManager;
using ch1seL.TonNet.Debot;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class AddTonClientServiceCollectionExtensions
    {
        /// <summary>
        ///     Provide ITonClient and ITonPackageManager in DI
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="configureNetworkOptions">Configure network <see cref="NetworkConfig" /> https://docs.ton.dev/86757ecb2/p/5328db-tonclient/t/31a659</param>
        /// <param name="configurePackageManagerOptions">Configure package manager, contracts path and etc. <see cref="PackageManagerOptions" /></param>
        /// <returns></returns>
        public static IServiceCollection AddTonClient(this IServiceCollection serviceCollection, Action<NetworkConfig> configureNetworkOptions = null,
            Action<PackageManagerOptions> configurePackageManagerOptions = null)
        {
            return serviceCollection
                .AddTransient<ITonClient, TonClient>()
                .AddTransient<ITonPackageManager, FilePackageManager>()
                .AddTransient<IDebotBrowser, DefaultDebotBrowser>()
                .Configure<NetworkConfig>(options => configureNetworkOptions?.Invoke(options))
                .Configure<PackageManagerOptions>(options => configurePackageManagerOptions?.Invoke(options));
        }
    }
}