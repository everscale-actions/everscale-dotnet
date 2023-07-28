using EverscaleNet.Abstract.Modules;

namespace EverscaleNet.Abstract
{
    /// <summary>
    /// <para>IEverClient</para>
    /// </summary>
    public interface IEverClient
    {
        /// <summary>
        /// <para>Provides information about library.</para>
        /// </summary>
        public IClientModule Client { get; }

        /// <summary>
        /// <para>Crypto functions.</para>
        /// </summary>
        public ICryptoModule Crypto { get; }

        /// <summary>
        /// <para>Provides message encoding and decoding according to the ABI specification.</para>
        /// </summary>
        public IAbiModule Abi { get; }

        /// <summary>
        /// <para>BOC manipulation module.</para>
        /// </summary>
        public IBocModule Boc { get; }

        /// <summary>
        /// <para>Message processing module.</para>
        /// <para>This module incorporates functions related to complex message</para>
        /// <para>processing scenarios.</para>
        /// </summary>
        public IProcessingModule Processing { get; }

        /// <summary>
        /// <para>Misc utility Functions.</para>
        /// </summary>
        public IUtilsModule Utils { get; }

        /// <summary>
        /// <para>Not described yet..</para>
        /// </summary>
        public ITvmModule Tvm { get; }

        /// <summary>
        /// <para>Network access.</para>
        /// </summary>
        public INetModule Net { get; }

        /// <summary>
        /// <para>[UNSTABLE](UNSTABLE.md) [DEPRECATED](DEPRECATED.md) Module for working with debot.</para>
        /// </summary>
        public IDebotModule Debot { get; }

        /// <summary>
        /// <para>[UNSTABLE](UNSTABLE.md) [DEPRECATED](DEPRECATED.md) Module for proving data, retrieved from TONOS API.</para>
        /// </summary>
        public IProofsModule Proofs { get; }
    }
}