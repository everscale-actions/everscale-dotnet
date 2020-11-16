using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Abstract;
using ch1seL.TonNet.Client.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.Client
{
    public interface IUtils : ITonModule
    {
        public Task<ConvertAddressResponse> ConvertAddress(ConvertAddressRequest @params, CancellationToken cancellationToken = default);
    }
}