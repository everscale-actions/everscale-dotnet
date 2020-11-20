using System;

namespace ch1seL.TonNet.TestsShared
{
    internal static class TestsEnvironment
    {
        private const int DefaultAbiVersion = 2;
        private const string DefaultTonNetworkAddress = "http://localhost";
        
        public static readonly int CurrentAbiVersion = int.TryParse(Environment.GetEnvironmentVariable("ABI_VERSION"), out var version) 
            ? version 
            : DefaultAbiVersion;

        public static readonly string TonNetworkAddress = Environment.GetEnvironmentVariable("TON_NETWORK_ADDRESS") ?? DefaultTonNetworkAddress;
    }
}