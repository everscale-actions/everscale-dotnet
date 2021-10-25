using ch1seL.TonNet.Adapter.Rust.RustInterop.Models;

namespace ch1seL.TonNet.Adapter.Rust.Utils
{
    public static class InteropStringDisposableExtensions
    {
        public static InteropStringDisposable ToInteropStringDisposable(this string str)
        {
            return InteropStringDisposable.CreateAndAlloc(str);
        }
    }
}