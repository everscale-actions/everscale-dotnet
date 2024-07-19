using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Images;

namespace EverscaleNet.TestSuite.Services;

/// <summary>
/// </summary>
public class NodeSeDockerContainer : IAsyncDisposable {
	private readonly IContainer _everNodeSeContainer;

	/// <summary>
	/// </summary>
	public NodeSeDockerContainer(ILoggerFactory loggerFactory) {
		_everNodeSeContainer =
			new ContainerBuilder()
				.WithImage("tonlabs/local-node:latest")
				.WithImagePullPolicy(PullPolicy.Always)
				.WithEnvironment("USER_AGREEMENT", "yes")
				.WithPortBinding(80, true)
				.WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(80))
				.WithWaitStrategy(Wait.ForUnixContainer().AddCustomWaitStrategy(new WaitNodeSeFirstBlockStrategy(loggerFactory)))
				.WithLogger(loggerFactory.CreateLogger<NodeSeDockerContainer>())
				.Build();
	}

	/// <summary>
	/// </summary>
	public string Endpoint => $"http://localhost:{_everNodeSeContainer.GetMappedPublicPort(80)}/graphql";

	/// <inheritdoc />
	public async ValueTask DisposeAsync() {
		await _everNodeSeContainer.StopAsync();
		await _everNodeSeContainer.DisposeAsync();
		GC.SuppressFinalize(this);
	}

	/// <summary>
	/// </summary>
	/// <param name="cancellationToken"></param>
	public async Task StartAsync(CancellationToken cancellationToken) {
		if (_everNodeSeContainer.State is not TestcontainersStates.Running) {
			await _everNodeSeContainer.StartAsync(cancellationToken);
		}
	}

	private class WaitNodeSeFirstBlockStrategy : IWaitUntil {
		private readonly ILogger<WaitNodeSeFirstBlockStrategy> _logger;
		private readonly ILoggerFactory _loggerFactory;

		/// <summary>
		/// </summary>
		/// <param name="loggerFactory"></param>
		public WaitNodeSeFirstBlockStrategy(ILoggerFactory loggerFactory) {
			_loggerFactory = loggerFactory;
			_logger = loggerFactory.CreateLogger<WaitNodeSeFirstBlockStrategy>();
		}

		/// <summary>
		/// </summary>
		/// <param name="container"></param>
		/// <returns></returns>
		public async Task<bool> UntilAsync(IContainer container) {
			try {
				await using var adapter = new EverClientRustAdapter(new OptionsWrapper<EverClientOptions>(new EverClientOptions {
					Network = new NetworkConfig {
						Endpoints = new[] { $"http://localhost:{container.GetMappedPublicPort(80)}" }
					}
				}), _loggerFactory.CreateLogger<EverClientRustAdapter>());
				var everClient = new EverClient(adapter);
				ResultOfQueryCollection? resultOfQueryCollection = await everClient.Net.QueryCollection(new ParamsOfQueryCollection {
					Collection = "blocks",
					Limit = 1,
					Result = "id"
				});
				return resultOfQueryCollection.Result.Length > 0;
			} catch (EverClientException) {
				_logger.LogInformation("Waiting for Node SE will be ready..");
				return false;
			}
		}
	}
}
