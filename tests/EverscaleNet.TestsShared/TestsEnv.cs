using EverscaleNet.Client.PackageManager;
using EverscaleNet.Utils;
using Microsoft.Extensions.Options;

namespace EverscaleNet.TestsShared;

public static class TestsEnv {
	private const int DefaultAbiVersion = 2;
	private const string ContractsPath = "_contracts";
	private const string NetworkEndpointsEnvVariable = "EVERSCALE_NETWORK_ENDPOINTS";

	private static readonly IEverPackageManager EverPackageManager = new FilePackageManager(Options.Create(
		                                                                                        new FilePackageManagerOptions
			                                                                                        { PackagesPath = Path.Join(ContractsPath, $"abi_v{CurrentAbiVersion}") }));
	public static readonly string[] EverscaleNetworkEndpoints = Environment.GetEnvironmentVariable(NetworkEndpointsEnvVariable)?.Split(";") ?? EverOS.Endpoints.NodeSE;

	private static int CurrentAbiVersion => int.TryParse(Environment.GetEnvironmentVariable("ABI_VERSION"), out int version)
		                                        ? version == 0 ? DefaultAbiVersion : version
		                                        : DefaultAbiVersion;

	public static class SeGiver {
		public const string Address = "0:ece57bcc6c530283becbbd8a3b24d3c5987cdddc3c8b7b33be6e4a6312490415";
		public static readonly Signer Signer = new Signer.Keys {
			KeysAccessor = new KeyPair {
				Public = "2ada2e65ab8eeab09490e3521415f45b6e42df9c760a639bcf53957550b25a16",
				Secret = "172af540e43a524763dd53b26a066d472a97c4de37d5498170564510608250c3"
			}
		};
		public static readonly Abi Abi = EverPackageManager.LoadAbi("GiverV2").GetAwaiter().GetResult();
	}

	public static class Packages {
		public static readonly Package Events = EverPackageManager.LoadPackage("Events").GetAwaiter().GetResult();
		public static readonly Package Hello = EverPackageManager.LoadPackage("Hello").GetAwaiter().GetResult();

		public static readonly Package Subscription =
			EverPackageManager.LoadPackage("Subscription").GetAwaiter().GetResult();

		public static readonly Package TestDebotTarget =
			EverPackageManager.LoadPackage("testDebotTarget").GetAwaiter().GetResult();

		public static readonly Package TestDebot =
			EverPackageManager.LoadPackage("testDebot").GetAwaiter().GetResult();
	}
}
