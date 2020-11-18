using System.Threading.Tasks;
using ch1seL.TonNet.Client.Models;

namespace ch1seL.TonNet.Client.Extensions
{
    public static class TonClientExtensions
    {
        public static async Task<string> SignDetached(this ITonClient tonClient, KeyPair pair, string data)
        {
            KeyPair keys = await tonClient.Crypto.NaclSignKeypairFromSecretKey(new ParamsOfNaclSignKeyPairFromSecret
            {
                Secret = pair.Secret
            });

            ResultOfNaclSignDetached result = await tonClient.Crypto.NaclSignDetached(new ParamsOfNaclSign
            {
                Secret = keys.Secret,
                Unsigned = data
            });

            return result.Signature;
        }
    }
}