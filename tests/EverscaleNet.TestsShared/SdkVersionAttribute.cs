namespace EverscaleNet.TestsShared;

[AttributeUsage(AttributeTargets.Assembly)]
public sealed class SdkVersionAttribute : Attribute {
	public SdkVersionAttribute(string sdkVersion) {
		SdkVersion = sdkVersion;
	}

	public string SdkVersion { get; }
}
