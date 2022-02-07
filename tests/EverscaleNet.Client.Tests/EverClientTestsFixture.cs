using EverscaleNet.TestsShared;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Extensions.Logging;
using Xunit.Abstractions;

namespace EverscaleNet.Client.Tests;

// ReSharper disable once ClassNeverInstantiated.Global
public class EverClientTestsFixture : IDisposable {
	private LoggerFactory _loggerFactory;

	protected internal IEverClient CreateClient(ITestOutputHelper output, bool useNodeSe = false) {
		_loggerFactory ??= new LoggerFactory(new[] {
			new SerilogLoggerProvider(new LoggerConfiguration()
			                          .MinimumLevel.Verbose()
			                          .WriteTo.TestOutput(output)
			                          .CreateLogger())
		});

		var options = new EverClientOptions {
			Network = new NetworkConfig {
				Endpoints = useNodeSe ? TestsEnv.EverscaleNetworkEndpoints : null
			}
		};

		var adapter = new EverClientRustAdapter(new OptionsWrapper<EverClientOptions>(options), _loggerFactory.CreateLogger<EverClientRustAdapter>());

		return new EverClient(adapter);
	}

	public void Dispose() {
		_loggerFactory?.Dispose();
	}
}
