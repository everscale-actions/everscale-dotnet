using EverscaleNet.Abstract;
using EverscaleNet.Client.Models;
using EverscaleNet.Models;
using EverscaleNet.Serialization;
using EverscaleNet.Utils;

namespace MessageSenderService;

public class Worker : BackgroundService {
	private const string Mnemonic = "spin tilt boss upper random exit spice ankle leave grief short clever";

	private const string SenderContractName = "15_MessageSender";
	private const string ReceiverContractName = "15_MessageReceiver";
	private readonly IEverClient _everClient;
	private readonly ILogger<Worker> _logger;
	private readonly IEverPackageManager _packageManager;

	public Worker(ILogger<Worker> logger, IEverClient everClient, IEverPackageManager packageManager) {
		_logger = logger;
		_everClient = everClient;
		_packageManager = packageManager;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
		while (!stoppingToken.IsCancellationRequested) {
			try {
				// load contracts from abi.json and tvc files 
				IPackage senderContract = await _packageManager.LoadPackage(SenderContractName, stoppingToken);
				IPackage receiverContract = await _packageManager.LoadPackage(ReceiverContractName, stoppingToken);

				// get keys by mnemonic
				KeyPair keys =
					await _everClient.Crypto.MnemonicDeriveSignKeys(
						new ParamsOfMnemonicDeriveSignKeys { Phrase = Mnemonic }, stoppingToken);

				// ensure that balance of sender address is good and contract has been already deployed
				string senderAddress = await CheckBalanceAndDeploy(senderContract, keys, stoppingToken);

				// send message to receiver
				await SendMessage(senderContract, receiverContract, keys, senderAddress, stoppingToken);

				// get sent messages count
				ulong count = await GetSentMessagesCount(senderContract, keys, senderAddress, stoppingToken);

				_logger.LogInformation("Total sent messages: {Count} Repeat again in 10 sec...", count);
			} catch (Exception e) {
				_logger.LogError(e, "Something went wrong. Will try again in 10 sec...");
			} finally {
				await Task.Delay(10000, stoppingToken);
			}
		}
	}

	private async Task<ulong> GetSentMessagesCount(IPackage contract, KeyPair keys, string senderAddress,
	                                               CancellationToken cancellationToken) {
		ResultOfQueryCollection accountBocResult = await _everClient.Net.QueryCollection(new ParamsOfQueryCollection {
			Collection = "accounts",
			Filter = new { id = new { eq = senderAddress } }.ToJsonElement(),
			Result = "boc",
			Limit = 1
		}, cancellationToken);

		var accountBoc = accountBocResult.Result[0].Get<string>("boc");

		ResultOfEncodeMessage getCountEncodedMessage = await _everClient.Abi.EncodeMessage(new ParamsOfEncodeMessage {
			Address = senderAddress,
			Abi = contract.Abi,
			CallSet = new CallSet { FunctionName = "getCounter" },
			Signer = new Signer.Keys { KeysAccessor = keys }
		}, cancellationToken);

		ResultOfRunTvm result = await _everClient.Tvm.RunTvm(new ParamsOfRunTvm {
			Abi = contract.Abi,
			Account = accountBoc,
			Message = getCountEncodedMessage.Message
		}, cancellationToken);

		return result.Decoded.Output!.Get<string>("c").HexToDec();
	}

	private async Task SendMessage(IPackage senderContract, IPackage receiverContract, KeyPair keys,
	                               string senderAddress,
	                               CancellationToken cancellationToken) {
		ResultOfEncodeMessage encodedMessage = await _everClient.Abi.EncodeMessage(new ParamsOfEncodeMessage {
			Abi = receiverContract.Abi,
			DeploySet = new DeploySet { Tvc = receiverContract.Tvc },
			Signer = new Signer.Keys { KeysAccessor = keys }
		}, cancellationToken);

		var sendMessageEncoded = new ParamsOfEncodeMessage {
			Abi = senderContract.Abi,
			Address = senderAddress,
			CallSet = new CallSet {
				FunctionName = "sendMessage",
				Input = new { anotherContract = encodedMessage.Address }.ToJsonElement()
			},
			Signer = new Signer.Keys { KeysAccessor = keys }
		};

		await _everClient.ProcessAndWaitInternalMessages(sendMessageEncoded, cancellationToken);
	}

	private async Task<string> CheckBalanceAndDeploy(IPackage package, KeyPair keys,
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

		if (result.Result.Length == 0 || result.Result[0].Get<string>("balance").NanoToCoins() < 10m) {
			await SendGramsFromGiver(encoded.Address, cancellationToken);
		}

		try {
			await _everClient.ProcessAndWaitInternalMessages(deployParams, cancellationToken);
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
					value = 100m.CoinsToNano(),
					bounce = false
				}.ToJsonElement()
			},
			Signer = SeGiver.Signer
		};

		await _everClient.ProcessAndWaitInternalMessages(sendGramsEncodedMessage, cancellationToken);
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
