using System;
using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Adapter.Web;
using ch1seL.TonNet.Client;
using ch1seL.TonNet.Client.Web.PackageManager;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class AddTonClientServiceCollectionExtensions
    {
        /// <summary>
        ///     Provide ITonClient and ITonPackageManager in DI
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configureTonClientOptions">
        ///     Configure network <see cref="TonClientOptions" />
        ///     https://github.com/tonlabs/TON-SDK/blob/master/docs/mod_client.md#networkconfig
        ///     https://github.com/tonlabs/TON-SDK/blob/master/docs/mod_client.md#cryptoconfig
        ///     https://github.com/tonlabs/TON-SDK/blob/master/docs/mod_client.md#abiconfig
        /// </param>
        /// <param name="configurePackageManagerOptions">
        ///     Configure package manager, contracts path and etc.
        ///     <see cref="WebPackageManager" />
        /// </param>
        /// <returns></returns>
        public static IServiceCollection AddTonClient(this IServiceCollection services,
            Action<TonClientOptions> configureTonClientOptions = null,
            Action<WebPackageManagerOptions> configurePackageManagerOptions = null)
        {
            if (configureTonClientOptions != null) services.Configure(configureTonClientOptions);
            if (configurePackageManagerOptions != null) services.Configure(configurePackageManagerOptions);

            return services
                .AddTransient<ITonClientAdapter, TonClientWebAdapter>()
                .AddTransient<ITonClient, TonClient>()
                .AddTransient<ITonPackageManager, WebPackageManager>();
        }
    }
}