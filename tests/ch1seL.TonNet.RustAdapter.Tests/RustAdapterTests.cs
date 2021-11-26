using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Adapter.Rust;
using ch1seL.TonNet.Client;
using ch1seL.TonNet.Serialization;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Serilog;
using Xunit;
using Xunit.Abstractions;

namespace ch1seL.TonNet.RustAdapter.Tests;

public class RustAdapterTests {
	public RustAdapterTests(ITestOutputHelper output) {
		ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddSerilog(new LoggerConfiguration()
		                                                                                  .MinimumLevel.Verbose()
		                                                                                  .WriteTo.TestOutput(output)
		                                                                                  .CreateLogger()));
		_logger = loggerFactory.CreateLogger<TonClientRustAdapter>();
	}

	private readonly ILogger<TonClientRustAdapter> _logger;

	[Fact(Timeout = 10000)]
	public async Task AdapterDisposingNotThrowExceptionsTest() {
		Func<Task> act = async () => {
			await using ITonClientAdapter rustAdapter = TestsHelpers.CreateRustAdapter(_logger);
			await Task.WhenAll(Enumerable.Repeat(0, 100)
			                             // ReSharper disable once AccessToDisposedClosure
			                             .Select(_ => rustAdapter.Request("client.get_api_reference")));
		};

		await act.Should().NotThrowAsync();
	}

	[Fact]
	public async Task FactorizeReturnsCorrectOutputTest() {
		await using ITonClientAdapter rustAdapter = TestsHelpers.CreateRustAdapter(_logger);

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
			await using ITonClientAdapter rustAdapter = TestsHelpers.CreateRustAdapter(_logger);
		});

		await act.Should().NotThrowAsync();
	}

	[Fact]
	public async Task SdkInitializedWithoutNetworkExceptionTest() {
		Func<Task> act = async () => {
			await using ITonClientAdapter rustAdapter = TestsHelpers.CreateRustAdapter(_logger);
			await rustAdapter.Request<JsonElement>("net.get_endpoints");
		};

		await act.Should().ThrowAsync<TonClientException>()
		         .WithMessage("SDK is initialized without network config");
	}

	[Fact]
	public async Task VersionRequestResponseWithVersionRegexTest() {
		await using ITonClientAdapter rustAdapter = TestsHelpers.CreateRustAdapter(_logger);

		var response = await rustAdapter.Request<JsonElement>("client.version");

		response.ToString().Should().MatchRegex(@"{""version"":""\d+\.\d+\.\d+""}");
	}
}