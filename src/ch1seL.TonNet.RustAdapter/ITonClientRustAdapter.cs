using System;
using System.Threading;
using System.Threading.Tasks;

namespace ch1seL.TonNet.RustAdapter
{
    public interface ITonClientRustAdapter : IDisposable
    {
        Task<string> RustRequest(string method, string requestJson, Action<string, uint> callback = null, CancellationToken cancellationToken = default);
    }
}