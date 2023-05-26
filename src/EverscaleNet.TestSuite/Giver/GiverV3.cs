namespace EverscaleNet.TestSuite.Giver;

/// <inheritdoc cref="IEverGiver" />
public class GiverV3 : AccountBase, IEverGiver {
	private const string SeGiverAddress = "0:96137b99dcd65afce5a54a48dac83c0fd276432abbe3ba7f1bfb0fb795e69025";

	/// <summary>
	///     Create Giver by address. Init is not necessary.
	/// </summary>
	/// <param name="everClient"></param>
	/// <param name="optionsAccessor"></param>
	public GiverV3(IEverClient everClient, IOptions<GiverOptions> optionsAccessor) : base(
		everClient,
		new FilePackageManager(new OptionsWrapper<FilePackageManagerOptions>(new FilePackageManagerOptions {
			KeyPairFileTemplate = "seGiver.keys.json"
		})),
		address: optionsAccessor.Value.Address ?? SeGiverAddress
	) { }

	/// <inheritdoc />
	public async Task<ResultOfProcessMessage> SendTransaction(string dest, decimal coins, bool bounce = false, CancellationToken cancellationToken = default) {
		return await Run("sendTransaction", new {
			dest,
			value = coins.CoinsToNano(),
			bounce
		}, cancellationToken);
	}
}
