using System;
using System.Runtime.InteropServices;
using EverscaleNet.Adapter.Rust.RustInterop.Models;

namespace EverscaleNet.Adapter.Rust.RustInterop;

internal static class RustInteropInterface {
	[DllImport(EverClientLib, CallingConvention = CallingConvention.Cdecl)]
	public static extern IntPtr tc_create_context(InteropStringDisposable config);

	[DllImport(EverClientLib, CallingConvention = CallingConvention.Cdecl)]
	public static extern void tc_destroy_context(uint context);

	[DllImport(EverClientLib, CallingConvention = CallingConvention.Cdecl)]
	public static extern InteropString tc_read_string(IntPtr str);

	[DllImport(EverClientLib, CallingConvention = CallingConvention.Cdecl)]
	public static extern void tc_destroy_string(IntPtr str);

	[DllImport(EverClientLib, CallingConvention = CallingConvention.Cdecl)]
	public static extern void tc_request(uint context, InteropStringDisposable function,
	                                     InteropStringDisposable parameters, uint requestId,
	                                     CallbackDelegate callbackPointer);

	private const string EverClientLib = "tonclient";
}