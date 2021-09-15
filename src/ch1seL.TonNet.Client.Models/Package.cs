using ch1seL.TonNet.Client.Models;

namespace ch1seL.TonNet.Client
{
    public class Package
    {
        public Package(Abi abi, string tvc)
        {
            Abi = abi;
            Tvc = tvc;
        }

        public Abi Abi { get; }
        public string Tvc { get; }
    }
}