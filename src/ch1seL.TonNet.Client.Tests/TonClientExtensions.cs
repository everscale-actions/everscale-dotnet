using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ch1seL.TonNet.Client.Models;
using ch1seL.TonNet.Serialization;
using TestsShared;

namespace ch1seL.TonNet.Client.Tests
{
    public static class TonClientExtensions
    {
        private const string LocalGiverAddress = "0:841288ed3b55d9cdafa806807f02a0ae0c169aa5edfe88a789a6482429756a94";

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

        public static async Task SendGramsFromLocalGiver(this ITonClient tonClient, string account, ulong? value = 100000000)
        {
            Abi giverAbi = await TestPackage.GetAbi("Giver", 1);
            var processMessageParams = new ParamsOfProcessMessage
            {
                MessageEncodeParams = new ParamsOfEncodeMessage
                {
                    Address = LocalGiverAddress,
                    Abi = giverAbi,
                    CallSet = new CallSet
                    {
                        FunctionName = "sendGrams",
                        Input = new {dest = account, amount = value}.ToJsonElement()
                    },
                    Signer = new Signer.None()
                },
                SendEvents = false
            };

            ResultOfProcessMessage runResult = await tonClient.Processing.ProcessMessage(processMessageParams, null);

            foreach (var outMessage in runResult.OutMessages)
            {
                ResultOfParse parsed = await tonClient.Boc.ParseMessage(new ParamsOfParse
                {
                    Boc = outMessage
                });

                var message = parsed.Parsed!.Value.ToObject<TestsMessage>();
                if (message.Type == 0)
                    await tonClient.Net.WaitForCollection(new ParamsOfWaitForCollection
                    {
                        Collection = "transactions",
                        Filter = new {in_msg = new {eq = message.Id}}.ToJsonElement(),
                        Result = "id"
                    });
            }
        }
    }

    public class TestsMessage
    {
        [JsonPropertyName("type")] public int Type { get; set; }

        [JsonPropertyName("id")] public string Id { get; set; }
    }
}