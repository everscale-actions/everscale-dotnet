using EverscaleNet.Abstract;
using EverscaleNet.Client.Models;

namespace EverscaleNet;

/// <inheritdoc />
public class EverDevGiver : EverGiverBase {
	/// <inheritdoc />
	public EverDevGiver(IEverClient everClient, IEverPackageManager packageManager) : base(everClient, packageManager) { }
}
