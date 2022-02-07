using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;

namespace EverscaleNet.Adapter.Rust.RustInterop.Models;

[StructLayout(LayoutKind.Sequential)]
[SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
internal struct InteropString {
	private IntPtr Pointer;
	private uint Length;

	public override string ToString() {
		var bytes = new byte[Length];
		Marshal.Copy(Pointer, bytes, 0, (int)Length);
		return Encoding.UTF8.GetString(bytes);
	}
}
