using System.Reflection;
using EverscaleNet.Adapter.Rust;
using EverscaleNet.Client;
using EverscaleNet.Client.PackageManager;
using EverscaleNet.WebClient.PackageManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Extensions.Logging;

namespace TestingExample;

public class EverDevTestsFixture : IEverTestsFixture {
	private readonly WebPackageManager _giverPackageManager;
	private EverClientRustAdapter _adapter;
	private IConfigurationRoot _configuration;
	private HttpClient _httpClient;
	private LoggerFactory _loggerFactory;

	public EverDevTestsFixture() {
		_httpClient = new HttpClient();
		_giverPackageManager = new WebPackageManager(_httpClient, new OptionsWrapper<WebPackageManagerOptions>(new WebPackageManagerOptions {
			PackagesPath = "https://raw.githubusercontent.com/tonlabs/evernode-se/5652dc8710d8c1f249a663f537ef78116bf97f6d/contracts/giver_v2/"
		}));
	}

	public IEverClient Client { get; private set; }
	public IEverPackageManager PackageManager { get; private set; }
	public IEverGiver Giver { get; private set; }

	public async Task Init(ITestOutputHelper output) {
		await InitAsync(output);
	}

	public async ValueTask DisposeAsync() {
		await DisposeAsyncCore().ConfigureAwait(false);
		GC.SuppressFinalize(this);
	}

	private async Task InitAsync(ITestOutputHelper output) {
		_configuration ??= new ConfigurationBuilder()
		                   .AddUserSecrets(Assembly.GetExecutingAssembly())
		                   .Build();
		_loggerFactory ??= new LoggerFactory(new[] {
			new SerilogLoggerProvider(new LoggerConfiguration()
			                          .MinimumLevel.Verbose()
			                          .WriteTo.TestOutput(output)
			                          .CreateLogger())
		});
		Client ??= CreateClient(_loggerFactory.CreateLogger<EverClientRustAdapter>());
		PackageManager ??= new FilePackageManager(new OptionsWrapper<FilePackageManagerOptions>(new FilePackageManagerOptions()));
		Giver ??= await CreateGiver();
	}

	private async Task<EverDevGiver> CreateGiver() {
		var giverKeyPair = new KeyPair {
			Public = _configuration["Giver:Public"],
			Secret = _configuration["Giver:Secret"]
		};

		var giver = new EverDevGiver(Client, _giverPackageManager, giverKeyPair);
		await giver.Init();
		return giver;
	}

	private IEverClient CreateClient(ILogger<EverClientRustAdapter> logger) {
		string[] endpoints = EverOS.Endpoints.Development;

		string everCloudProjectId = _configuration["Evercloud:ProjectId"];
		if (everCloudProjectId != null) {
			endpoints = endpoints.Select(e => $"{e.Trim('/')}/{everCloudProjectId}").ToArray();
		}

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
		if (_adapter is not null) {
			await _adapter.DisposeAsync().ConfigureAwait(false);
		}
		_loggerFactory?.Dispose();
		_httpClient?.Dispose();
		_adapter = null;
		_loggerFactory = null;
		_httpClient = null;
	}
}
