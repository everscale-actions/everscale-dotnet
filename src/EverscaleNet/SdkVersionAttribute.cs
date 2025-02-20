namespace EverscaleNet;

[AttributeUsage(AttributeTargets.Assembly)]
internal sealed class SdkVersionAttribute : Attribute {
	public SdkVersionAttribute(string sdkVersion) {
		SDK_Version = sdkVersion;
	}

	// ReSharper disable once InconsistentNaming
	internal string SDK_Version { get; }
}
