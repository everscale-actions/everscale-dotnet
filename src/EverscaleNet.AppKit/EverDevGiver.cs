using EverscaleNet.Abstract;
using EverscaleNet.Client.Models;

namespace EverscaleNet;

/// <inheritdoc />
public class EverDevGiver : EverGiverBase {
	/// <inheritdoc />
	public EverDevGiver(IEverClient everClient, IEverPackageManager packageManager, KeyPair keyPair) : base(everClient, packageManager, keyPair) { }
}
