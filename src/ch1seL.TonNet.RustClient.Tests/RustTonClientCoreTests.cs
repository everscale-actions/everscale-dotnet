using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ch1seL.TonNet.Serialization;
using FluentAssertions;
using Xunit;

namespace ch1seL.TonNet.RustClient.Tests
{
    public class RustTonClientCoreTests
    {
        [Fact]
        public void CreatingClientInitNotThrowException()
        {
            var act = new Action(() =>
            {
                using RustTonClientCore client = TestsHelpers.CreateTonClient();
            });

            act.Should().NotThrow();
        }

        [Fact]
        public async Task TestClosure()
        {
            RustTonClientCore client = TestsHelpers.CreateTonClient();
            Func<Task> act = () =>
            {
                {
                    return Task.WhenAll(Enumerable.Repeat(0, 10000)
                        // ReSharper disable once AccessToDisposedClosure
                        .Select(_ => client.Request("client.version", null)));
                }
            };

            await act.Should().NotThrowAsync();
            client.Dispose();
        }

        [Fact]
        public async Task FactorizeReturnsCorrectOutput()
        {
            using RustTonClientCore client = TestsHelpers.CreateTonClient();

            const string method = "crypto.factorize";
            var parameters = new
            {
                composite = "17ED48941A08F981"
            };
            var response = await client.Request(method, JsonSerializer.Serialize(parameters, JsonOptionsProvider.JsonSerializerOptions));

            response.Should().Be("{\"factors\":[\"494C553B\",\"53911073\"]}");
        }

        [Fact]
        public async Task VersionRequestResponseWithNotEmptyResultAndNullError()
        {
            using RustTonClientCore client = TestsHelpers.CreateTonClient();

            var response = await client.Request("client.version", null);

            response.Should().Be("{\"version\":\"1.1.1\"}");
        }
    }
}