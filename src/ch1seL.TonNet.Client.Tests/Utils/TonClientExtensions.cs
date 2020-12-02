using System.Threading.Tasks;
using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Models;
using ch1seL.TonNet.Serialization;
using ch1seL.TonNet.TestsShared;

namespace ch1seL.TonNet.Client.Tests.Utils
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

        /// <summary>
        ///     send 100000000 tons to account
        /// </summary>
        /// <param name="tonClient"></param>
        /// <param name="account">the giver sends money to himself by default</param>
        public static async Task SendGramsFromLocalGiver(this ITonClient tonClient, string account = null)
        {
            account ??= LocalGiverAddress;

            var processMessageParams = new ParamsOfProcessMessage
            {
                MessageEncodeParams = new ParamsOfEncodeMessage
                {
                    Address = LocalGiverAddress,
                    Abi = TestsEnv.Packages.GiverAbiV1,
                    CallSet = new CallSet
                    {
                        FunctionName = "sendGrams",
                        Input = new {dest = account, amount = 100000000}.ToJsonElement()
                    },
                    Signer = new Signer.None()
                },
                SendEvents = false
            };
            ResultOfProcessMessage resultOfProcessMessage = await tonClient.Processing.ProcessMessage(processMessageParams);

            foreach (var outMessage in resultOfProcessMessage.OutMessages)
            {
                ResultOfParse parseResult = await tonClient.Boc.ParseMessage(new ParamsOfParse
                {
                    Boc = outMessage
                });
                var parsedPrototype = new {type = default(int), id = default(string)};
                var parsedMessage = parseResult.Parsed!.Value.ToAnonymous(parsedPrototype);

                if (parsedMessage.type == 0)
                    await tonClient.Net.WaitForCollection(new ParamsOfWaitForCollection
                    {
                        Collection = "transactions",
                        Filter = new {in_msg = new {eq = parsedMessage.id}}.ToJsonElement(),
                        Result = "id"
                    });
            }
        }

        public static async Task<string> DeployWithGiver(this ITonClient tonClient, ParamsOfEncodeMessage encodeMessageParams)
        {
            ResultOfEncodeMessage address = await tonClient.Abi.EncodeMessage(encodeMessageParams);
            await tonClient.SendGramsFromLocalGiver(address.Address);
            await tonClient.Processing.ProcessMessage(new ParamsOfProcessMessage {MessageEncodeParams = encodeMessageParams});
            return address.Address;
        }
    }
}