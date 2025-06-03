using Shouldly;

namespace EverscaleNet.Client.Tests.Modules;

public class ProcessModuleTests : IClassFixture<EverClientTestsFixture>
{
    private readonly IEverClient _everClient;

    public ProcessModuleTests(EverClientTestsFixture fixture, ITestOutputHelper outputHelper)
    {
        _everClient = fixture.CreateClient(outputHelper, true);
    }

    [Fact]
    public async Task WaitMessage()
    {
        //arrange
        KeyPair keys = await _everClient.Crypto.GenerateRandomSignKeys();
        ResultOfEncodeMessage encoded = await _everClient.Abi.EncodeMessage(new ParamsOfEncodeMessage
        {
            Abi = TestsEnv.Packages.Events.Abi, DeploySet = new DeploySet
            {
                Tvc = TestsEnv.Packages.Events.Tvc
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

        await _everClient.SendGramsFromLocalGiver(encoded.Address);

        var events = new List<ProcessingEvent>();

        Task ProcessingCallback(ProcessingEvent @event, uint code, CancellationToken cancellationToken)
        {
            code.ShouldBe((uint)100);
            @event.ShouldNotBeNull();
            events.Add(@event);
            return Task.CompletedTask;
        }

        ResultOfSendMessage sendMessageResult = await _everClient.Processing.SendMessage(new ParamsOfSendMessage
        {
            Message = encoded.Message,
            Abi = TestsEnv.Packages.Events.Abi,
            SendEvents = true
        }, ProcessingCallback);

        //act
        ResultOfProcessMessage waitForTransactionResult = await _everClient.Processing.WaitForTransaction(
            new ParamsOfWaitForTransaction
            {
                Message = encoded.Message,
                ShardBlockId = sendMessageResult.ShardBlockId,
                SendEvents = true,
                Abi = TestsEnv.Packages.Events.Abi
            }, ProcessingCallback);

        //assert
        waitForTransactionResult.OutMessages.ShouldBeEmpty();
        waitForTransactionResult.Decoded.OutMessages.ShouldBeEmpty();
        waitForTransactionResult.Decoded.Output.ShouldBeNull();

        events.Count.ShouldBeGreaterThanOrEqualTo(4);
        events[0].ShouldBeOfType<ProcessingEvent.WillFetchFirstBlock>();
        events[1].ShouldBeOfType<ProcessingEvent.WillSend>();
        events[2].ShouldBeOfType<ProcessingEvent.DidSend>();
        events.GetRange(3, events.Count - 3)
            .ShouldAllBe(@event => @event.GetType() == typeof(ProcessingEvent.WillFetchNextBlock));
    }
}