using System;
using System.Runtime.InteropServices;
using ch1seL.TonNet.RustAdapter.RustInterop.Models;

namespace ch1seL.TonNet.RustAdapter.RustInterop
{
    internal static class RustInteropInterface
    {
        private const string TonClientLib = "tonclient";

        [DllImport(TonClientLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr tc_create_context(InteropStringDisposable config);

        [DllImport(TonClientLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void tc_destroy_context(uint context);

        [DllImport(TonClientLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern InteropString tc_read_string(IntPtr str);

        [DllImport(TonClientLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void tc_destroy_string(IntPtr str);

        [DllImport(TonClientLib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void tc_request(uint context, InteropStringDisposable function, InteropStringDisposable parameters, uint requestId,
            CallbackDelegate callbackPointer);
    }
}