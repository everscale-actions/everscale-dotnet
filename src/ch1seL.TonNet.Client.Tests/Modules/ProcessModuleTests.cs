using System.Collections.Generic;
using System.Threading.Tasks;
using ch1seL.TonNet.Client.Models;
using ch1seL.TonNet.Client.Tests.Utils;
using FluentAssertions;
using TestsShared;
using Xunit;
using Xunit.Abstractions;

namespace ch1seL.TonNet.Client.Tests.Modules
{
    public class ProcessModuleTests : IClassFixture<TonClientTestsFixture>
    {
        private readonly ITonClient _tonClient;

        public ProcessModuleTests(TonClientTestsFixture fixture, ITestOutputHelper outputHelper)
        {
            _tonClient = fixture.CreateClient(outputHelper, true);
        }

        [Fact]
        public async Task WaitMessage()
        {
            TestPackage eventsPackage = await TestPackage.GetPackage("Events", 2);
            KeyPair keys = await _tonClient.Crypto.GenerateRandomSignKeys();
            ResultOfEncodeMessage encoded = await _tonClient.Abi.EncodeMessage(new ParamsOfEncodeMessage
            {
                Abi = eventsPackage.Abi,
                DeploySet = new DeploySet
                {
                    Tvc = eventsPackage.Tvc
                },
                CallSet = new CallSet
                {
                    FunctionName = "constructor",
                    Header = new FunctionHeader
                    {
                        Pubkey = keys.Public
                    }
                },
                Signer = new Signer.Keys
                {
                    KeysAccessor = keys
                }
            });

            await _tonClient.SendGramsFromLocalGiver(encoded.Address);

            var events = new List<ProcessingEvent>();

            void ProcessingCallback(ProcessingEvent @event, uint code)
            {
                code.Should().Be(100);
                @event.Should().NotBeNull();
                events.Add(@event);
            }

            ResultOfSendMessage sendMessageResult = await _tonClient.Processing.SendMessage(new ParamsOfSendMessage
            {
                Message = encoded.Message,
                Abi = eventsPackage.Abi,
                SendEvents = true
            }, ProcessingCallback);

            ResultOfProcessMessage waitForTransactionResult = await _tonClient.Processing.WaitForTransaction(new ParamsOfWaitForTransaction
            {
                Message = encoded.Message,
                ShardBlockId = sendMessageResult.ShardBlockId,
                SendEvents = true,
                Abi = eventsPackage.Abi
            }, ProcessingCallback);


            events[0].Should().BeOfType<ProcessingEvent.WillFetchFirstBlock>();
            events[1].Should().BeOfType<ProcessingEvent.WillSend>();
            events[2].Should().BeOfType<ProcessingEvent.DidSend>();
            events.GetRange(3, events.Count - 3).Should().AllBeOfType<ProcessingEvent.WillFetchNextBlock>();
        }
    }
}