using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ch1seL.TonNet.RustAdapter;
using ch1seL.TonNet.Serialization;
using FluentAssertions;
using Xunit;

namespace ch1seL.TonNet.RustClient.Tests
{
    public class RustAdapterTests
    {
        [Fact]
        public void CreatingClientInitNotThrowException()
        {
            var act = new Action(() =>
            {
                using ITonClientRustAdapter client = TestsHelpers.CreateRustAdapter();
            });

            act.Should().NotThrow();
        }

        [Fact(Timeout = 10000)]
        public async Task TonClientDisposing()
        {
            Func<Task> act = async () =>
            {
                ITonClientRustAdapter client = TestsHelpers.CreateRustAdapter();
                await Task.WhenAll(Enumerable.Repeat(0, 1000)
                    // ReSharper disable once AccessToDisposedClosure
                    .Select(_ => client.RustRequest("client.get_api_reference", null)));

                client.Dispose();
            };

            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task FactorizeReturnsCorrectOutput()
        {
            using ITonClientRustAdapter client = TestsHelpers.CreateRustAdapter();

            const string method = "crypto.factorize";
            var parameters = new
            {
                composite = "17ED48941A08F981"
            };
            var response = await client.RustRequest(method, JsonSerializer.Serialize(parameters, JsonOptionsProvider.JsonSerializerOptions));

            response.Should().Be("{\"factors\":[\"494C553B\",\"53911073\"]}");
        }

        [Fact]
        public async Task VersionRequestResponseWithNotEmptyResultAndNullError()
        {
            using ITonClientRustAdapter client = TestsHelpers.CreateRustAdapter();

            var response = await client.RustRequest("client.version", null);

            response.Should().MatchRegex(@"{""version"":""\d+\.\d+\.\d+""}");
        }
    }
}