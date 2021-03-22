using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Abstract.Modules;
using ch1seL.TonNet.Client.Models;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Client.Modules
{
    public class UtilsModule : IUtilsModule
    {
        private readonly ITonClientAdapter _tonClientAdapter;

        public UtilsModule(ITonClientAdapter tonClientAdapter)
        {
            _tonClientAdapter = tonClientAdapter;
        }

        /// <summary>
        /// Converts address from any TON format to any TON format
        /// </summary>
        public async Task<ResultOfConvertAddress> ConvertAddress(ParamsOfConvertAddress @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfConvertAddress, ResultOfConvertAddress>("utils.convert_address", @params, cancellationToken);
        }

        /// <summary>
        /// Calculates storage fee for an account over a specified time period
        /// </summary>
        public async Task<ResultOfCalcStorageFee> CalcStorageFee(ParamsOfCalcStorageFee @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfCalcStorageFee, ResultOfCalcStorageFee>("utils.calc_storage_fee", @params, cancellationToken);
        }
    }
}