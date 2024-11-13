namespace EverscaleNet;

[AttributeUsage(AttributeTargets.Assembly)]
internal sealed class SdkVersionAttribute : Attribute {
	public SdkVersionAttribute(string sdkVersion) {
		SDK_Version = sdkVersion;
	}

	internal string SDK_Version { get; }
}
