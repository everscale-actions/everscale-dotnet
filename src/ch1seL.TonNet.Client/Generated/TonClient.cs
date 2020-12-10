using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Abstract.Modules;
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
        /// Not described yet..
        /// </summary>
        public IClientModule Client { get; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public ICryptoModule Crypto { get; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public IAbiModule Abi { get; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public IBocModule Boc { get; }

        /// <summary>
        /// <para>This module incorporates functions related to complex message</para>
        /// <para>processing scenarios.</para>
        /// </summary>
        public IProcessingModule Processing { get; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public IUtilsModule Utils { get; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public ITvmModule Tvm { get; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public INetModule Net { get; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public IDebotModule Debot { get; }

        public void Dispose()
        {
            _serviceProvider?.Dispose();
        }
    }
}