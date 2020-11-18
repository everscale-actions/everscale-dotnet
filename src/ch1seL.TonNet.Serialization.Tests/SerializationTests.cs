using System.Text.Json;
using System.Threading.Tasks;
using ch1seL.TonNet.Client.Models;
using FluentAssertions;
using TestsShared;
using Xunit;

namespace ch1seL.TonNet.Serialization.Tests
{
    public class SerializationTests
    {
        [Fact]
        public async Task SerializeAbi()
        {
            TestPackage package = await TestPackage.GetPackage("Events", 2);

            var json = JsonSerializer.Serialize(package, JsonOptionsProvider.JsonSerializerOptions);

            json.Should().NotBeNull();
        }

        [Fact]
        public async Task SerializeEncodeMessageRequest()
        {
            TestPackage package = await TestPackage.GetPackage("Events", 2);

            var messageRequest = new ParamsOfEncodeMessage
            {
                Abi = package.Abi,
                DeploySet = new DeploySet
                {
                    Tvc = package.Tvc
                },
                CallSet = new CallSet
                {
                    FunctionName = "constructor",
                    Header = new FunctionHeader
                    {
                        Time = 123123,
                        Expire = 123123
                    }
                },
                Signer = new Signer.External
                {
                    PublicKey = "4c7c408ff1ddebb8d6405ee979c716a14fdd6cc08124107a61d3c25597099499"
                }
            };

            var json = JsonSerializer.Serialize(messageRequest, JsonOptionsProvider.JsonSerializerOptions);
            JsonElement jsonElement = JsonDocument.Parse(json).RootElement;

            jsonElement.Should().NotBeNull();

            jsonElement.TryGetProperty("abi", out JsonElement abi).Should().BeTrue();
            abi.TryGetProperty("type", out JsonElement apiType).Should().BeTrue();
            apiType.GetString().Should().Be("Contract");

            jsonElement.TryGetProperty("signer", out JsonElement signer).Should().BeTrue();
            signer.TryGetProperty("type", out JsonElement signerType).Should().BeTrue();
            signerType.GetString().Should().Be("External");

            json.Should().Contain("4c7c408ff1ddebb8d6405ee979c716a14fdd6cc08124107a61d3c25597099499");
        }

        [Fact]
        public void DoubleNoneField()
        {
            var signerNone = new Signer.None();
            var accountForExecutorNone = new AccountForExecutor.None();

            var signerNoneJson = JsonSerializer.Serialize(signerNone, JsonOptionsProvider.JsonSerializerOptions);
            var accountForExecutorNoneJson = JsonSerializer.Serialize(accountForExecutorNone, JsonOptionsProvider.JsonSerializerOptions);

            signerNoneJson.Should().Be(accountForExecutorNoneJson);
        }

        [Fact]
        public void BigIntegerConverting()
        {
            var objectWithBigInteger = new {bigIng = (ulong) 1231231231231123123};

            var json = JsonSerializer.Serialize(objectWithBigInteger);

            json.Should().NotBeNull();
        }
    }
}