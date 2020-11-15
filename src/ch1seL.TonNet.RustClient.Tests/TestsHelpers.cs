using ch1seL.TonNet.RustClient.Models;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace ch1seL.TonNet.RustClient.Tests
{
    internal static class TestsHelpers{
        public static RustTonClientCore CreateTonClient()
        {
            return new(Options.Create(new TonClientOptions()
            {
                BaseUrl = "localhost"
            }), NullLogger<RustTonClientCore>.Instance);
        }
    }
}