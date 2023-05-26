namespace EverscaleNet.TestSuite.Services;

/// <summary>
///     Class can be used for start Node SE docker container through DI with `services.AddHostedService&lt;InitNodeSeService&gt;();`
/// </summary>
public class InitNodeSeService : IHostedService {
	private readonly ILogger<InitNodeSeService> _logger;
	private readonly NodeSeDockerContainer _nodeSeDockerContainer;

	/// <summary>
	/// </summary>
	/// <param name="nodeSeDockerContainer"></param>
	/// <param name="logger"></param>
	public InitNodeSeService(NodeSeDockerContainer nodeSeDockerContainer, ILogger<InitNodeSeService> logger) {
		_nodeSeDockerContainer = nodeSeDockerContainer;
		_logger = logger;
	}

	/// <summary>
	/// </summary>
	/// <param name="cancellationToken"></param>
	public async Task StartAsync(CancellationToken cancellationToken) {
		_logger.LogInformation("Starting Node Se container..");
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
