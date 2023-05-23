using EverscaleNet.Abstract;

namespace EverscaleNet.TestSuite;

/// <summary>
/// </summary>
public class SafeMultisigAccount : MultisigAccountBase {
	/// <summary>
	/// </summary>
	/// <param name="client"></param>
	/// <param name="packageManager"></param>
	public SafeMultisigAccount(IEverClient client, IEverPackageManager packageManager) : base(client, packageManager) { }

	/// <summary>
	/// </summary>
	protected override string Name => "SafeMultisig";
}
