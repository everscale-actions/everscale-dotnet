using System;
using System.IO;
using System.Reflection;
using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client;
using ch1seL.TonNet.Client.Models;
using ch1seL.TonNet.Client.PackageManager;
using Microsoft.Extensions.Options;

namespace ch1seL.TonNet.TestsShared;

public static class TestsEnv {
	public const string LocalGiverAddress = "0:841288ed3b55d9cdafa806807f02a0ae0c169aa5edfe88a789a6482429756a94";
	private const int DefaultAbiVersion = 2;
	private const string DefaultTonNetworkAddress = "http://localhost:8080";
	private const string ContractsPath = "_contracts";
	private static readonly ITonPackageManager TonPackageManager = new FilePackageManager(Options.Create(
		                                                                                      new FilePackageManagerOptions
			                                                                                      { PackagesPath = Path.Join(ContractsPath, $"abi_v{CurrentAbiVersion}") }));
	private static readonly ITonPackageManager TonPackageManagerAbi1 = new FilePackageManager(Options.Create(
		                                                                                          new FilePackageManagerOptions
			                                                                                          { PackagesPath = Path.Join(ContractsPath, "abi_v1") }));
	public static readonly string TonNetworkAddress =
		Environment.GetEnvironmentVariable("TON_NETWORK_ADDRESS") ?? DefaultTonNetworkAddress;

	public static readonly string SdkVersion =
		typeof(SdkVersionAttribute).Assembly!.GetCustomAttribute<SdkVersionAttribute>()?.SdkVersion;

	private static int CurrentAbiVersion =>
		int.TryParse(Environment.GetEnvironmentVariable("ABI_VERSION"), out int version)
			? version == 0 ? DefaultAbiVersion : version
			: DefaultAbiVersion;

	public static class Packages {
		public static readonly Package Events = TonPackageManager.LoadPackage("Events").GetAwaiter().GetResult();
		public static readonly Package Hello = TonPackageManager.LoadPackage("Hello").GetAwaiter().GetResult();

		public static readonly Package Subscription =
			TonPackageManager.LoadPackage("Subscription").GetAwaiter().GetResult();

		public static readonly Abi GiverAbiV1 = TonPackageManagerAbi1.LoadAbi("Giver").GetAwaiter().GetResult();

		public static readonly Package TestDebotTarget =
			TonPackageManager.LoadPackage("testDebotTarget").GetAwaiter().GetResult();

		public static readonly Package TestDebot =
			TonPackageManager.LoadPackage("testDebot").GetAwaiter().GetResult();
	}
}