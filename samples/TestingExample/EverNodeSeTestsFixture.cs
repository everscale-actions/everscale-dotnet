using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using EverscaleNet.Adapter.Rust;
using EverscaleNet.Client;
using EverscaleNet.Client.PackageManager;
using EverscaleNet.WebClient.PackageManager;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Extensions.Logging;

namespace TestingExample;

public class EverNodeSeTestsFixture : IEverTestsFixture {
	// if you want to start new node se container per fixture set true
	// todo: init by configuration file or env variables
	private const bool RunNodeSeContainer = false;

	private const string NetworkEndpointsEnvVariable = "EVERSCALE_NETWORK_ENDPOINTS";
	private readonly WebPackageManager _giverPackageManager;
	private EverClientRustAdapter _adapter;
	private TestcontainersContainer _everNodeSeContainer;
	private HttpClient _httpClient;
	private LoggerFactory _loggerFactory;

	public EverNodeSeTestsFixture() {
		_httpClient = new HttpClient();
		_giverPackageManager = new WebPackageManager(_httpClient, new OptionsWrapper<WebPackageManagerOptions>(new WebPackageManagerOptions {
			PackagesPath = "https://raw.githubusercontent.com/tonlabs/evernode-se/5652dc8710d8c1f249a663f537ef78116bf97f6d/contracts/giver_v2/"
		}));
	}

	public IEverClient Client { get; private set; }
	public IEverPackageManager PackageManager { get; private set; }
	public IEverGiver Giver { get; private set; }

	public async Task Init(ITestOutputHelper output) {
		await InitAsync(RunNodeSeContainer, output);
	}

	public async ValueTask DisposeAsync() {
		await DisposeAsyncCore().ConfigureAwait(false);
		Dispose(false);
		GC.SuppressFinalize(this);
	}

	private async Task InitAsync(bool runNodeSeContainer, ITestOutputHelper output) {
		_loggerFactory ??= new LoggerFactory(new[] {
			new SerilogLoggerProvider(new LoggerConfiguration()
			                          .MinimumLevel.Verbose()
			                          .WriteTo.TestOutput(output)
			                          .CreateLogger())
		});
		if (runNodeSeContainer) {
			TestcontainersSettings.Logger ??= _loggerFactory.CreateLogger("Testcontainers");
			_everNodeSeContainer ??= await BuildAndStartNodeSE();
		} else {
			//todo: ensure that node se is ready
		}
		Client ??= CreateClient(_loggerFactory.CreateLogger<EverClientRustAdapter>());
		PackageManager ??= new FilePackageManager(new OptionsWrapper<FilePackageManagerOptions>(new FilePackageManagerOptions()));
		Giver ??= new EverNodeSeGiver(Client, _giverPackageManager);
	}

	private static async Task<TestcontainersContainer> BuildAndStartNodeSE() {
		TestcontainersContainer everNodeSE = new TestcontainersBuilder<TestcontainersContainer>()
		                                     .WithImage("tonlabs/local-node:latest")
		                                     .WithEnvironment("USER_AGREEMENT", "yes")
		                                     .WithPortBinding(80, true)
		                                     .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(80))
		                                     .WithWaitStrategy(Wait.ForUnixContainer().AddCustomWaitStrategy(new WaitNodeSeFirstBlockStrategy()))
		                                     .Build();
		await everNodeSE.StartAsync();
		return everNodeSE;
	}

	private IEverClient CreateClient(ILogger<EverClientRustAdapter> logger) {
		string[] endpoints = _everNodeSeContainer == null
			                     ? Environment.GetEnvironmentVariable(NetworkEndpointsEnvVariable)?.Split(";") ?? EverOS.Endpoints.NodeSE
			                     : EverOS.Endpoints.NodeSE.Select(e => $"{e}:{_everNodeSeContainer.GetMappedPublicPort(80)}").ToArray();

		var options = new EverClientOptions {
			Network = new NetworkConfig {
				Endpoints = endpoints,
				QueriesProtocol = NetworkQueriesProtocol.WS
			}
		};

		_adapter = new EverClientRustAdapter(new OptionsWrapper<EverClientOptions>(options), logger);
		return new EverClient(_adapter);
	}

	private async ValueTask DisposeAsyncCore() {
		await _adapter.DisposeAsync();
		_adapter = null;
		await _everNodeSeContainer.DisposeAsync();
		_everNodeSeContainer = null;
	}

	protected virtual void Dispose(bool disposing) {
		if (disposing) {
			_loggerFactory?.Dispose();
			_loggerFactory = null;
			_httpClient?.Dispose();
			_httpClient = null;
		}
	}
}