namespace EverscaleNet;

[AttributeUsage(AttributeTargets.Assembly)]
internal sealed class SdkVersionAttribute : Attribute {
	public SdkVersionAttribute(string sdkVersion) {
		SdkVersion = sdkVersion;
	}

	internal string SdkVersion { get; }
}
