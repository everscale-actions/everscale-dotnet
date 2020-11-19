using System.Collections.Generic;
using System.Threading.Tasks;
using ch1seL.TonNet.Client.Models;
using FluentAssertions;
using TestsShared;
using Xunit;
using Xunit.Abstractions;

namespace ch1seL.TonNet.Client.Tests.Modules
{
    public class ProcessModuleTests : TonClientTestsBase
    {
        public ProcessModuleTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper, true)
        {
        }

        [Fact]
        public async Task WaitMessage()
        {
            TestPackage eventsPackage = await TestPackage.GetPackage("Events", 2);
            KeyPair keys = await TonClient.Crypto.GenerateRandomSignKeys();
            ResultOfEncodeMessage encoded = await TonClient.Abi.EncodeMessage(new ParamsOfEncodeMessage
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

            await TonClient.SendGramsFromLocalGiver(encoded.Address);

            var events = new List<ProcessingEvent>();

            void ProcessingCallback(ProcessingEvent @event, uint code)
            {
                code.Should().Be(100);
                @event.Should().NotBeNull();
                events.Add(@event);
            }

            ResultOfSendMessage result = await TonClient.Processing.SendMessage(new ParamsOfSendMessage
            {
                Message = encoded.Message,
                Abi = eventsPackage.Abi,
                SendEvents = true
            }, ProcessingCallback);

            ResultOfProcessMessage output = await TonClient.Processing.WaitForTransaction(new ParamsOfWaitForTransaction
            {
                Message = encoded.Message,
                ShardBlockId = result.ShardBlockId,
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