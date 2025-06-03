namespace EverscaleNet;

[AttributeUsage(AttributeTargets.Assembly)]
internal sealed class SdkVersionAttribute(string sdkVersion) : Attribute {
	// ReSharper disable once InconsistentNaming
	internal string SDK_Version { get; } = sdkVersion;
}