using Microsoft.Extensions.DependencyInjection;
using System;

namespace ch1seL.TonNet.Client
{
    public class TonClient : ITonClient, IDisposable
    {
        private readonly ServiceProvider _serviceProvider;

        public TonClient(IServiceProvider serviceProvider = null)
        {
            _serviceProvider = TonClientServiceProviderBuilder.BuildTonClientServiceProvider(serviceProvider); Client = _serviceProvider.GetRequiredService<IClient>(); Crypto = _serviceProvider.GetRequiredService<ICrypto>(); Abi = _serviceProvider.GetRequiredService<IAbi>(); Boc = _serviceProvider.GetRequiredService<IBoc>(); Processing = _serviceProvider.GetRequiredService<IProcessing>(); Utils = _serviceProvider.GetRequiredService<IUtils>(); Tvm = _serviceProvider.GetRequiredService<ITvm>(); Net = _serviceProvider.GetRequiredService<INet>();
        }

        /// <summary>
        ///  Provides information about library.
        /// </summary>
        public IClient Client { get; }

        /// <summary>
        ///  Crypto functions.
        /// </summary>
        public ICrypto Crypto { get; }

        /// <summary>
        /// <para> Provides message encoding and decoding according to the ABI</para>
        /// <para> specification.</para>
        /// </summary>
        public IAbi Abi { get; }

        /// <summary>
        ///  BOC manipulation module.
        /// </summary>
        public IBoc Boc { get; }

        /// <summary>
        /// <para> Message processing module.</para>
        /// <para> This module incorporates functions related to complex message</para>
        /// <para> processing scenarios.</para>
        /// </summary>
        public IProcessing Processing { get; }

        /// <summary>
        ///  Misc utility Functions.
        /// </summary>
        public IUtils Utils { get; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public ITvm Tvm { get; }

        /// <summary>
        ///  Network access.
        /// </summary>
        public INet Net { get; }

        public void Dispose()
        {
            _serviceProvider?.Dispose();
        }
    }
}