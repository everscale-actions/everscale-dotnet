using EverscaleNet.Abstract;

namespace EverscaleNet;

/// <inheritdoc />
public class EverNodeSeGiver : EverGiverBase {
	private const string GiverAddress = "0:ece57bcc6c530283becbbd8a3b24d3c5987cdddc3c8b7b33be6e4a6312490415";

	/// <inheritdoc />
	public EverNodeSeGiver(IEverClient everClient, IEverPackageManager packageManager) : base(everClient, packageManager, GiverAddress) { }
}
