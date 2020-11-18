using System;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.RustClient
{
    public interface IRustTonClientCore
    {
        Task<string> Request<TEvent>(string method, string requestJson, Action<TEvent, uint> callback = null, CancellationToken cancellationToken = default);
    }
}