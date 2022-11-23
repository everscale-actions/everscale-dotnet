using EverscaleNet.Adapter.Wasm.RustInterop.Models;

namespace EverscaleNet.Adapter.Wasm.Utils;

internal static class InteropStringDisposableExtensions {
	internal static InteropStringDisposable ToInteropStringDisposable(this string str) {
		return InteropStringDisposable.CreateAndAlloc(str);
	}
}

