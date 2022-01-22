using System;
using System.Runtime.InteropServices;
using System.Text;

namespace EverscaleNet.Adapter.Rust.RustInterop.Models;

//we have to use separate disposable struct to avoid use unmanaged shared resources
[StructLayout(LayoutKind.Sequential)]
public struct InteropStringDisposable : IDisposable {
	private IntPtr Pointer;
	private uint Length;

	private bool _disposed;

	public static InteropStringDisposable CreateAndAlloc(string str) {
		str ??= string.Empty;
		byte[] bytes = Encoding.UTF8.GetBytes(str);
		IntPtr pointer = Marshal.AllocHGlobal(bytes.Length);
		Marshal.Copy(bytes, 0, pointer, bytes.Length);
		return new InteropStringDisposable {
			Pointer = pointer,
			Length = (uint)bytes.Length
		};
	}

	public void Dispose() {
		if (_disposed) {
			return;
		}

		Marshal.FreeHGlobal(Pointer);
		_disposed = true;
	}
}