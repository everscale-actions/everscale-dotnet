using ch1seL.TonNet.Client.Models;

namespace ch1seL.TonNet.Debot.Models
{
    public class DebotData
    {
        public string DebotAddr { get; set; }
        public string TargetAddr { get; set; }
        public KeyPair Keys { get; set; }
        public string Abi { get; set; }
    }
}