using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
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
        public void InitAdapterNotThrowExceptionTest()
        {
            var act = new Action(() =>
            {
                using ITonClientRustAdapter rustAdapter = TestsHelpers.CreateRustAdapter(_logger);
            });

            act.Should().NotThrow();
        }

        [Fact(Timeout = 10000)]
        public async Task AdapterDisposingNotThrowExceptionsTest()
        {
            Func<Task> act = async () =>
            {
                ITonClientRustAdapter rustAdapter = TestsHelpers.CreateRustAdapter(_logger);
                await Task.WhenAll(Enumerable.Repeat(0, 100)
                    // ReSharper disable once AccessToDisposedClosure
                    .Select(_ => rustAdapter.RustRequest("client.get_api_reference", null)));

                rustAdapter.Dispose();
            };

            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task FactorizeReturnsCorrectOutputTest()
        {
            using ITonClientRustAdapter rustAdapter = TestsHelpers.CreateRustAdapter(_logger);

            const string method = "crypto.factorize";
            var parameters = new
            {
                composite = "17ED48941A08F981"
            };
            var response = await rustAdapter.RustRequest(method, JsonSerializer.Serialize(parameters, JsonOptionsProvider.JsonSerializerOptions));

            response.Should().Be("{\"factors\":[\"494C553B\",\"53911073\"]}");
        }

        [Fact]
        public async Task VersionRequestResponseWithVersionRegexTest()
        {
            using ITonClientRustAdapter rustAdapter = TestsHelpers.CreateRustAdapter(_logger);

            var response = await rustAdapter.RustRequest("client.version", null);

            response.Should().MatchRegex(@"{""version"":""\d+\.\d+\.\d+""}");
        }
    }
}