namespace ch1seL.TonNet.ClientGenerator
{
    [System.AttributeUsage(System.AttributeTargets.Assembly)]
    internal sealed class RepositoryLocationAttribute : System.Attribute
    {
        public string SourcesLocation { get; }
        public RepositoryLocationAttribute(string sourcesLocation)
        {
            SourcesLocation = sourcesLocation;
        }
    }
}