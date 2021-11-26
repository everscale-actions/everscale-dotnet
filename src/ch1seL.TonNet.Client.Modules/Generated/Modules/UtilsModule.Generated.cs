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
        /// <para>Validates and returns the type of any TON address.</para>
        /// <para>Address types are the following</para>
        /// <para>`0:919db8e740d50bf349df2eea03fa30c385d846b991ff5542e67098ee833fc7f7` - standard TON address most</para>
        /// <para>commonly used in all cases. Also called as hex address</para>
        /// <para>`919db8e740d50bf349df2eea03fa30c385d846b991ff5542e67098ee833fc7f7` - account ID. A part of full</para>
        /// <para>address. Identifies account inside particular workchain</para>
        /// <para>`EQCRnbjnQNUL80nfLuoD+jDDhdhGuZH/VULmcJjugz/H9wam` - base64 address. Also called "user-friendly".</para>
        /// <para>Was used at the beginning of TON. Now it is supported for compatibility</para>
        /// </summary>
        public async Task<ResultOfGetAddressType> GetAddressType(ParamsOfGetAddressType @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfGetAddressType, ResultOfGetAddressType>("utils.get_address_type", @params, cancellationToken);
        }

        /// <summary>
        /// Calculates storage fee for an account over a specified time period
        /// </summary>
        public async Task<ResultOfCalcStorageFee> CalcStorageFee(ParamsOfCalcStorageFee @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfCalcStorageFee, ResultOfCalcStorageFee>("utils.calc_storage_fee", @params, cancellationToken);
        }

        /// <summary>
        /// Compresses data using Zstandard algorithm
        /// </summary>
        public async Task<ResultOfCompressZstd> CompressZstd(ParamsOfCompressZstd @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfCompressZstd, ResultOfCompressZstd>("utils.compress_zstd", @params, cancellationToken);
        }

        /// <summary>
        /// Decompresses data using Zstandard algorithm
        /// </summary>
        public async Task<ResultOfDecompressZstd> DecompressZstd(ParamsOfDecompressZstd @params, CancellationToken cancellationToken = default)
        {
            return await _tonClientAdapter.Request<ParamsOfDecompressZstd, ResultOfDecompressZstd>("utils.decompress_zstd", @params, cancellationToken);
        }
    }
}