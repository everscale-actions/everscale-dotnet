using ch1seL.TonNet.Client.Models;

namespace ch1seL.TonNet.Client.Tests.Modules.DebotModule
{
    public class DebotData
    {
        public string DebotAddr { get; init; }
        public string TargetAddr { get; init; }
        public KeyPair Keys { get; init; }
    }
}