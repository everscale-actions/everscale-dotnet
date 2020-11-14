using ch1seL.TonClientDotnet.Models;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace ch1seL.TonClientDotnet.UnitTests
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