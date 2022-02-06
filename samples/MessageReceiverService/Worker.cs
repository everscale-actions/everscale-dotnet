using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EverscaleNet.Abstract;
using EverscaleNet.Client.Models;
using EverscaleNet.Models;
using EverscaleNet.Serialization;
using EverscaleNet.Utils;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MessageReceiverService;

public class Worker : BackgroundService {
	private const string Mnemonic = "spin tilt boss upper random exit spice ankle leave grief short clever";
	private const string ReceiverContractName = "15_MessageReceiver";
	private readonly ILogger<Worker> _logger;
	private readonly IEverPackageManager _packageManager;
	private readonly IEverClient _everClient;

	public Worker(ILogger<Worker> logger, IEverClient everClient, IEverPackageManager packageManager) {
		_logger = logger;
		_everClient = everClient;
		_packageManager = packageManager;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
		while (!stoppingToken.IsCancellationRequested) {
			try {
				// load contracts from abi.json and tvc files 
				// Package senderContract = await _packageManager.LoadPackage(SenderContractName);
				Package receiverContract = await _packageManager.LoadPackage(ReceiverContractName, stoppingToken);

				// get keys by mnemonic
				KeyPair keys =
					await _everClient.Crypto.MnemonicDeriveSignKeys(
						new ParamsOfMnemonicDeriveSignKeys { Phrase = Mnemonic }, stoppingToken);

				// ensure that balance of receiver address is good and contract has been already deployed
				string receiverAddress = await CheckBalanceAndDeploy(receiverContract, keys, stoppingToken);

				// get received messages count
				ulong count = await GetReceivedMessagesCount(receiverContract, keys, receiverAddress, stoppingToken);

				_logger.LogInformation("Total received messages: {Count} Repeat again in 10 sec...", count);
			} catch (Exception e) {
				_logger.LogError(e, "Something went wrong. Will try again in 10 sec...");
			} finally {
				await Task.Delay(10000, stoppingToken);
			}
		}
	}

	private async Task<ulong> GetReceivedMessagesCount(Package contract, KeyPair keys, string address,
	                                                   CancellationToken cancellationToken) {
		ResultOfQueryCollection accountBocResult = await _everClient.Net.QueryCollection(new ParamsOfQueryCollection {
			Collection = "accounts",
			Filter = new { id = new { eq = address } }.ToJsonElement(),
			Result = "boc",
			Limit = 1
		}, cancellationToken);

		var accountBoc = accountBocResult.Result[0].Get<string>("boc");

		ResultOfEncodeMessage getCountEncodedMessage = await _everClient.Abi.EncodeMessage(new ParamsOfEncodeMessage {
			Address = address,
			Abi = contract.Abi,
			CallSet = new CallSet { FunctionName = "getCounter" },
			Signer = new Signer.Keys { KeysAccessor = keys }
		}, cancellationToken);

		ResultOfRunTvm result = await _everClient.Tvm.RunTvm(new ParamsOfRunTvm {
			Abi = contract.Abi,
			Account = accountBoc,
			Message = getCountEncodedMessage.Message
		}, cancellationToken);

		return result.Decoded.Output.Get<string>("c").HexToDec();
	}

	private async Task<string> CheckBalanceAndDeploy(Package package, KeyPair keys,
	                                                 CancellationToken cancellationToken) {
		var deployParams = new ParamsOfEncodeMessage {
			Abi = package.Abi,
			DeploySet = new DeploySet { Tvc = package.Tvc },
			Signer = new Signer.Keys { KeysAccessor = keys },
			CallSet = new CallSet { FunctionName = "constructor" }
		};

		ResultOfEncodeMessage encoded = await _everClient.Abi.EncodeMessage(deployParams, cancellationToken);
		ResultOfQueryCollection result = await _everClient.Net.QueryCollection(new ParamsOfQueryCollection {
			Collection = "accounts",
			Filter = new { id = new { eq = encoded.Address } }.ToJsonElement(),
			Result = "balance(format: DEC)",
			Limit = 1
		}, cancellationToken);

		if (result.Result.Length == 0 || result.Result[0].Get<string>("balance").DecToBalance() < 10m) {
			await SendGramsFromGiver(encoded.Address, cancellationToken);
		}

		try {
			await ProcessAndWaitTransactions(deployParams, cancellationToken);
		} catch (EverClientException e) when (e.Code == 414) {
			_logger.LogInformation("Contract already has been deployed");
		}

		return encoded.Address;
	}

	private async Task SendGramsFromGiver(string account, CancellationToken cancellationToken) {
		var sendGramsEncodedMessage = new ParamsOfEncodeMessage {
			Address = SeGiver.Address,
			Abi = await _packageManager.LoadAbi("GiverV2", cancellationToken),
			CallSet = new CallSet {
				FunctionName = "sendTransaction",
				Input = new {
					dest = account ?? SeGiver.Address,
					value = 100_000_000_000_000,
					bounce = false
				}.ToJsonElement()
			},
			Signer = SeGiver.Signer
		};
		await ProcessAndWaitTransactions(sendGramsEncodedMessage, cancellationToken);
	}

	private async Task ProcessAndWaitTransactions(ParamsOfEncodeMessage encodedMessage,
	                                              CancellationToken cancellationToken) {
		ResultOfProcessMessage resultOfProcessMessage = await _everClient.Processing.ProcessMessage(
			                                                new ParamsOfProcessMessage {
				                                                MessageEncodeParams = encodedMessage
			                                                }, cancellationToken: cancellationToken);

		await Task.WhenAll(resultOfProcessMessage.OutMessages.Select(async message => {
			ResultOfParse parseResult =
				await _everClient.Boc.ParseMessage(new ParamsOfParse { Boc = message }, cancellationToken);
			var parsedPrototype = new { type = default(int), id = default(string) };
			var parsedMessage = parseResult.Parsed!.Value.ToAnonymous(parsedPrototype);
			if (parsedMessage.type == 0) {
				await _everClient.Net.WaitForCollection(new ParamsOfWaitForCollection {
					Collection = "transactions",
					Filter = new { in_msg = new { eq = parsedMessage.id } }.ToJsonElement(),
					Result = "id"
				}, cancellationToken);
			}
		}));
	}

	private static class SeGiver {
		public const string Address = "0:ece57bcc6c530283becbbd8a3b24d3c5987cdddc3c8b7b33be6e4a6312490415";
		public static readonly Signer Signer = new Signer.Keys {
			KeysAccessor = new KeyPair {
				Public = "2ada2e65ab8eeab09490e3521415f45b6e42df9c760a639bcf53957550b25a16",
				Secret = "172af540e43a524763dd53b26a066d472a97c4de37d5498170564510608250c3"
			}
		};
	}
}
