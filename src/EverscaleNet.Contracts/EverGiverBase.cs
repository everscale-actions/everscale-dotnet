using System.Threading;
using System.Threading.Tasks;
using EverscaleNet.Abstract;
using EverscaleNet.Client.Models;
using EverscaleNet.Serialization;
using EverscaleNet.Utils;

namespace EverscaleNet;

/// <inheritdoc cref="IEverGiver" />
public abstract class EverGiverBase : ContractBase, IEverGiver {
	private readonly IEverClient _everClient;

	/// <summary>
	///     Create Giver by address. Init is not necessary.
	/// </summary>
	/// <param name="everClient"></param>
	/// <param name="packageManager"></param>
	/// <param name="address"></param>
	protected EverGiverBase(IEverClient everClient, IEverPackageManager packageManager, string address) : base(everClient, packageManager, address) {
		_everClient = everClient;
	}

	/// <summary>
	///     Create giver by key pair. Do not forget init after creation.
	/// </summary>
	/// <param name="everClient"></param>
	/// <param name="packageManager"></param>
	/// <param name="keyPair"></param>
	protected EverGiverBase(IEverClient everClient, IEverPackageManager packageManager, KeyPair keyPair) : base(everClient, packageManager, keyPair: keyPair) {
		_everClient = everClient;
	}

	/// <inheritdoc />
	protected override string Name => "GiverV2";

	/// <inheritdoc />
	public async Task SendTransaction(string dest, decimal coins, bool bounce = false, CancellationToken cancellationToken = default) {
		await Init(cancellationToken: cancellationToken);

		var value = $"{coins.CoinsToNano():0000}";
		var encodedMessage = new ParamsOfEncodeMessage {
			Address = Address,
			Abi = await GetAbi(cancellationToken),
			CallSet = new CallSet {
				FunctionName = "sendTransaction",
				Input = new { dest, value, bounce }.ToJsonElement()
			},
			Signer = new Signer.Keys { KeysAccessor = await GetKeyPair(cancellationToken) }
		};
		await _everClient.ProcessAndWaitInternalMessages(encodedMessage, cancellationToken);
	}
}
