using EverscaleNet.Abstract;
using Microsoft.Extensions.Hosting;

namespace EverscaleNet.TestSuite;

/// <summary>
/// </summary>
public class InitKeyPairService : IHostedService {
	private readonly IEverClient _everClient;
	private readonly KeyPair _keyPair;

	/// <summary>
	/// </summary>
	/// <param name="everClient"></param>
	/// <param name="keyPair"></param>
	public InitKeyPairService(IEverClient everClient, KeyPair keyPair) {
		_everClient = everClient;
		_keyPair = keyPair;
	}

	/// <inheritdoc />
	public async Task StartAsync(CancellationToken cancellationToken) {
		KeyPair keyPair = await _everClient.Crypto.GenerateRandomSignKeys(cancellationToken);
		_keyPair.Public = keyPair.Public;
		_keyPair.Secret = keyPair.Secret;
	}

	/// <inheritdoc />
	public Task StopAsync(CancellationToken cancellationToken) {
		return Task.CompletedTask;
	}
}
