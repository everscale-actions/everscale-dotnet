using System.Threading;
using System.Threading.Tasks;
using EverscaleNet.Abstract;
using EverscaleNet.Client.Models;
using EverscaleNet.Serialization;
using EverscaleNet.TestsShared;

namespace EverscaleNet.Client.Tests.Utils;

public static class EverClientExtensions {
	// todo: migrate to new giver contract after next SDK Release 
	// https://t.me/ton_sdk/4616
	// https://t.me/ton_sdk/4609
	private static readonly SemaphoreSlim WorkaroundOldGiverSemaphore = new(1, 1);

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
		account ??= TestsEnv.LocalGiverAddress;

		var processMessageParams = new ParamsOfProcessMessage {
			MessageEncodeParams = new ParamsOfEncodeMessage {
				Address = TestsEnv.LocalGiverAddress,
				Abi = TestsEnv.Packages.GiverAbiV1,
				CallSet = new CallSet {
					FunctionName = "sendGrams",
					Input = new { dest = account, amount = 1000000000 }.ToJsonElement()
				},
				Signer = new Signer.None()
			},
			SendEvents = false
		};

		await WorkaroundOldGiverSemaphore.WaitAsync();
		ResultOfProcessMessage resultOfProcessMessage;
		try {
			resultOfProcessMessage = await everClient.Processing.ProcessMessage(processMessageParams);
		} finally {
			WorkaroundOldGiverSemaphore.Release();
		}

		foreach (string outMessage in resultOfProcessMessage.OutMessages) {
			ResultOfParse parseResult = await everClient.Boc.ParseMessage(new ParamsOfParse {
				Boc = outMessage
			});
			var parsedPrototype = new { type = default(int), id = default(string) };
			var parsedMessage = parseResult.Parsed!.Value.ToAnonymous(parsedPrototype);

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