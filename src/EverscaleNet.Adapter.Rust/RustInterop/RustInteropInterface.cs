using System.Runtime.InteropServices;
using EverscaleNet.Adapter.Rust.RustInterop.Models;

namespace EverscaleNet.Adapter.Rust.RustInterop;

internal static class RustInteropInterface {
	private const string EverClientLib = "tonclient";

	[DllImport(EverClientLib, EntryPoint = "tc_create_context", CallingConvention = CallingConvention.Cdecl)]
	public static extern IntPtr CreateContext(InteropStringDisposable config);

	[DllImport(EverClientLib, EntryPoint = "tc_destroy_context", CallingConvention = CallingConvention.Cdecl)]
	public static extern void DestroyContext(uint context);

	[DllImport(EverClientLib, EntryPoint = "tc_read_string", CallingConvention = CallingConvention.Cdecl)]
	public static extern InteropString ReadString(IntPtr str);

	[DllImport(EverClientLib, EntryPoint = "tc_destroy_string", CallingConvention = CallingConvention.Cdecl)]
	public static extern void DestroyString(IntPtr str);

	[DllImport(EverClientLib, EntryPoint = "tc_request", CallingConvention = CallingConvention.Cdecl)]
	public static extern void Request(uint context, InteropStringDisposable function,
	                                  InteropStringDisposable parameters, uint requestId,
	                                  CallbackDelegate callbackPointer);
}
