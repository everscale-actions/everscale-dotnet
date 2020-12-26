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
                .AddSingleton<ITonClientAdapter>(provider =>
                {
                    ILoggerFactory loggerFactory = serviceProvider?.GetService<ILoggerFactory>() ?? NullLoggerFactory.Instance;
                    TonClientOptions tonClientOptions = serviceProvider?.GetService<IOptions<TonClientOptions>>()?.Value ?? new TonClientOptions();

                    var configJson = JsonSerializer.Serialize(tonClientOptions, JsonOptionsProvider.JsonSerializerOptions);
                    var logger = loggerFactory.CreateLogger<TonClientRustAdapter>();

                    return new TonClientRustAdapter(configJson, logger);
                })
                .AddServicesAsTransient(typeof(ITonModule), new[] {typeof(ITonModule).Assembly, typeof(AbiModule).Assembly})
                .BuildServiceProvider();
        }
    }
}