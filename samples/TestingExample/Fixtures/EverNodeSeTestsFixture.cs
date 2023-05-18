using System.Reflection;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using EverscaleNet.Adapter.Rust;
using EverscaleNet.Client;
using EverscaleNet.Client.PackageManager;
using EverscaleNet.WebClient.PackageManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Extensions.Logging;

namespace TestingExample.Fixtures;

public class EverNodeSeTestsFixture : IEverTestsFixture {
	private readonly IConfigurationRoot _configuration;
	private EverClientRustAdapter _adapter;
	private IContainer _everNodeSeContainer;
	private HttpClient _httpClient;
	private LoggerFactory _loggerFactory;

	public EverNodeSeTestsFixture() {
		_httpClient = new HttpClient();
		_configuration = new ConfigurationBuilder()
		                 .AddUserSecrets(Assembly.GetExecutingAssembly())
		                 .AddEnvironmentVariables()
		                 .Build();
	}

	public IEverClient Client { get; private set; }
	public IEverPackageManager PackageManager { get; private set; }
	public IEverGiver Giver { get; private set; }

	public async Task Init(ITestOutputHelper output) {
		InitLoggerFactory(output);
		Client ??= CreateEverClient();
		PackageManager ??= CreatePackageManager();
		Giver ??= await CreateGiver();
		await RunNodeSeContainer();
	}

	public async ValueTask DisposeAsync() {
		await DisposeAsyncCore().ConfigureAwait(false);
		GC.SuppressFinalize(this);
	}

	private static FilePackageManager CreatePackageManager() {
		return new FilePackageManager(new OptionsWrapper<PackageManagerOptions>(new PackageManagerOptions()));
	}

	private async Task<EverNodeSeGiver> CreateGiver() {
		var giverPackageManager = new WebPackageManager(_httpClient, new OptionsWrapper<PackageManagerOptions>(new PackageManagerOptions {
			PackagesPath = "https://raw.githubusercontent.com/tonlabs/evernode-se/5652dc8710d8c1f249a663f537ef78116bf97f6d/contracts/giver_v2/"
		}));
		var giver = new EverNodeSeGiver(Client, giverPackageManager);
		await giver.InitByPackage();
		return giver;
	}

	private async Task RunNodeSeContainer() {
		if (_configuration["EVERSCALE_NETWORK_ENDPOINTS"] is not null) {
			return;
		}
		if (!bool.TryParse(_configuration["EverNodeSe:RunNodeSeContainer"], out bool runNodeSeContainer)) {
			runNodeSeContainer = true;
		}
		if (runNodeSeContainer) {
			TestcontainersSettings.Logger ??= _loggerFactory.CreateLogger("NodeSE");
			_everNodeSeContainer ??= await BuildAndStartNodeSE();
		}
	}

	private void InitLoggerFactory(ITestOutputHelper output) {
		_loggerFactory ??= new LoggerFactory(new[] {
			new SerilogLoggerProvider(new LoggerConfiguration()
			                          .MinimumLevel.Verbose()
			                          .WriteTo.TestOutput(output)
			                          .CreateLogger())
		});
	}

	private static async Task<IContainer> BuildAndStartNodeSE() {
		IContainer everNodeSE = new ContainerBuilder()
		                        .WithImage("tonlabs/local-node:latest")
		                        .WithEnvironment("USER_AGREEMENT", "yes")
		                        .WithPortBinding(80, true)
		                        .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(80))
		                        .WithWaitStrategy(Wait.ForUnixContainer().AddCustomWaitStrategy(new WaitNodeSeFirstBlockStrategy()))
		                        .Build();
		await everNodeSE.StartAsync();
		return everNodeSE;
	}

	private EverClient CreateEverClient() {
		string[] endpoints = _everNodeSeContainer is not null
			                     ? EverOS.Endpoints.NodeSE.Select(e => $"{e}:{_everNodeSeContainer.GetMappedPublicPort(80)}").ToArray()
			                     : _configuration["EVERSCALE_NETWORK_ENDPOINTS"]?.Split(',', ';', '|')
			                       ?? _configuration["EverNodeSe:Endpoint"]?.Split(',', ';', '|')
			                       ?? EverOS.Endpoints.NodeSE;

		var options = new OptionsWrapper<EverClientOptions>(new EverClientOptions {
			Network = {
				Endpoints = endpoints,
				QueriesProtocol = NetworkQueriesProtocol.WS
			}
		});

		ILogger<EverClientRustAdapter> logger = _loggerFactory.CreateLogger<EverClientRustAdapter>();

		_adapter = new EverClientRustAdapter(options, logger);
		return new EverClient(_adapter);
	}

	private async ValueTask DisposeAsyncCore() {
		if (_adapter is not null) {
			await _adapter.DisposeAsync().ConfigureAwait(false);
		}
		if (_everNodeSeContainer is not null) {
			await _everNodeSeContainer.DisposeAsync().ConfigureAwait(false);
		}
		_loggerFactory?.Dispose();
		_httpClient?.Dispose();
		_adapter = null;
		_everNodeSeContainer = null;
		_loggerFactory = null;
		_httpClient = null;
	}
}
