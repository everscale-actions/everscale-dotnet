using System;
using System.IO;
using System.Reflection;
using ch1seL.TonNet.Client.Models;
using ch1seL.TonNet.Client.PackageManager;
using Microsoft.Extensions.Options;

namespace ch1seL.TonNet.TestsShared
{
    internal static class TestsEnv
    {
        private const int DefaultAbiVersion = 2;
        private const string DefaultTonNetworkAddress = "http://localhost";
        private const string ContractsPath = "_contracts";

        private static readonly ITonPackageManager TonPackageManager = new FilePackageManager(Options.Create(new PackageManagerOptions
            {PackagesPath = Path.Join(ContractsPath, $"abi_v{CurrentAbiVersion}")}));

        private static readonly ITonPackageManager TonPackageManagerAbi1 = new FilePackageManager(Options.Create(new PackageManagerOptions
            {PackagesPath = Path.Join(ContractsPath, "abi_v1")}));

        public static readonly string TonNetworkAddress = Environment.GetEnvironmentVariable("TON_NETWORK_ADDRESS") ?? DefaultTonNetworkAddress;

        public static readonly string SdkVersion = typeof(SdkVersionAttribute).Assembly!.GetCustomAttribute<SdkVersionAttribute>()?.SdkVersion;

        private static int CurrentAbiVersion => int.TryParse(Environment.GetEnvironmentVariable("ABI_VERSION"), out var version)
            ? version == 0 ? DefaultAbiVersion : version
            : DefaultAbiVersion;

        public static class Packages
        {
            public static readonly Package Events = TonPackageManager.LoadPackage("Events").GetAwaiter().GetResult();
            public static readonly Package Hello = TonPackageManager.LoadPackage("Hello").GetAwaiter().GetResult();
            public static readonly Package Subscription = TonPackageManager.LoadPackage("Subscription").GetAwaiter().GetResult();
            public static readonly Abi GiverAbiV1 = TonPackageManagerAbi1.LoadAbi("Giver").GetAwaiter().GetResult();
        }
    }
}