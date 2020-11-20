using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Models;
using ch1seL.TonNet.Client.Tests.Utils;
using ch1seL.TonNet.RustClient.RustInterop.Models;
using ch1seL.TonNet.Serialization;
using ch1seL.TonNet.TestsShared;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace ch1seL.TonNet.Client.Tests.Modules
{
    public class NetModuleTests : IClassFixture<TonClientTestsFixture>
    {
        private readonly TonClientTestsFixture _fixture;
        private readonly ITestOutputHelper _outputHelper;
        private readonly ITonClient _tonClient;

        public NetModuleTests(TonClientTestsFixture fixture, ITestOutputHelper outputHelper)
        {
            _fixture = fixture;
            _outputHelper = outputHelper;
            _tonClient = fixture.CreateClient(outputHelper, true);
        }

        private ITonClient GetNewClient()
        {
            return _fixture.CreateClient(_outputHelper, true);
        }

        [Fact]
        public async Task QueryCollectionBLockSignatures()
        {
            ResultOfQueryCollection result = await _tonClient.Net.QueryCollection(new ParamsOfQueryCollection
            {
                Collection = "blocks_signatures",
                Filter = new { }.ToJsonElement(),
                Result = "id",
                Limit = 1
            });

            result.Should().NotBeNull();
        }

        [Fact]
        public async Task QueryCollectionAllAccounts()
        {
            ResultOfQueryCollection result = await _tonClient.Net.QueryCollection(new ParamsOfQueryCollection
            {
                Collection = "accounts",
                Filter = new { }.ToJsonElement(),
                Result = "id balance"
            });

            result.Result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task QueryCollectionRanges()
        {
            ResultOfQueryCollection result = await _tonClient.Net.QueryCollection(new ParamsOfQueryCollection
            {
                Collection = "messages",
                Filter = new {created_at = new {gt = 1562342740}}.ToJsonElement(),
                Result = "body created_at"
            });

            Assert.NotEmpty(result.Result);
            Assert.True(result.Result?[0].Get<ulong>("created_at") > 1562342740);
        }

        [Fact]
        public async Task WaitFor()
        {
            var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            // get new client here to avoid lock another tests
            var request = GetNewClient().Net.WaitForCollection(new ParamsOfWaitForCollection
            {
                Collection = "transactions",
                Filter = new {now = new {gt = now}}.ToJsonElement(),
                Result = "id now"
            });

            await _tonClient.SendGramsFromLocalGiver();

            ResultOfWaitForCollection result = await request;
            result.Result.Get<long>("now").Should().BeGreaterThan(now);
        }

        [Fact]
        public async Task SubscribeForTransactionsWithAddresses()
        {
            KeyPair keys = await _tonClient.Crypto.GenerateRandomSignKeys();
            PackageHelpers package = await PackageHelpers.GetPackage("Hello");

            var deployParams = new ParamsOfEncodeMessage
            {
                Abi = package.Abi,
                DeploySet = new DeploySet {Tvc = package.Tvc},
                Signer = new Signer.Keys {KeysAccessor = keys},
                CallSet = new CallSet {FunctionName = "constructor"}
            };

            ResultOfEncodeMessage msg = await _tonClient.Abi.EncodeMessage(deployParams);
            var transactions = new List<JsonElement>();
            var transactionsLock = new object();
            var address = msg.Address;

            var callback = new Action<JsonElement, uint>((serdeJson, responseType) =>
            {
                JsonElement result = (ResponseType) responseType switch
                {
                    ResponseType.Custom => serdeJson.GetProperty("result"),
                    _ => throw new TonClientException("bad callback gotten")
                };
                lock (transactionsLock)
                {
                    transactions.Add(result);
                }
            });

            //act
            ResultOfSubscribeCollection handle = await _tonClient.Net.SubscribeCollection(new ParamsOfSubscribeCollection
            {
                Collection = "transactions",
                Filter = new
                {
                    account_addr = new {eq = address},
                    status = new {eq = (int) TransactionProcessingStatus.Finalized}
                }.ToJsonElement(),
                Result = "id account_addr"
            }, callback);
            await _tonClient.DeployWithGiver(deployParams);
            await Task.Delay(TimeSpan.FromSeconds(1));
            await _tonClient.Net.Unsubscribe(new ResultOfSubscribeCollection
            {
                Handle = handle.Handle
            });

            //assert
            transactions.Count.Should().Be(2);
            transactions.Select(t => t.Get<string>("account_addr")).Should().BeEquivalentTo(address, address);
            transactions[0].Get<string>("id").Should().NotBe(transactions[1].Get<string>("id"));
        }

        [Fact]
        public async Task SubscribeForMessages()
        {
            var messagesLock = new object();
            var messages = new List<JsonElement>();

            var callback = new Action<JsonElement, uint>((serdeJson, responseType) =>
            {
                JsonElement message = (ResponseType) responseType switch
                {
                    ResponseType.Custom => new {result = serdeJson}.ToJsonElement(),
                    _ => throw new TonClientException("bad callback gotten")
                };
                lock (messagesLock)
                {
                    messages.Add(message);
                }
            });

            //act
            ResultOfSubscribeCollection handle = await _tonClient.Net.SubscribeCollection(new ParamsOfSubscribeCollection
            {
                Collection = "messages",
                Filter = new {dst = new {eq = "1"}}.ToJsonElement(),
                Result = "id"
            }, callback);
            await _tonClient.SendGramsFromLocalGiver();
            await _tonClient.Net.Unsubscribe(new ResultOfSubscribeCollection
            {
                Handle = handle.Handle
            });

            // arrange
            messages.Count.Should().Be(0);
        }
    }
}