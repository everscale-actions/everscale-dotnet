using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace ch1seL.TonNet.RustAdapter.Tests
{
    internal static class TestsHelpers
    {
        public static ITonClientRustAdapter CreateTonClient(ILogger<TonClientRustAdapter> logger = null)
        {
            return new TonClientRustAdapter(null, logger ?? NullLogger<TonClientRustAdapter>.Instance);
        }
    }
}