using EverscaleNet.Abstract;
using EverscaleNet.Abstract.Modules;
using EverscaleNet.Client.Modules;
using System;

namespace EverscaleNet.Client
{
    public class EverClient : IEverClient
    {
        private readonly IEverClientAdapter _everClientAdapter;

        public EverClient(IEverClientAdapter everClientAdapter)
        {
            _everClientAdapter = everClientAdapter;
            Client = new ClientModule(everClientAdapter);
            Crypto = new CryptoModule(everClientAdapter);
            Abi = new AbiModule(everClientAdapter);
            Boc = new BocModule(everClientAdapter);
            Processing = new ProcessingModule(everClientAdapter);
            Utils = new UtilsModule(everClientAdapter);
            Tvm = new TvmModule(everClientAdapter);
            Net = new NetModule(everClientAdapter);
            Debot = new DebotModule(everClientAdapter);
            Proofs = new ProofsModule(everClientAdapter);
        }

        /// <summary>
        /// Provides information about library.
        /// </summary>
        public IClientModule Client { get; }

        /// <summary>
        /// Crypto functions.
        /// </summary>
        public ICryptoModule Crypto { get; }

        /// <summary>
        /// Provides message encoding and decoding according to the ABI specification.
        /// </summary>
        public IAbiModule Abi { get; }

        /// <summary>
        /// BOC manipulation module.
        /// </summary>
        public IBocModule Boc { get; }

        /// <summary>
        /// <para>Message processing module.</para>
        /// <para>This module incorporates functions related to complex message</para>
        /// <para>processing scenarios.</para>
        /// </summary>
        public IProcessingModule Processing { get; }

        /// <summary>
        /// Misc utility Functions.
        /// </summary>
        public IUtilsModule Utils { get; }

        /// <summary>
        /// Not described yet..
        /// </summary>
        public ITvmModule Tvm { get; }

        /// <summary>
        /// Network access.
        /// </summary>
        public INetModule Net { get; }

        /// <summary>
        /// [UNSTABLE](UNSTABLE.md) Module for working with debot.
        /// </summary>
        public IDebotModule Debot { get; }

        /// <summary>
        /// [UNSTABLE](UNSTABLE.md) Module for proving data, retrieved from TONOS API.
        /// </summary>
        public IProofsModule Proofs { get; }
    }
}