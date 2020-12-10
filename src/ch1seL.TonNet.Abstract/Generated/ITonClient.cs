using ch1seL.TonNet.Abstract.Modules;

namespace ch1seL.TonNet.Abstract
{
    public interface ITonClient
    {
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
    }
}