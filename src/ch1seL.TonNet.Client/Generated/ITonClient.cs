using System;

namespace ch1seL.TonNet.Client
{
    public interface ITonClient : IDisposable
    {
        public IClient Client { get; }
        public ICrypto Crypto { get; }
        public IAbi Abi { get; }
        public IBoc Boc { get; }
        public IProcessing Processing { get; }
        public IUtils Utils { get; }
        public ITvm Tvm { get; }
        public INet Net { get; }
    }
}