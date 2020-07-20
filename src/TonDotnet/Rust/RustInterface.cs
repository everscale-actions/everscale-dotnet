using System;
using System.Runtime.InteropServices;
using TonDotnet.Rust.Models;

namespace TonDotnet.Rust
{
    internal static class RustInterface
    {
        [DllImport("ton_client")]
        public static extern int tc_create_context();

        [DllImport("ton_client")]
        public static extern int tc_destroy_context(int context);

        [DllImport("ton_client")]
        public static extern IntPtr tc_json_request(int context, InteropString methodName, InteropString paramsJson);

        [DllImport("ton_client")]
        public static extern void tc_destroy_json_response(IntPtr response);

        [DllImport("ton_client")]
        public static extern InteropJsonResponse tc_read_json_response(IntPtr response);
    }
}