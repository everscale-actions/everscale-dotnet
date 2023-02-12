using System.Text;

namespace EverscaleNet.Client.Tests.Utils;

internal static class StringExtensions {
	public static string ToBase64(this string input) {
		return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
	}

	public static string ToHexString(this string input) {
		byte[] bytes = Encoding.Default.GetBytes(input);
		return BitConverter.ToString(bytes).Replace("-", string.Empty);
	}

	public static string FromBase64(this string input) {
		return Encoding.UTF8.GetString(Convert.FromBase64String(input));
	}

	public static string HexToBase64(this string input) {
		byte[] bytes = Enumerable.Range(0, input.Length)
		                         .Where(x => x % 2 == 0)
		                         .Select(x => Convert.ToByte(input.Substring(x, 2), 16))
		                         .ToArray();

		return Convert.ToBase64String(bytes);
	}
}
