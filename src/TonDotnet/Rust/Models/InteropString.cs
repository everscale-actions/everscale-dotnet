using System;
using System.Runtime.InteropServices;

namespace TonDotnet.Rust.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public struct InteropString
    {
        public IntPtr Content;
        public int Length;
    }
}