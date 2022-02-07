using System.Reflection;

namespace EverscaleNet.ClientGenerator;

internal class Program {
	private static async Task Main() {
		string repositoryLocation = Assembly.GetEntryAssembly()!
		                                    .GetCustomAttribute<RepositoryLocationAttribute>()
		                                    ?.SourcesLocation;

		await ClientGenerator.GenerateClient(repositoryLocation);
	}
}
