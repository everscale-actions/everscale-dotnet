using System;
using System.IO;
using System.Reflection;
using EverscaleNet.Abstract;
using EverscaleNet.Client.Models;
using EverscaleNet.Client.PackageManager;
using EverscaleNet.Models;
using EverscaleNet.Utils;
using Microsoft.Extensions.Options;

namespace EverscaleNet.TestsShared;

public static class TestsEnv {
	public const string LocalGiverAddress = "0:841288ed3b55d9cdafa806807f02a0ae0c169aa5edfe88a789a6482429756a94";
	private const int DefaultAbiVersion = 2;
	private const string ContractsPath = "_contracts";
	private const string NetworkEndpoints = "EVERSCALE_NETWORK_ENDPOINTS";

	private static readonly IEverPackageManager EverPackageManager = new FilePackageManager(Options.Create(
		                                                                                        new FilePackageManagerOptions
			                                                                                        { PackagesPath = Path.Join(ContractsPath, $"abi_v{CurrentAbiVersion}") }));
	private static readonly IEverPackageManager EverPackageManagerAbi1 = new FilePackageManager(Options.Create(
		                                                                                            new FilePackageManagerOptions
			                                                                                            { PackagesPath = Path.Join(ContractsPath, "abi_v1") }));
	public static readonly string[] EverscaleNetworkEndpoints = Environment.GetEnvironmentVariable(NetworkEndpoints)?.Split(";") ?? EverOS.Endpoints.NodeSE;

	public static readonly string SdkVersion = typeof(SdkVersionAttribute).Assembly!.GetCustomAttribute<SdkVersionAttribute>()?.SdkVersion;

	private static int CurrentAbiVersion => int.TryParse(Environment.GetEnvironmentVariable("ABI_VERSION"), out int version)
		                                        ? version == 0 ? DefaultAbiVersion : version
		                                        : DefaultAbiVersion;

	public static class Packages {
		public static readonly Package Events = EverPackageManager.LoadPackage("Events").GetAwaiter().GetResult();
		public static readonly Package Hello = EverPackageManager.LoadPackage("Hello").GetAwaiter().GetResult();

		public static readonly Package Subscription =
			EverPackageManager.LoadPackage("Subscription").GetAwaiter().GetResult();

		public static readonly Abi GiverAbiV1 = EverPackageManagerAbi1.LoadAbi("Giver").GetAwaiter().GetResult();

		public static readonly Package TestDebotTarget =
			EverPackageManager.LoadPackage("testDebotTarget").GetAwaiter().GetResult();

		public static readonly Package TestDebot =
			EverPackageManager.LoadPackage("testDebot").GetAwaiter().GetResult();
	}
}
