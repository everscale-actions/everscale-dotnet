using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace ch1seL.TonNet.RustAdapter.Tests
{
    internal static class TestsHelpers
    {
        public static RustTonClientCore CreateTonClient(ILogger<RustTonClientCore> logger = null)
        {
            return new RustTonClientCore(null, logger ?? NullLogger<RustTonClientCore>.Instance);
        }
    }
}