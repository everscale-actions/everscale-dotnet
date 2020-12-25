using System;
using System.Text.Json;
using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Modules;
using ch1seL.TonNet.RustAdapter;
using ch1seL.TonNet.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace ch1seL.TonNet.Client
{
    internal static class TonClientServiceProviderBuilder
    {
        /// <summary>
        ///     Build TON Client personal service provider to hide all internal logic from user
        /// </summary>
        /// <param name="serviceProvider">User service provider</param>
        /// <returns>Created personal provider</returns>
        internal static ServiceProvider BuildTonClientServiceProvider(IServiceProvider serviceProvider = null)
        {
            var serviceCollection = new ServiceCollection();
            return serviceCollection
                .AddSingleton(serviceProvider?.GetService<ILoggerFactory>() ?? NullLoggerFactory.Instance)
                .AddTransient<ITonClientAdapter, TonNetRustAdapter>()
                .AddSingleton<IRustTonClientCore>(provider =>
                {
                    ILoggerFactory loggerFactory = serviceProvider?.GetService<ILoggerFactory>() ?? NullLoggerFactory.Instance;
                    TonClientOptions tonClientOptions = serviceProvider?.GetRequiredService<IOptions<TonClientOptions>>().Value ?? new TonClientOptions();

                    // todo: workaround fixed in 1.5.0, remove this line
                    tonClientOptions.Network.ServerAddress ??= string.Empty;

                    var configJson = JsonSerializer.Serialize(tonClientOptions, JsonOptionsProvider.JsonSerializerOptions);
                    var logger = loggerFactory.CreateLogger<RustTonClientCore>();

                    return new RustTonClientCore(configJson, logger);
                })
                .AddServicesAsTransient(typeof(ITonModule), new[] {typeof(ITonModule).Assembly, typeof(AbiModule).Assembly})
                .BuildServiceProvider();
        }
    }
}