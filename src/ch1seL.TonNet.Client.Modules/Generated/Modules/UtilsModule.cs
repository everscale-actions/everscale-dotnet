using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Client
{
    public class UtilsModule : IUtilsModule
    {
        private readonly ITonClientAdapter _tonClientAdapter;

        public UtilsModule(ITonClientAdapter tonClientAdapter)
        {
            _tonClientAdapter = tonClientAdapter;
        }

        /// <summary>
        ///  Converts address from any TON format to any TON format
        /// </summary>
        public async Task<ResultOfConvertAddress> ConvertAddress(ParamsOfConvertAddress @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfConvertAddress, ResultOfConvertAddress>("utils.convert_address", @params, cancellationToken);
        }
    }
}