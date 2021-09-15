using System;
using System.Text.Json;
using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client;
using ch1seL.TonNet.Client.PackageManager;
using ch1seL.TonNet.RustAdapter;
using ch1seL.TonNet.Serialization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

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
        /// <param name="configurePackageManagerOptions">Configure package manager, contracts path and etc. <see cref="FilePackageManagerOptions" /></param>
        /// <returns></returns>
        public static IServiceCollection AddTonClient(this IServiceCollection serviceCollection, Action<TonClientOptions> configureTonClientOptions = null,
            Action<FilePackageManagerOptions> configurePackageManagerOptions = null)
        {
            return serviceCollection
                .AddSingleton<ITonClient>(provider =>
                {
                    var configJson = JsonSerializer.Serialize(provider.GetRequiredService<IOptions<TonClientOptions>>().Value,
                        JsonOptionsProvider.JsonSerializerOptions);
                    var logger = provider.GetService<ILogger<TonClientRustAdapter>>() ?? NullLogger<TonClientRustAdapter>.Instance;
                    return new TonClient(new TonClientRustAdapter(configJson, logger));
                })
                .AddTransient<ITonPackageManager, FilePackageManager>()
                .Configure<TonClientOptions>(options => configureTonClientOptions?.Invoke(options))
                .Configure<FilePackageManagerOptions>(options => configurePackageManagerOptions?.Invoke(options));
        }
    }
}