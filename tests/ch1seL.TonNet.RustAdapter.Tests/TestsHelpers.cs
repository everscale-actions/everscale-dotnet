using ch1seL.TonNet.Client;
using Microsoft.Extensions.Logging;

namespace ch1seL.TonNet.RustAdapter.Tests
{
    internal static class TestsHelpers
    {
        public static ITonClientRustAdapter CreateRustAdapter(ILogger<TonClientRustAdapter> logger)
        {
            return new TonClientRustAdapter(new TonClientOptions(), logger);
        }
    }
}