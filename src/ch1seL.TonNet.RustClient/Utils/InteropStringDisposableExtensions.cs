using ch1seL.TonClientDotnet.RustInterop.Models;

namespace ch1seL.TonClientDotnet.Utils
{
    public static class InteropStringDisposableExtensions
    {
        public static InteropStringDisposable ToInteropStringDisposable(this string str)
        {
            return InteropStringDisposable.CreateAndAlloc(str);
        }
    }
}