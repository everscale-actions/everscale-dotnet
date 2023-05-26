namespace EverscaleNet.TestSuite.Services;

/// <summary>
/// </summary>
public class InitMultisigAccountService : IHostedService {
	private readonly IEverGiver _giver;
	private readonly KeyPair _keyPair;
	private readonly IMultisigAccount _multisig;

	/// <summary>
	/// </summary>
	/// <param name="multisig"></param>
	/// <param name="keyPair"></param>
	/// <param name="giver"></param>
	public InitMultisigAccountService(IMultisigAccount multisig, KeyPair keyPair, IEverGiver giver) {
		_multisig = multisig;
		_keyPair = keyPair;
		_giver = giver;
	}

	/// <summary>
	/// </summary>
	/// <param name="cancellationToken"></param>
	public async Task StartAsync(CancellationToken cancellationToken) {
		await _multisig.Init(_keyPair.Public, cancellationToken: cancellationToken);
		await _giver.SendTransaction(_multisig.Address, 20m, false, cancellationToken);
		await _multisig.Deploy(new[] { _keyPair.Public }, 1, TimeSpan.FromHours(1), cancellationToken);
	}

	/// <summary>
	/// </summary>
	/// <param name="cancellationToken"></param>
	public async Task StopAsync(CancellationToken cancellationToken) {
		await _multisig.SubmitTransaction(_giver.Address, 0, true, true, string.Empty, cancellationToken: cancellationToken);
	}
}
