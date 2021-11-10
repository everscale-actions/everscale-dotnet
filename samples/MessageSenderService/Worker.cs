using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client;
using ch1seL.TonNet.Client.Models;
using ch1seL.TonNet.Serialization;
using ch1seL.TonNet.Utils;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MessageSenderService
{
    public class Worker : BackgroundService
    {
        private const string Mnemonic = "spin tilt boss upper random exit spice ankle leave grief short clever";
        private const string GiverAddress = "0:841288ed3b55d9cdafa806807f02a0ae0c169aa5edfe88a789a6482429756a94";
        private const string SenderContractName = "15_MessageSender";
        private const string ReceiverContractName = "15_MessageReceiver";
        private readonly ILogger<Worker> _logger;
        private readonly ITonPackageManager _packageManager;
        private readonly ITonClient _tonClient;

        public Worker(ILogger<Worker> logger, ITonClient tonClient, ITonPackageManager packageManager)
        {
            _logger = logger;
            _tonClient = tonClient;
            _packageManager = packageManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
                try
                {
                    // load contracts from abi.json and tvc files 
                    Package senderContract = await _packageManager.LoadPackage(SenderContractName);
                    Package receiverContract = await _packageManager.LoadPackage(ReceiverContractName);

                    // get keys by mnemonic
                    KeyPair keys =
                        await _tonClient.Crypto.MnemonicDeriveSignKeys(
                            new ParamsOfMnemonicDeriveSignKeys { Phrase = Mnemonic }, stoppingToken);

                    // ensure that balance of sender address is good and contract has been already deployed
                    var senderAddress = await CheckBalanceAndDeploy(senderContract, keys, stoppingToken);

                    // send message to receiver
                    await SendMessage(senderContract, receiverContract, keys, senderAddress, stoppingToken);

                    // get sent messages count
                    var count = await GetSentMessagesCount(senderContract, keys, senderAddress, stoppingToken);

                    _logger.LogInformation("Total sent messages: {Count} Repeat again in 10 sec...", count);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Something went wrong. Will try again in 10 sec...");
                }
                finally
                {
                    await Task.Delay(10000, stoppingToken);
                }
        }

        private async Task<ulong> GetSentMessagesCount(Package contract, KeyPair keys, string senderAddress,
            CancellationToken cancellationToken)
        {
            ResultOfQueryCollection accountBocResult = await _tonClient.Net.QueryCollection(new ParamsOfQueryCollection
            {
                Collection = "accounts",
                Filter = new { id = new { eq = senderAddress } }.ToJsonElement(),
                Result = "boc",
                Limit = 1
            }, cancellationToken);

            var accountBoc = accountBocResult.Result[0].Get<string>("boc");

            ResultOfEncodeMessage getCountEncodedMessage = await _tonClient.Abi.EncodeMessage(new ParamsOfEncodeMessage
            {
                Address = senderAddress,
                Abi = contract.Abi,
                CallSet = new CallSet { FunctionName = "getCounter" },
                Signer = new Signer.Keys { KeysAccessor = keys }
            }, cancellationToken);

            ResultOfRunTvm result = await _tonClient.Tvm.RunTvm(new ParamsOfRunTvm
            {
                Abi = contract.Abi,
                Account = accountBoc,
                Message = getCountEncodedMessage.Message
            }, cancellationToken);

            return result.Decoded.Output.Get<string>("c").HexToDec();
        }

        private async Task SendMessage(Package senderContract, Package receiverContract, KeyPair keys,
            string senderAddress,
            CancellationToken cancellationToken)
        {
            ResultOfEncodeMessage encodedMessage = await _tonClient.Abi.EncodeMessage(new ParamsOfEncodeMessage
            {
                Abi = receiverContract.Abi,
                DeploySet = new DeploySet { Tvc = receiverContract.Tvc },
                Signer = new Signer.Keys { KeysAccessor = keys }
            }, cancellationToken);

            var sendMessageEncoded = new ParamsOfEncodeMessage
            {
                Abi = senderContract.Abi,
                Address = senderAddress,
                CallSet = new CallSet
                {
                    FunctionName = "sendMessage",
                    Input = new { anotherContract = encodedMessage.Address }.ToJsonElement()
                },
                Signer = new Signer.Keys { KeysAccessor = keys }
            };

            await ProcessAndWaitTransactions(sendMessageEncoded, cancellationToken);
        }

        private async Task<string> CheckBalanceAndDeploy(Package package, KeyPair keys,
            CancellationToken cancellationToken)
        {
            var deployParams = new ParamsOfEncodeMessage
            {
                Abi = package.Abi,
                DeploySet = new DeploySet { Tvc = package.Tvc },
                Signer = new Signer.Keys { KeysAccessor = keys },
                CallSet = new CallSet { FunctionName = "constructor" }
            };

            ResultOfEncodeMessage encoded = await _tonClient.Abi.EncodeMessage(deployParams, cancellationToken);
            ResultOfQueryCollection result = await _tonClient.Net.QueryCollection(new ParamsOfQueryCollection
            {
                Collection = "accounts",
                Filter = new { id = new { eq = encoded.Address } }.ToJsonElement(),
                Result = "balance",
                Limit = 1
            }, cancellationToken);

            if (result.Result.Length == 0 || result.Result[0].Get<string>("balance").HexToDec() < 1_000_000_000_000ul)
                await SendGramsFromGiver(encoded.Address, cancellationToken);

            try
            {
                await ProcessAndWaitTransactions(deployParams, cancellationToken);
            }
            catch (TonClientException e) when (e.Code == 414)
            {
                _logger.LogInformation("Contract already has been deployed");
            }

            return encoded.Address;
        }

        private async Task SendGramsFromGiver(string account, CancellationToken cancellationToken)
        {
            var sendGramsEncodedMessage = new ParamsOfEncodeMessage
            {
                Address = GiverAddress,
                Abi = await _packageManager.LoadAbi("Giver"),
                CallSet = new CallSet
                {
                    FunctionName = "sendGrams",
                    Input = new { dest = account, amount = 100_000_000_000_000ul }.ToJsonElement()
                },
                Signer = new Signer.None()
            };
            await ProcessAndWaitTransactions(sendGramsEncodedMessage, cancellationToken);
        }

        private async Task ProcessAndWaitTransactions(ParamsOfEncodeMessage encodedMessage,
            CancellationToken cancellationToken)
        {
            ResultOfProcessMessage resultOfProcessMessage = await _tonClient.Processing.ProcessMessage(
                new ParamsOfProcessMessage
                {
                    MessageEncodeParams = encodedMessage
                }, cancellationToken: cancellationToken);
            await Task.WhenAll(resultOfProcessMessage.OutMessages.Select(async message =>
            {
                ResultOfParse parseResult =
                    await _tonClient.Boc.ParseMessage(new ParamsOfParse { Boc = message }, cancellationToken);
                var parsedPrototype = new { msg_type = default(int), id = default(string) };
                var parsedMessage = parseResult.Parsed!.Value.ToAnonymous(parsedPrototype);
                if (parsedMessage.msg_type == 0)
                    await _tonClient.Net.WaitForCollection(new ParamsOfWaitForCollection
                    {
                        Collection = "transactions",
                        Filter = new { in_msg = new { eq = parsedMessage.id } }.ToJsonElement(),
                        Result = "id"
                    }, cancellationToken);
            }));
        }
    }
}