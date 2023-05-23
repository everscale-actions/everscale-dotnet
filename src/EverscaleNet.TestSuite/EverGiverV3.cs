using EverscaleNet.Abstract;
using EverscaleNet.Client.Models;
using EverscaleNet.Client.PackageManager;
using EverscaleNet.Serialization;
using EverscaleNet.Utils;
using Microsoft.Extensions.Options;

namespace EverscaleNet.TestSuite;

/// <inheritdoc cref="IEverGiver" />
public class EverGiverV3 : AccountBase, IEverGiver {
	private const string SeGiverAddress = "0:96137b99dcd65afce5a54a48dac83c0fd276432abbe3ba7f1bfb0fb795e69025";

	/// <summary>
	///     Create Giver by address. Init is not necessary.
	/// </summary>
	/// <param name="everClient"></param>
	/// <param name="optionsAccessor"></param>
	public EverGiverV3(IEverClient everClient, IOptions<GiverOptions> optionsAccessor) : base(
		everClient,
		new FilePackageManager(new OptionsWrapper<FilePackageManagerOptions>(new FilePackageManagerOptions {
			KeyPairFileTemplate = "seGiver.keys.json"
		})),
		address: optionsAccessor.Value.Address ?? SeGiverAddress
	) { }

	/// <inheritdoc />
	protected override string Name => "GiverV3";

	/// <inheritdoc />
	public async Task SendTransaction(string dest, decimal coins, bool bounce = false, CancellationToken cancellationToken = default) {
		var value = $"{coins.CoinsToNano():0}";
		await Run(new CallSet {
			FunctionName = "sendTransaction",
			Input = new { dest, value, bounce }.ToJsonElement()
		}, cancellationToken);
	}
}
