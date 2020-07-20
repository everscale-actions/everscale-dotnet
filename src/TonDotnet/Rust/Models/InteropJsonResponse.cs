using System.Runtime.InteropServices;

namespace TonDotnet.Rust.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public struct InteropJsonResponse
    {
        public InteropString ResultJson { get; set; }
        public InteropString ErrorJson { get; set; }
    }
}