using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using EverscaleNet.Abstract;
using EverscaleNet.Client.Models;
using EverscaleNet.Serialization;
using EverscaleNet.Utils;

namespace EverscaleNet.Testing;

/// <inheritdoc cref="EverscaleNet.Testing.IEverGiver" />
public class EverGiverV2 : IEverGiver {
	private const string GiverV2AbiJson =
		"{\"ABI version\": 2,\"header\": [\"time\", \"expire\"],\"functions\": [{\"name\": \"upgrade\",\"inputs\": [{\"name\":\"newcode\",\"type\":\"cell\"}],\"outputs\": []},{\"name\": \"sendTransaction\",\"inputs\": [{\"name\":\"dest\",\"type\":\"address\"},{\"name\":\"value\",\"type\":\"uint128\"},{\"name\":\"bounce\",\"type\":\"bool\"}],\"outputs\": []},{\"name\": \"getMessages\",\"inputs\": [],\"outputs\": [{\"components\":[{\"name\":\"hash\",\"type\":\"uint256\"},{\"name\":\"expireAt\",\"type\":\"uint64\"}],\"name\":\"messages\",\"type\":\"tuple[]\"}]},{\"name\": \"constructor\",\"inputs\": [],\"outputs\": []}],\"events\": []}";
	private static readonly Abi.Contract Abi = new() {
		Value = JsonSerializer.Deserialize<AbiContract>(GiverV2AbiJson, JsonOptionsProvider.JsonSerializerOptions)
	};

	private readonly IEverClient _everClient;
	private readonly KeyPair _keyPair;

	/// <summary>
	/// </summary>
	/// <param name="everClient"></param>
	/// <param name="keyPair"></param>
	/// <param name="address">Put if giver uses not generated address</param>
	public EverGiverV2(IEverClient everClient, KeyPair keyPair, string address) {
		Address = address;
		_everClient = everClient;
		_keyPair = keyPair;
	}

	/// <inheritdoc />
	public async Task SendCoins(string dest, decimal coins, bool bounce, CancellationToken cancellationToken) {
		var value = $"{coins.CoinsToNano():0000}";
		var encodedMessage = new ParamsOfEncodeMessage {
			Address = Address,
			Abi = Abi,
			CallSet = new CallSet {
				FunctionName = "sendTransaction",
				Input = new { dest, value, bounce }.ToJsonElement()
			},
			Signer = new Signer.Keys { KeysAccessor = _keyPair }
		};
		await _everClient.ProcessEncodeMessageAndWaitTransaction(encodedMessage, cancellationToken);
	}

	/// <inheritdoc />
	public string Address { get; }
	public Task<decimal> GetBalance(CancellationToken cancellationToken = default) {
		throw new NotImplementedException();
	}

	public Task InitByPublicKey(string publicKey, CancellationToken cancellationToken = default) {
		throw new NotImplementedException();
	}
}
