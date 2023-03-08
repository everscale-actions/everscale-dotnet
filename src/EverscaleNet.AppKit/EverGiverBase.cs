using System.Threading;
using System.Threading.Tasks;
using EverscaleNet.Abstract;
using EverscaleNet.Client.Models;
using EverscaleNet.Serialization;
using EverscaleNet.Utils;

namespace EverscaleNet;

/// <inheritdoc cref="IEverGiver" />
public abstract class EverGiverBase : AccountBase, IEverGiver {
	/// <summary>
	///     Create Giver by address. Init is not necessary.
	/// </summary>
	/// <param name="everClient"></param>
	/// <param name="packageManager"></param>
	/// <param name="address"></param>
	/// <param name="keyPair"></param>
	protected EverGiverBase(IEverClient everClient, IEverPackageManager packageManager, string address, KeyPair keyPair) : base(
		everClient, packageManager, new Signer.Keys { KeysAccessor = keyPair }, address) { }

	/// <summary>
	///     Create giver by key pair. Do not forget init after creation.
	/// </summary>
	/// <param name="everClient"></param>
	/// <param name="packageManager"></param>
	/// <param name="address"></param>
	protected EverGiverBase(IEverClient everClient, IEverPackageManager packageManager, string address) : base(everClient, packageManager, address) { }

	/// <summary>
	///     Create giver by key pair. Do not forget init after creation.
	/// </summary>
	/// <param name="everClient"></param>
	/// <param name="packageManager"></param>
	protected EverGiverBase(IEverClient everClient, IEverPackageManager packageManager) : base(everClient, packageManager) { }

	/// <inheritdoc />
	protected override string Name => "GiverV2";

	/// <inheritdoc />
	public async Task SendTransaction(string dest, decimal coins, bool bounce = false, CancellationToken cancellationToken = default) {
		var value = $"{coins.CoinsToNano():0}";
		await Run(new CallSet {
			FunctionName = "sendTransaction",
			Input = new { dest, value, bounce }.ToJsonElement()
		}, cancellationToken);
	}
}
