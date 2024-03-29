﻿using System.Runtime.InteropServices;

namespace EverscaleNet.Adapter.Rust.RustInterop.Models;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal delegate void CallbackDelegate(uint requestId, InteropString paramsJson, uint responseType, bool finished);
