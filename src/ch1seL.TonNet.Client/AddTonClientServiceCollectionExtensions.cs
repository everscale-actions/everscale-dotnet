using System;
using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client;
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
        /// <param name="configureTonClientOptions">
        ///     Configure network <see cref="TonClientOptions" />
        ///     https://github.com/tonlabs/TON-SDK/blob/master/docs/mod_client.md#networkconfig
        ///     https://github.com/tonlabs/TON-SDK/blob/master/docs/mod_client.md#cryptoconfig
        ///     https://github.com/tonlabs/TON-SDK/blob/master/docs/mod_client.md#abiconfig
        /// </param>
        /// <param name="configurePackageManagerOptions">Configure package manager, contracts path and etc. <see cref="PackageManagerOptions" /></param>
        /// <returns></returns>
        public static IServiceCollection AddTonClient(this IServiceCollection serviceCollection, Action<TonClientOptions> configureTonClientOptions = null,
            Action<PackageManagerOptions> configurePackageManagerOptions = null)
        {
            if (configurePackageManagerOptions != null) serviceCollection.Configure<PackageManagerOptions>(configurePackageManagerOptions.Invoke);

            return serviceCollection
                .AddTransient<ITonClient, TonClient>()
                .AddTransient<ITonPackageManager, FilePackageManager>()
                .AddTransient<IDebotBrowser, DefaultDebotBrowser>()
                .Configure<TonClientOptions>(options => configureTonClientOptions?.Invoke(options))
                .Configure<PackageManagerOptions>(options => configurePackageManagerOptions?.Invoke(options));
        }
    }
}