using System.Collections.Generic;
using System.Threading.Tasks;
using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Models;
using ch1seL.TonNet.Client.Tests.Utils;
using ch1seL.TonNet.TestsShared;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace ch1seL.TonNet.Client.Tests.Modules;

public class ProcessModuleTests : IClassFixture<TonClientTestsFixture> {
	public ProcessModuleTests(TonClientTestsFixture fixture, ITestOutputHelper outputHelper) {
		_tonClient = fixture.CreateClient(outputHelper, true);
	}

	private readonly ITonClient _tonClient;

	[Fact]
	public async Task WaitMessage() {
		//arrange
		KeyPair keys = await _tonClient.Crypto.GenerateRandomSignKeys();
		ResultOfEncodeMessage encoded = await _tonClient.Abi.EncodeMessage(new ParamsOfEncodeMessage {
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

		await _tonClient.SendGramsFromLocalGiver(encoded.Address);

		var events = new List<ProcessingEvent>();

		void ProcessingCallback(ProcessingEvent @event, uint code) {
			code.Should().Be(100);
			@event.Should().NotBeNull();
			events.Add(@event);
		}

		ResultOfSendMessage sendMessageResult = await _tonClient.Processing.SendMessage(new ParamsOfSendMessage {
			Message = encoded.Message,
			Abi = TestsEnv.Packages.Events.Abi,
			SendEvents = true
		}, ProcessingCallback);

		//act
		ResultOfProcessMessage waitForTransactionResult = await _tonClient.Processing.WaitForTransaction(new ParamsOfWaitForTransaction {
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