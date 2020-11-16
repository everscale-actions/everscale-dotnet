using ch1seL.TonNet.Abstract;
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

        public IClient Client { get; }
        public ICrypto Crypto { get; }
        public IAbi Abi { get; }
        public IBoc Boc { get; }
        public IProcessing Processing { get; }
        public IUtils Utils { get; }
        public ITvm Tvm { get; }
        public INet Net { get; }

        public void Dispose()
        {
            _serviceProvider?.Dispose();
        }
    }
}