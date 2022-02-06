using System.Text.Json;
using EverscaleNet.Abstract;
using EverscaleNet.Client.Models;
using EverscaleNet.Models;
using EverscaleNet.Serialization;
using EverscaleNet.Utils;

namespace BlazorWasm6;

public class MessageSender {
	private const string SafeMultisigWallet = "SafeMultisigWallet";
	private const string Transfer = "transfer";

	private readonly IEverClient _everClient;
	private readonly IEverPackageManager _everPackageManager;

	public MessageSender(IEverClient everClient, IEverPackageManager everPackageManager) {
		_everClient = everClient;
		_everPackageManager = everPackageManager;
	}

	public async Task SendMessage(string phrase, string recipient, string message) {
		Package contract = await _everPackageManager.LoadPackage(SafeMultisigWallet);
		Abi transferAbi = await _everPackageManager.LoadAbi(Transfer);

		(string address, KeyPair keyPair) = await Deploy(phrase);

		ResultOfEncodeMessageBody body = await _everClient.Abi.EncodeMessageBody(new ParamsOfEncodeMessageBody {
			Abi = transferAbi,
			CallSet = new CallSet {
				FunctionName = "transfer",
				Input = new { comment = message.ToHexString() }.ToJsonElement()
			},
			IsInternal = true,
			Signer = new Signer.None()
		});

		await _everClient.Processing.ProcessMessage(new ParamsOfProcessMessage {
			SendEvents = false,
			MessageEncodeParams = new ParamsOfEncodeMessage {
				Abi = contract.Abi,
				Address = address,
				CallSet = new CallSet {
					FunctionName = "submitTransaction",
					Input = new {
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

	private async Task<(string, KeyPair)> Deploy(string phrase) {
		KeyPair keyPair =
			await _everClient.Crypto.MnemonicDeriveSignKeys(new ParamsOfMnemonicDeriveSignKeys { Phrase = phrase });

		Package contract = await _everPackageManager.LoadPackage(SafeMultisigWallet);

		var paramsOfEncodedMessage = new ParamsOfEncodeMessage {
			Abi = contract.Abi,
			DeploySet = new DeploySet {
				Tvc = contract.Tvc,
				InitialData = new { }.ToJsonElement()
			},
			CallSet = new CallSet {
				FunctionName = "constructor",
				Input = new { owners = new[] { $"0x{keyPair.Public}" }, reqConfirms = 0 }.ToJsonElement()
			},
			Signer = new Signer.Keys { KeysAccessor = keyPair },
			ProcessingTryIndex = 1
		};

		ResultOfEncodeMessage encodeMessage = await _everClient.Abi.EncodeMessage(paramsOfEncodedMessage);
		string address = encodeMessage.Address;

		ResultOfQueryCollection result = await _everClient.Net.QueryCollection(new ParamsOfQueryCollection {
			Collection = "accounts",
			Filter = new { id = new { eq = address } }.ToJsonElement(),
			Result = "acc_type balance(format: DEC)",
			Limit = 1
		});

		if (result.Result.Length == 0) {
			throw new Exception($"You need to transfer at least 0.5 tokens for deploy to {address}");
		}

		JsonElement account = result.Result[0];
		decimal balance = account.Get<string>("balance").DecBalanceToCoins();
		var accType = account.Get<int>("acc_type");
		switch (accType) {
			case 0 when balance < 0.5m:
				throw new Exception($"You need to transfer at least 0.5 tokens for deploy to {address}");
			case 1:
				return (address, keyPair);
			default:
				await _everClient.Processing.ProcessMessage(new ParamsOfProcessMessage {
					SendEvents = false,
					MessageEncodeParams = paramsOfEncodedMessage
				});
				return (address, keyPair);
		}
	}
}
