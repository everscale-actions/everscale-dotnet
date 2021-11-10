using System.Numerics;
using System.Text;
using System.Text.Json;
using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client;
using ch1seL.TonNet.Client.Models;
using ch1seL.TonNet.Serialization;

namespace BlazorWasm6;

public class MessageSender
{
    private const string SafeMultisigWallet = "SafeMultisigWallet";
    private const string Transfer = "transfer";

    private readonly ITonClient _tonClient;
    private readonly ITonPackageManager _tonPackageManager;

    public MessageSender(ITonClient tonClient, ITonPackageManager tonPackageManager)
    {
        _tonClient = tonClient;
        _tonPackageManager = tonPackageManager;
    }

    public async Task SendMessage(string phrase, string recipient, string message)
    {
        Package contract = await _tonPackageManager.LoadPackage(SafeMultisigWallet);
        Abi transferAbi = await _tonPackageManager.LoadAbi(Transfer);

        (var address, KeyPair keyPair) = await Deploy(phrase);

        ResultOfEncodeMessageBody body = await _tonClient.Abi.EncodeMessageBody(new ParamsOfEncodeMessageBody
        {
            Abi = transferAbi,
            CallSet = new CallSet
            {
                FunctionName = "transfer",
                Input = new { comment = ToHexString(message) }.ToJsonElement()
            },
            IsInternal = true,
            Signer = new Signer.None()
        });

        await _tonClient.Processing.ProcessMessage(new ParamsOfProcessMessage
        {
            SendEvents = false,
            MessageEncodeParams = new ParamsOfEncodeMessage
            {
                Abi = contract.Abi,
                Address = address,
                CallSet = new CallSet
                {
                    FunctionName = "submitTransaction",
                    Input = new
                    {
                        dest = recipient,
                        value = 100_000_000,
                        bounce = false,
                        allBalance = false,
                        payload = body.Body
                    }.ToJsonElement()
                },
                Signer = new Signer.Keys { KeysAccessor = keyPair }
            }
        });
    }

    private async Task<(string, KeyPair)> Deploy(string phrase)
    {
        KeyPair keyPair =
            await _tonClient.Crypto.MnemonicDeriveSignKeys(new ParamsOfMnemonicDeriveSignKeys { Phrase = phrase });

        Package contract = await _tonPackageManager.LoadPackage(SafeMultisigWallet);

        var paramsOfEncodedMessage = new ParamsOfEncodeMessage
        {
            Abi = contract.Abi,
            DeploySet = new DeploySet
            {
                Tvc = contract.Tvc,
                InitialData = new { }.ToJsonElement()
            },
            CallSet = new CallSet
            {
                FunctionName = "constructor",
                Input = new { owners = new[] { $"0x{keyPair.Public}" }, reqConfirms = 0 }.ToJsonElement()
            },
            Signer = new Signer.Keys { KeysAccessor = keyPair },
            ProcessingTryIndex = 1
        };

        ResultOfEncodeMessage encodeMessage = await _tonClient.Abi.EncodeMessage(paramsOfEncodedMessage);
        var address = encodeMessage.Address;

        ResultOfQueryCollection result = await _tonClient.Net.QueryCollection(new ParamsOfQueryCollection
        {
            Collection = "accounts",
            Filter = new { id = new { eq = address } }.ToJsonElement(),
            Result = "acc_type balance"
        });

        if (result.Result.Length == 0)
            throw new Exception($"You need to transfer at least 0.5 tokens for deploy to {address}");

        JsonElement account = result.Result[0];
        var balance = ToDecimalBalance(new BigInteger(Convert.ToUInt64(account.Get<string>("balance"), 16)));
        var accType = account.Get<int>("acc_type");
        switch (accType)
        {
            case 0 when balance < (decimal)0.5:
                throw new Exception($"You need to transfer at least 0.5 tokens for deploy to {address}");
            case 1:
                return (address, keyPair);
            default:
                await _tonClient.Processing.ProcessMessage(new ParamsOfProcessMessage
                {
                    SendEvents = false,
                    MessageEncodeParams = paramsOfEncodedMessage
                });
                return (address, keyPair);
        }
    }

    private static decimal ToDecimalBalance(BigInteger bigInteger)
    {
        return Math.Round((decimal)BigInteger.Divide(bigInteger, 1_000_000) / 1000, 2);
    }

    private static string ToHexString(string input)
    {
        return BitConverter.ToString(Encoding.Default.GetBytes(input)).Replace("-", string.Empty);
    }
}