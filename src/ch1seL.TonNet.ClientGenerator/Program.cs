using System.Threading.Tasks;

namespace ch1seL.TonNet.ClientGenerator
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            await Generator.GenerateClient();
        }
    }
}