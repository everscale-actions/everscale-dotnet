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

        [Fact(Skip = "WORK IN PROGRESS")]
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

            output.Decoded.Output.Should().NotBeNull();
            output.Decoded.OutMessages.Should().NotBeNull();

            using List<ProcessingEvent>.Enumerator enumerator = events.GetEnumerator();
            Assert.True(enumerator.MoveNext());
            Assert.IsType<ProcessingEvent.WillFetchFirstBlock>(enumerator.Current);
            Assert.True(enumerator.MoveNext());
            Assert.IsType<ProcessingEvent.WillSend>(enumerator.Current);
            Assert.True(enumerator.MoveNext());
            Assert.IsType<ProcessingEvent.DidSend>(enumerator.Current);
            Assert.True(enumerator.MoveNext());
            do
            {
                Assert.IsType<ProcessingEvent.WillFetchNextBlock>(enumerator.Current);
            } while (enumerator.MoveNext());
        }
    }
}