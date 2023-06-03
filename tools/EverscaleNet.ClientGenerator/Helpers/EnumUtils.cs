namespace EverscaleNet.ClientGenerator.Helpers;

internal static class EnumUtils {
	public static string GetEnumMemberValueOrString<T>(this T enumValue) where T : Enum {
		return enumValue.FindAttributeOfType<EnumMemberAttribute>()?.Value ?? enumValue.ToString();
	}

	private static T FindAttributeOfType<T>(this Enum enumValue, Func<T, bool> filter = null) {
		return enumValue.GetType()
		                .GetField(enumValue.ToString())!
		                .GetCustomAttributes(false)
		                .OfType<T>()
		                .SingleOrDefault(a => filter?.Invoke(a) ?? true);
	}
}
