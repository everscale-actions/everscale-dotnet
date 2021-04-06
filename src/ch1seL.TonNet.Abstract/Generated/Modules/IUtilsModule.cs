using ch1seL.TonNet.Client.Models;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Abstract.Modules
{
    public interface IUtilsModule : ITonModule
    {
        /// <summary>
        /// Converts address from any TON format to any TON format
        /// </summary>
        public Task<ResultOfConvertAddress> ConvertAddress(ParamsOfConvertAddress @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Calculates storage fee for an account over a specified time period
        /// </summary>
        public Task<ResultOfCalcStorageFee> CalcStorageFee(ParamsOfCalcStorageFee @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Compresses data using Zstandard algorithm
        /// </summary>
        public Task<ResultOfCompressZstd> CompressZstd(ParamsOfCompressZstd @params, CancellationToken cancellationToken = default);

        /// <summary>
        /// Decompresses data using Zstandard algorithm
        /// </summary>
        public Task<ResultOfDecompressZstd> DecompressZstd(ParamsOfDecompressZstd @params, CancellationToken cancellationToken = default);
    }
}