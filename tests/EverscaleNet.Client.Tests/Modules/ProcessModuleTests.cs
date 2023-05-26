namespace EverscaleNet.Client.Tests.Modules;

public class ProcessModuleTests : IClassFixture<EverClientTestsFixture> {
	private readonly IEverClient _everClient;

	public ProcessModuleTests(EverClientTestsFixture fixture, ITestOutputHelper outputHelper) {
		_everClient = fixture.CreateClient(outputHelper, true);
	}

	[Fact]
	public async Task WaitMessage() {
		//arrange
		KeyPair keys = await _everClient.Crypto.GenerateRandomSignKeys();
		ResultOfEncodeMessage encoded = await _everClient.Abi.EncodeMessage(new ParamsOfEncodeMessage {
			Abi = TestsEnv.Packages.Events.Abi, DeploySet = new DeploySet {
				Tvc = TestsEnv.Packages.Events.Tvc
			},
			CallSet = new CallSet {
				FunctionName = "constructor",
				Header = new FunctionHeader {
					Pubkey = keys.Public
				}
			},
			Signer = new Signer.Keys {
				KeysAccessor = keys
			}
		});

		await _everClient.SendGramsFromLocalGiver(encoded.Address);

		var events = new List<ProcessingEvent>();

		Task ProcessingCallback(ProcessingEvent @event, uint code, CancellationToken cancellationToken) {
			code.Should().Be(100);
			@event.Should().NotBeNull();
			events.Add(@event);
			return Task.CompletedTask;
		}

		ResultOfSendMessage sendMessageResult = await _everClient.Processing.SendMessage(new ParamsOfSendMessage {
			Message = encoded.Message,
			Abi = TestsEnv.Packages.Events.Abi,
			SendEvents = true
		}, ProcessingCallback);

		//act
		ResultOfProcessMessage waitForTransactionResult = await _everClient.Processing.WaitForTransaction(new ParamsOfWaitForTransaction {
			Message = encoded.Message,
			ShardBlockId = sendMessageResult.ShardBlockId,
			SendEvents = true,
			Abi = TestsEnv.Packages.Events.Abi
		}, ProcessingCallback);

		//assert
		waitForTransactionResult.OutMessages.Should().BeEmpty();
		waitForTransactionResult.Decoded.OutMessages.Should().BeEmpty();
		waitForTransactionResult.Decoded.Output.Should().BeNull();

		events.Count.Should().BeGreaterOrEqualTo(4);
		events[0].Should().BeOfType<ProcessingEvent.WillFetchFirstBlock>();
		events[1].Should().BeOfType<ProcessingEvent.WillSend>();
		events[2].Should().BeOfType<ProcessingEvent.DidSend>();
		events.GetRange(3, events.Count - 3).Should().AllBeOfType<ProcessingEvent.WillFetchNextBlock>();
	}
}
