using ch1seL.TonNet.Client.Modules;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ch1seL.TonNet.Client
{
    public class TonClient : ITonClient, IDisposable
    {
        private readonly ServiceProvider _serviceProvider;

        public TonClient(IServiceProvider serviceProvider = null)
        {
            _serviceProvider = TonClientServiceProviderBuilder.BuildTonClientServiceProvider(serviceProvider); Client = _serviceProvider.GetRequiredService<IClientModule>(); Crypto = _serviceProvider.GetRequiredService<ICryptoModule>(); Abi = _serviceProvider.GetRequiredService<IAbiModule>(); Boc = _serviceProvider.GetRequiredService<IBocModule>(); Processing = _serviceProvider.GetRequiredService<IProcessingModule>(); Utils = _serviceProvider.GetRequiredService<IUtilsModule>(); Tvm = _serviceProvider.GetRequiredService<ITvmModule>(); Net = _serviceProvider.GetRequiredService<INetModule>(); Debot = _serviceProvider.GetRequiredService<IDebotModule>();
        }

        /// <summary>
        ///  Provides information about library.
        /// </summary>
        public IClientModule Client { get; }

        /// <summary>
        ///  Crypto functions.
        /// </summary>
        public ICryptoModule Crypto { get; }

        /// <summary>
        /// <para> Provides message encoding and decoding according to the ABI</para>
        /// <para> specification.</para>
        /// </summary>
        public IAbiModule Abi { get; }

        /// <summary>
        ///  BOC manipulation module.
        /// </summary>
        public IBocModule Boc { get; }

        /// <summary>
        /// <para> Message processing module.</para>
        /// <para> This module incorporates functions related to complex message</para>
        /// <para> processing scenarios.</para>
        /// </summary>
        public IProcessingModule Processing { get; }

        /// <summary>
        ///  Misc utility Functions.
        /// </summary>
        public IUtilsModule Utils { get; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public ITvmModule Tvm { get; }

        /// <summary>
        ///  Network access.
        /// </summary>
        public INetModule Net { get; }

        /// <summary>
        ///  [UNSTABLE](UNSTABLE.md) Module for working with debot.
        /// </summary>
        public IDebotModule Debot { get; }

        public void Dispose()
        {
            _serviceProvider?.Dispose();
        }
    }
}