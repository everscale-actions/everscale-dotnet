namespace EverscaleNet;

internal static class Static {
	internal const string BindingName = "everscale-actions/everscale-dotnet";
	internal static readonly string SdkVersion = typeof(SdkVersionAttribute).Assembly.GetCustomAttribute<SdkVersionAttribute>()!.SdkVersion;
}
