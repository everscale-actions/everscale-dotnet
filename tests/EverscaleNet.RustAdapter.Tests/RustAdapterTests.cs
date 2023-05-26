using EverscaleNet.Abstract;
using EverscaleNet.Adapter.Rust;
using EverscaleNet.Models;
using EverscaleNet.Serialization;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Serilog;
using Xunit;
using Xunit.Abstractions;

namespace EverscaleNet.RustAdapter.Tests;

public class RustAdapterTests {
	private readonly ILogger<EverClientRustAdapter> _logger;

	public RustAdapterTests(ITestOutputHelper output) {
		ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddSerilog(new LoggerConfiguration()
		                                                                                  .MinimumLevel.Verbose()
		                                                                                  .WriteTo.TestOutput(output)
		                                                                                  .CreateLogger()));
		_logger = loggerFactory.CreateLogger<EverClientRustAdapter>();
	}

	[Fact(Timeout = 10000)]
	public async Task AdapterDisposingNotThrowExceptionsTest() {
		Func<Task> act = async () => {
			await using IEverClientAdapter rustAdapter = TestsHelpers.CreateRustAdapter(_logger);
			await Task.WhenAll(Enumerable.Repeat(0, 100)
			                             // ReSharper disable once AccessToDisposedClosure
			                             .Select(_ => rustAdapter.Request("client.get_api_reference")));
		};

		await act.Should().NotThrowAsync();
	}

	[Fact]
	public async Task FactorizeReturnsCorrectOutputTest() {
		await using IEverClientAdapter rustAdapter = TestsHelpers.CreateRustAdapter(_logger);

		const string method = "crypto.factorize";
		var parameters = new {
			composite = "17ED48941A08F981"
		};
		JsonElement response =
			await rustAdapter.Request<JsonElement, JsonElement>(method, parameters.ToJsonElement());

		response.ToString().Should().Be("{\"factors\":[\"494C553B\",\"53911073\"]}");
	}

	[Fact]
	public async Task InitAdapterNotThrowExceptionTest() {
		var act = new Func<Task>(async () => {
			await using IEverClientAdapter rustAdapter = TestsHelpers.CreateRustAdapter(_logger);
		});

		await act.Should().NotThrowAsync();
	}

	[Fact]
	public async Task SdkInitializedWithoutNetworkExceptionTest() {
		Func<Task> act = async () => {
			await using IEverClientAdapter rustAdapter = TestsHelpers.CreateRustAdapter(_logger);
			await rustAdapter.Request<JsonElement>("net.get_endpoints");
		};

		await act.Should().ThrowAsync<EverClientException>()
		         .WithMessage("SDK is initialized without network config");
	}

	[Fact]
	public async Task VersionRequestResponseWithVersionRegexTest() {
		await using IEverClientAdapter rustAdapter = TestsHelpers.CreateRustAdapter(_logger);

		var response = await rustAdapter.Request<JsonElement>("client.version");

		response.ToString().Should().MatchRegex(@"{""version"":""\d+\.\d+\.\d+""}");
	}
}
