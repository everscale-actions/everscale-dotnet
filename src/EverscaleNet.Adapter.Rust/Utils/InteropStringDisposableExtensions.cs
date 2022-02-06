using EverscaleNet.Adapter.Rust.RustInterop.Models;

namespace EverscaleNet.Adapter.Rust.Utils;

internal static class InteropStringDisposableExtensions {
	internal static InteropStringDisposable ToInteropStringDisposable(this string str) {
		return InteropStringDisposable.CreateAndAlloc(str);
	}
}
