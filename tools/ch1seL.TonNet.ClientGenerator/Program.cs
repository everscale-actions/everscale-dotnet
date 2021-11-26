using System.Reflection;
using System.Threading.Tasks;

namespace ch1seL.TonNet.ClientGenerator;

internal class Program {
	private static async Task Main() {
		string repositoryLocation = Assembly.GetEntryAssembly()!
		                                    .GetCustomAttribute<RepositoryLocationAttribute>()
		                                    ?.SourcesLocation;

		await ClientGenerator.GenerateClient(repositoryLocation);
	}
}