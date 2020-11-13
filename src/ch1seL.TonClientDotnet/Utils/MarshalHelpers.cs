using System.Runtime.InteropServices;
using System.Text;
using TonDotnet.Rust.Models;

namespace TonDotnet.Utils
{
    public class MarshalHelpers
    {
        public static InteropString StringToInteropString(string str)
        {
            str = str ?? string.Empty;

            var bytes = Encoding.UTF8.GetBytes(str);

            var pointer = Marshal.AllocHGlobal(bytes.Length);
            Marshal.Copy(bytes, 0, pointer, bytes.Length);

            return new InteropString
            {
                Pointer = pointer,
                Length = bytes.Length
            };
        }

        public static string InteropStringToString(InteropString str)
        {
            var bytes = new byte[str.Length];
            Marshal.Copy(str.Pointer, bytes, 0, str.Length);
            var res = Encoding.UTF8.GetString(bytes);
            return string.IsNullOrEmpty(res) ? "null" : res;
        }
    }
}