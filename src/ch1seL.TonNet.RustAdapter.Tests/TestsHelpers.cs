using ch1seL.TonNet.RustAdapter;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace ch1seL.TonNet.RustClient.Tests
{
    internal static class TestsHelpers
    {
        public static RustTonClientCore CreateTonClient(ILogger<RustTonClientCore> logger = null)
        {
            return new RustTonClientCore(null, logger ?? NullLogger<RustTonClientCore>.Instance);
        }
    }
}