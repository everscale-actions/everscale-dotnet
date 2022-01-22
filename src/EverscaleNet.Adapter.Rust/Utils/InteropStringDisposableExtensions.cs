using EverscaleNet.Adapter.Rust.RustInterop.Models;

namespace EverscaleNet.Adapter.Rust.Utils;

public static class InteropStringDisposableExtensions {
	public static InteropStringDisposable ToInteropStringDisposable(this string str) {
		return InteropStringDisposable.CreateAndAlloc(str);
	}
}