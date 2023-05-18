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

namespace TestingExample.Fixtures;

public class EverDevTestsFixture : IEverTestsFixture {
	private readonly IConfigurationRoot _configuration;
	private EverClientRustAdapter _adapter;
	private HttpClient _httpClient;
	private LoggerFactory _loggerFactory;

	public EverDevTestsFixture() {
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
		Client ??= CreateClient();
		PackageManager ??= CreatePackageManager();
		Giver ??= await CreateGiver();
	}

	public async ValueTask DisposeAsync() {
		await DisposeAsyncCore().ConfigureAwait(false);
		GC.SuppressFinalize(this);
	}

	private void InitLoggerFactory(ITestOutputHelper output) {
		_loggerFactory ??= new LoggerFactory(new[] {
			new SerilogLoggerProvider(new LoggerConfiguration()
			                          .MinimumLevel.Verbose()
			                          .WriteTo.TestOutput(output)
			                          .CreateLogger())
		});
	}

	private static FilePackageManager CreatePackageManager() {
		return new FilePackageManager(new OptionsWrapper<PackageManagerOptions>(new PackageManagerOptions()));
	}

	private async Task<EverDevGiver> CreateGiver() {
		var giverPackageManager = new WebPackageManager(_httpClient, new OptionsWrapper<PackageManagerOptions>(new PackageManagerOptions {
			PackagesPath = "https://raw.githubusercontent.com/tonlabs/evernode-se/5652dc8710d8c1f249a663f537ef78116bf97f6d/contracts/giver_v2/"
		}));

		var keyPair = new KeyPair {
			Public = _configuration["EverDev:Giver:Public"] ?? throw new NullReferenceException("EverDev:Giver:Public was not set"),
			Secret = _configuration["EverDev:Giver:Secret"] ?? throw new NullReferenceException("EverDev:Giver:Secret was not set")
		};

		var giver = new EverDevGiver(Client, giverPackageManager, keyPair);
		await giver.InitByPublicKey(keyPair.Public);
		return giver;
	}

	private IEverClient CreateClient() {
		string[] endpoints = EverOS.Endpoints.Development;

		string everCloudProjectId = _configuration["EverDev:Evercloud:ProjectId"];
		if (everCloudProjectId != null) {
			endpoints = endpoints.Select(e => $"{e.Trim('/')}/{everCloudProjectId}").ToArray();
		}

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
		_loggerFactory?.Dispose();
		_httpClient?.Dispose();
		_adapter = null;
		_loggerFactory = null;
		_httpClient = null;
	}
}
