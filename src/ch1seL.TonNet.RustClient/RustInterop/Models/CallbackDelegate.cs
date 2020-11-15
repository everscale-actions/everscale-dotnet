using System.Runtime.InteropServices;

namespace ch1seL.TonNet.RustClient.RustInterop.Models
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void CallbackDelegate(uint requestId,InteropString paramsJson, uint responseType, bool finished);
}