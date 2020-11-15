using ch1seL.TonNet.RustClient.RustInterop.Models;

namespace ch1seL.TonNet.RustClient.Utils
{
    public static class InteropStringDisposableExtensions
    {
        public static InteropStringDisposable ToInteropStringDisposable(this string str)
        {
            return InteropStringDisposable.CreateAndAlloc(str);
        }
    }
}