using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;

namespace ch1seL.TonNet.RustAdapter.RustInterop.Models
{
    [StructLayout(LayoutKind.Sequential)]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
    public struct InteropString
    {
        private IntPtr Pointer;
        private uint Length;

        public override string ToString()
        {
            var bytes = new byte[Length];
            Marshal.Copy(Pointer, bytes, 0, (int) Length);
            var res = Encoding.UTF8.GetString(bytes);
            return string.IsNullOrEmpty(res) ? null : res;
        }
    }
}