namespace EverscaleNet.Client.Tests.Utils;

public static class EverClientExtensions {
	public static async Task<string> SignDetached(this IEverClient everClient, KeyPair pair, string data) {
		KeyPair keys = await everClient.Crypto.NaclSignKeypairFromSecretKey(new ParamsOfNaclSignKeyPairFromSecret {
			Secret = pair.Secret
		});

		ResultOfNaclSignDetached result = await everClient.Crypto.NaclSignDetached(new ParamsOfNaclSign {
			Secret = keys.Secret,
			Unsigned = data
		});

		return result.Signature;
	}

	/// <summary>
	///     send 100000000 tons to account
	/// </summary>
	/// <param name="everClient"></param>
	/// <param name="account">the giver sends money to himself by default</param>
	public static async Task SendGramsFromLocalGiver(this IEverClient everClient, string account = null) {
		var processMessageParams = new ParamsOfProcessMessage {
			MessageEncodeParams = new ParamsOfEncodeMessage {
				Address = TestsEnv.SeGiver.Address,
				Abi = TestsEnv.SeGiver.Abi,
				CallSet = new CallSet {
					FunctionName = "sendTransaction",
					Input = new {
						dest = account ?? TestsEnv.SeGiver.Address,
						value = 1_000_000_000,
						bounce = false
					}.ToJsonElement()
				},
				Signer = TestsEnv.SeGiver.Signer
			},
			SendEvents = false
		};

		ResultOfProcessMessage resultOfProcessMessage = await everClient.Processing.ProcessMessage(processMessageParams);

		foreach (string outMessage in resultOfProcessMessage.OutMessages) {
			ResultOfParse parseResult = await everClient.Boc.ParseMessage(new ParamsOfParse {
				Boc = outMessage
			});
			var parsedPrototype = new { type = 0, id = string.Empty };
			var parsedMessage = parseResult.Parsed!.ToPrototype(parsedPrototype);

			if (parsedMessage.type == 0) {
				await everClient.Net.WaitForCollection(new ParamsOfWaitForCollection {
					Collection = "transactions",
					Filter = new { in_msg = new { eq = parsedMessage.id } }.ToJsonElement(),
					Result = "id"
				});
			}
		}
	}

	public static async Task<string> DeployWithGiver(this IEverClient everClient, ParamsOfEncodeMessage encodeMessageParams) {
		ResultOfEncodeMessage address = await everClient.Abi.EncodeMessage(encodeMessageParams);
		await everClient.SendGramsFromLocalGiver(address.Address);
		await everClient.Processing.ProcessMessage(new ParamsOfProcessMessage { MessageEncodeParams = encodeMessageParams });
		return address.Address;
	}
}
