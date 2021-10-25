using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Adapter.Rust;
using ch1seL.TonNet.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ch1seL.TonNet.RustAdapter.Tests
{
    internal static class TestsHelpers
    {
        public static ITonClientAdapter CreateRustAdapter(ILogger<TonClientRustAdapter> logger)
        {
            return new TonClientRustAdapter(Options.Create(new TonClientOptions()), logger);
        }
    }
}