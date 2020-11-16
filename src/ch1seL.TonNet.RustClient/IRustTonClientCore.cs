using System;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.RustClient
{
    internal interface IRustTonClientCore
    {
        Task<string> Request<TEvent>(string method, string paramsJson, Action<TEvent> callback = null, CancellationToken cancellationToken = default);
    }
}