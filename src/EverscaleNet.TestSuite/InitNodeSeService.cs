using Microsoft.Extensions.Hosting;

namespace EverscaleNet.TestSuite;

/// <summary>
///     Class can be used for start Node SE docker container through DI with `services.AddHostedService&lt;InitNodeSeService&gt;();`
/// </summary>
public class InitNodeSeService : IHostedService {
	private readonly NodeSeDockerContainer _nodeSeDockerContainer;

	/// <summary>
	/// </summary>
	/// <param name="nodeSeDockerContainer"></param>
	public InitNodeSeService(NodeSeDockerContainer nodeSeDockerContainer) {
		_nodeSeDockerContainer = nodeSeDockerContainer;
	}

	/// <summary>
	/// </summary>
	/// <param name="cancellationToken"></param>
	public async Task StartAsync(CancellationToken cancellationToken) {
		await _nodeSeDockerContainer.StartAsync(cancellationToken);
	}

	/// <summary>
	/// </summary>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	public Task StopAsync(CancellationToken cancellationToken) {
		return Task.CompletedTask;
	}
}
