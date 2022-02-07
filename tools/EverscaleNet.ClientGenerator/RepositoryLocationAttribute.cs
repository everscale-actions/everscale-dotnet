namespace EverscaleNet.ClientGenerator;

[AttributeUsage(AttributeTargets.Assembly)]
internal sealed class RepositoryLocationAttribute : Attribute {
	public RepositoryLocationAttribute(string sourcesLocation) {
		SourcesLocation = sourcesLocation;
	}

	public string SourcesLocation { get; }
}
