using ch1seL.TonNet.RustAdapter.RustInterop.Models;

namespace ch1seL.TonNet.RustAdapter.Utils
{
    public static class InteropStringDisposableExtensions
    {
        public static InteropStringDisposable ToInteropStringDisposable(this string str)
        {
            return InteropStringDisposable.CreateAndAlloc(str);
        }
    }
}