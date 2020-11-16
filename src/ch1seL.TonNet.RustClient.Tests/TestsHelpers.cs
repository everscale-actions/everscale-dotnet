using Microsoft.Extensions.Logging.Abstractions;

namespace ch1seL.TonNet.RustClient.Tests
{
    internal static class TestsHelpers
    {
        public static RustTonClientCore CreateTonClient()
        {
            return new(null, NullLogger<RustTonClientCore>.Instance);
        }
    }
}