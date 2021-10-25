using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Adapter.Rust;
using ch1seL.TonNet.Serialization;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Serilog;
using Xunit;
using Xunit.Abstractions;

namespace ch1seL.TonNet.RustAdapter.Tests
{
    public class RustAdapterTests
    {
        private readonly ILogger<TonClientRustAdapter> _logger;

        public RustAdapterTests(ITestOutputHelper output)
        {
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddSerilog(new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.TestOutput(output)
                .CreateLogger()));
            _logger = loggerFactory.CreateLogger<TonClientRustAdapter>();
        }

        [Fact]
        public async Task InitAdapterNotThrowExceptionTest()
        {
            var act = new Func<Task>(async () =>
            {
                await using ITonClientAdapter rustAdapter = TestsHelpers.CreateRustAdapter(_logger);
            });

            await act.Should().NotThrowAsync();
        }

        [Fact(Timeout = 10000)]
        public async Task AdapterDisposingNotThrowExceptionsTest()
        {
            Func<Task> act = async () =>
            {
                ITonClientAdapter rustAdapter = TestsHelpers.CreateRustAdapter(_logger);
                await Task.WhenAll(Enumerable.Repeat(0, 100)
                    .Select(_ => rustAdapter.Request<JsonElement>("client.get_api_reference")));

                await rustAdapter.DisposeAsync();
            };

            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task FactorizeReturnsCorrectOutputTest()
        {
            await using ITonClientAdapter rustAdapter = TestsHelpers.CreateRustAdapter(_logger);

            const string method = "crypto.factorize";
            var parameters = new
            {
                composite = "17ED48941A08F981"
            };
            JsonElement response =
                await rustAdapter.Request<JsonElement, JsonElement>(method, parameters.ToJsonElement());

            response.ToString().Should().Be("{\"factors\":[\"494C553B\",\"53911073\"]}");
        }

        [Fact]
        public async Task VersionRequestResponseWithVersionRegexTest()
        {
            await using ITonClientAdapter rustAdapter = TestsHelpers.CreateRustAdapter(_logger);

            var response = await rustAdapter.Request<JsonElement>("client.version");

            response.ToString().Should().MatchRegex(@"{""version"":""\d+\.\d+\.\d+""}");
        }
    }
}