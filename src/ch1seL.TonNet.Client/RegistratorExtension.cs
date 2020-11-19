﻿using System;
using System.Text.Json;
using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Models;
using ch1seL.TonNet.RustClient;
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
        /// <param name="loggerFactory"></param>
        /// <param name="serviceProvider">User service provider</param>
        /// <returns>Created personal provider</returns>
        internal static ServiceProvider BuildTonClientServiceProvider(IServiceProvider serviceProvider = null)
        {
            var serviceCollection = new ServiceCollection();
            return serviceCollection
                .AddLogging()
                .AddSingleton(serviceProvider?.GetService<ILoggerFactory>() ?? NullLoggerFactory.Instance)
                .AddSingleton<ITonClientAdapter, TonNetRustAdapter>()
                .AddSingleton<IRustTonClientCore>(provider =>
                {
                    NetworkConfig networkOptions = provider.GetRequiredService<IOptions<NetworkConfig>>().Value;
                    // workaround cause field is not marked as nullable in TON SDK
                    networkOptions.ServerAddress ??= string.Empty;
                    var optionsJson = JsonSerializer.Serialize(new {network = networkOptions}, JsonOptionsProvider.JsonSerializerOptions);
                    
                    var logger = provider.GetRequiredService<ILogger<RustTonClientCore>>();
                    
                    return new RustTonClientCore(optionsJson, logger);
                })
                .AddServicesAsTransient(typeof(ITonModule))
                .AddOptions()
                .AddSingleton(serviceProvider?.GetService<IOptions<NetworkConfig>>() ??
                              new OptionsWrapper<NetworkConfig>(new NetworkConfig {ServerAddress = "http://localhost"}))
                .BuildServiceProvider();
        }
    }
}