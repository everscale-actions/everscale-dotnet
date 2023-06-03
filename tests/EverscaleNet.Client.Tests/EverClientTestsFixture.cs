namespace EverscaleNet.Client.Tests;

// ReSharper disable once ClassNeverInstantiated.Global
public class EverClientTestsFixture : IDisposable, IAsyncDisposable {
	private IList<IEverClientAdapter> _adapters = new List<IEverClientAdapter>();
	private LoggerFactory _loggerFactory;

	public async ValueTask DisposeAsync() {
		await DisposeAsyncCore().ConfigureAwait(false);
		Dispose(false);
#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
		GC.SuppressFinalize(this);
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
	}

	public void Dispose() {
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected internal IEverClient CreateClient(ITestOutputHelper output, bool useNodeSe = false) {
		_loggerFactory ??= new LoggerFactory(new[] {
			new SerilogLoggerProvider(new LoggerConfiguration()
			                          .MinimumLevel.Verbose()
			                          .WriteTo.TestOutput(output)
			                          .CreateLogger())
		});

		var options = new EverClientOptions {
			Network = new NetworkConfig {
				Endpoints = useNodeSe ? TestsEnv.EverscaleNetworkEndpoints : null,
				QueriesProtocol = NetworkQueriesProtocol.WS
			}
		};

		var adapter = new EverClientRustAdapter(new OptionsWrapper<EverClientOptions>(options), _loggerFactory.CreateLogger<EverClientRustAdapter>());
		_adapters.Add(adapter);
		return new EverClient(adapter);
	}

	protected virtual void Dispose(bool disposing) {
		if (disposing) {
			_loggerFactory?.Dispose();
			_loggerFactory = null;
		}
	}

	private async Task DisposeAsyncCore() {
		_loggerFactory?.Dispose();
		foreach (IEverClientAdapter adapter in _adapters) {
			await adapter.DisposeAsync();
		}

		_loggerFactory = null;
		_adapters = null;
	}
}
