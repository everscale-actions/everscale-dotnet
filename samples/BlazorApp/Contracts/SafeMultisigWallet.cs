using EverscaleNet;

namespace BlazorApp.Contracts;

public class SafeMultisigWallet : AccountBase {
	private const string Transfer = "transfer";

	private readonly IEverClient _everClient;
	private readonly IEverPackageManager _everPackageManager;

	public SafeMultisigWallet(IEverClient everClient, IEverPackageManager everPackageManager) : base(everClient, everPackageManager) {
		_everClient = everClient;
		_everPackageManager = everPackageManager;
	}

	protected override string Name => "SafeMultisigWallet";

	public async Task SendMessage(string phrase, string recipient, string message) {
		Package contract = await _everPackageManager.LoadPackage(Name);
		Abi transferAbi = await _everPackageManager.LoadAbi(Transfer);
		KeyPair keyPair = await _everClient.Crypto.MnemonicDeriveSignKeys(new ParamsOfMnemonicDeriveSignKeys { Phrase = phrase });
		await Init(keyPair.Public);
		if (await GetAccountType() == AccountType.Uninit) {
			await Deploy(phrase);
		}
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
				Address = Address,
				CallSet = new CallSet {
					FunctionName = "submitTransaction",
					Input = new {
						dest = recipient,
						value = 0.1M.CoinsToNano(),
						bounce = false,
						allBalance = false,
						payload = body.Body
					}.ToJsonElement()
				},
				Signer = new Signer.Keys { KeysAccessor = keyPair }
			}
		});
	}
}
