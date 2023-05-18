using EverscaleNet.Abstract;
using EverscaleNet.Client.Models;

namespace EverscaleNet.TestSuite;

/// <summary>
/// </summary>
public class SafeMultisigAccount : MultisigAccountBase {
	/// <summary>
	/// </summary>
	/// <param name="client"></param>
	/// <param name="packageManager"></param>
	/// <param name="keyPair"></param>
	public SafeMultisigAccount(IEverClient client, IEverPackageManager packageManager, KeyPair keyPair) : base(client, packageManager, keyPair) { }

	/// <summary>
	/// </summary>
	protected override string Name => "SafeMultisig";
}
