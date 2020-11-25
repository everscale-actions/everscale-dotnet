using ch1seL.TonNet.Client.Modules;

namespace ch1seL.TonNet.Client
{
    public interface ITonClient
    {
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
    }
}