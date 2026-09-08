using Serilog.Sinks.XUnit3;
using Shouldly;

namespace EverscaleNet.RustAdapter.Tests;

public class RustAdapterTests {
	private readonly ILogger<EverClientRustAdapter> _logger;

	public RustAdapterTests(ITestOutputHelper output) {
		ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddSerilog(new LoggerConfiguration()
			.MinimumLevel.Verbose()
			.WriteTo.XUnit3TestOutput(new XUnit3TestOutputSink { TestOutputHelper = output })
			.CreateLogger()));
		_logger = loggerFactory.CreateLogger<EverClientRustAdapter>();
	}

	[Fact(Timeout = 10000)]
	public async Task AdapterDisposingNotThrowExceptionsTest() {
		var act = async () =>
		{
			await using IEverClientAdapter rustAdapter = TestsHelpers.CreateRustAdapter(_logger);
			await Task.WhenAll(Enumerable.Repeat(0, 100)
				// ReSharper disable once AccessToDisposedClosure
				.Select(_ => rustAdapter.Request("client.get_api_reference", cancellationToken: TestContext.Current.CancellationToken)));
		};

		await act.ShouldNotThrowAsync();
	}

	[Fact]
	public async Task FactorizeReturnsCorrectOutputTest() {
		await using IEverClientAdapter rustAdapter = TestsHelpers.CreateRustAdapter(_logger);

		const string method = "crypto.factorize";
		var parameters = new {
			composite = "17ED48941A08F981"
		};
		JsonElement response =
			await rustAdapter.Request<JsonElement, JsonElement>(method, parameters.ToJsonElement(), TestContext.Current.CancellationToken);

		response.ToString().ShouldBe("{\"factors\":[\"494C553B\",\"53911073\"]}");
	}

	[Fact]
	public async Task InitAdapterNotThrowExceptionTest() {
		var act = new Func<Task>(async () =>
		{
			await using IEverClientAdapter rustAdapter = TestsHelpers.CreateRustAdapter(_logger);
		});

		await act.ShouldNotThrowAsync();
	}

	[Fact]
	public async Task SdkInitializedWithoutNetworkExceptionTest() {
		var act = async () =>
		{
			await using IEverClientAdapter rustAdapter = TestsHelpers.CreateRustAdapter(_logger);
			await rustAdapter.Request<JsonElement>("net.get_endpoints");
		};

		var ex = await act.ShouldThrowAsync<EverClientException>();
		ex.Message.ShouldBe("SDK is initialized without network config");
	}

	[Fact]
	public async Task VersionRequestResponseWithVersionRegexTest() {
		await using IEverClientAdapter rustAdapter = TestsHelpers.CreateRustAdapter(_logger);

		var response = await rustAdapter.Request<JsonElement>("client.version", cancellationToken: TestContext.Current.CancellationToken);

		response.ToString().ShouldMatch("""{"version":"\d+\.\d+\.\d+"}""");
	}
}