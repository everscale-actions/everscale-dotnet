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
    }
}