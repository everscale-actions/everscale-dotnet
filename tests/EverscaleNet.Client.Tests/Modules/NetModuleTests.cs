using Shouldly;

namespace EverscaleNet.Client.Tests.Modules;

public class NetModuleTests : IClassFixture<EverClientTestsFixture> {
	private readonly IEverClient _everClient;

	private readonly EverClientTestsFixture _fixture;
	private readonly ITestOutputHelper _outputHelper;

	public NetModuleTests(EverClientTestsFixture fixture, ITestOutputHelper outputHelper) {
		_fixture = fixture;
		_outputHelper = outputHelper;
		_everClient = fixture.CreateClient(outputHelper, true);
	}

	private IEverClient GetNewClient() {
		return _fixture.CreateClient(_outputHelper, true);
	}

	[Fact]
	public async Task AllAccounts() {
		ResultOfQueryCollection result = await _everClient.Net.QueryCollection(new ParamsOfQueryCollection {
			Collection = "accounts",
			Filter = new { }.ToJsonElement(),
			Result = "id balance"
		}, TestContext.Current.CancellationToken);

		result.Result.ShouldNotBeEmpty();
	}

	[Fact]
	public async Task BlockSignatures() {
		ResultOfQueryCollection result = await _everClient.Net.QueryCollection(new ParamsOfQueryCollection {
			Collection = "blocks_signatures",
			Filter = new { }.ToJsonElement(),
			Result = "id",
			Limit = 1
		}, TestContext.Current.CancellationToken);

		result.ShouldNotBeNull();
	}

	[Fact]
	public async Task FindLastShardBlock() {
		ResultOfFindLastShardBlock block = await _everClient.Net.FindLastShardBlock(new ParamsOfFindLastShardBlock {
			Address = TestsEnv.SeGiver.Address
		}, TestContext.Current.CancellationToken);

		block.BlockId.ShouldNotBeNull();
		block.BlockId.Length.ShouldBe(64);
	}

	[Fact]
	public async Task Query() {
		ResultOfQuery result = await _everClient.Net.Query(new ParamsOfQuery {
			Query = "query{info{version}}"
		}, TestContext.Current.CancellationToken);

		var resultParsed = result.Result!.ToPrototype(new { data = new { info = new { version = default(string) } } });
		resultParsed!.data.info.version.Split('.').Length.ShouldBe(3);
	}

	[Fact]
	public async Task Ranges() {
		ResultOfQueryCollection result = await _everClient.Net.QueryCollection(new ParamsOfQueryCollection {
			Collection = "messages",
			Filter = new { created_at = new { gt = 1562342740 } }.ToJsonElement(),
			Result = "body created_at"
		}, TestContext.Current.CancellationToken);

		result.Result.ShouldNotBeEmpty();
		result.Result?[0].Get<ulong>("created_at").ShouldBeGreaterThan((ulong)1562342740);
	}

	[Fact]
	public async Task SubscribeForMessages() {
		object messagesLock = new();
		var messages = new List<JsonElement>();

		var callback = new Func<JsonElement, uint, CancellationToken, Task>((serdeJson, responseType, _) =>
		{
			JsonElement message = (ResponseType)responseType switch {
				ResponseType.Custom => new { result = serdeJson }.ToJsonElement(),
				_ => throw new EverClientException("bad callback gotten")
			};
			lock (messagesLock) {
				messages.Add(message);
			}

			return Task.CompletedTask;
		});

		//act
		ResultOfSubscribeCollection handle = await _everClient.Net.SubscribeCollection(new ParamsOfSubscribeCollection {
			Collection = "messages",
			Filter = new { dst = new { eq = "1" } }.ToJsonElement(),
			Result = "id"
		}, callback, TestContext.Current.CancellationToken);
		await _everClient.SendGramsFromLocalGiver(cancellationToken: TestContext.Current.CancellationToken);
		await _everClient.Net.Unsubscribe(new ResultOfSubscribeCollection {
			Handle = handle.Handle
		}, TestContext.Current.CancellationToken);

		// arrange
		messages.Count.ShouldBe(0);
	}

	[Fact]
	public async Task SubscribeForTransactionsWithAddresses() {
		KeyPair keys = await _everClient.Crypto.GenerateRandomSignKeys(TestContext.Current.CancellationToken);
		IEverClient subscriptionClient = _fixture.CreateClient(_outputHelper, true);

		var transactions = new List<string>();
		var addresses = new List<string>();
		var errorCodes = new List<uint>();
		object @lock = new();

		var deployParams = new ParamsOfEncodeMessage {
			Abi = TestsEnv.Packages.Hello.Abi,
			DeploySet = new DeploySet { Tvc = TestsEnv.Packages.Hello.Tvc },
			Signer = new Signer.Keys { KeysAccessor = keys },
			CallSet = new CallSet { FunctionName = "constructor" }
		};
		ResultOfEncodeMessage msg = await _everClient.Abi.EncodeMessage(deployParams, TestContext.Current.CancellationToken);
		string address = msg.Address;

		var callback = new Func<JsonElement, uint, CancellationToken, Task>((serdeJson, responseType, _) =>
		{
			switch ((SubscriptionResponseType)responseType) {
				case SubscriptionResponseType.Ok:
					var result = serdeJson.ToPrototype(new
						{ result = new { id = default(string), account_addr = default(string) } }).result;
					lock (@lock) {
						transactions.Add(result.id);
						addresses.Add(result.account_addr);
					}

					break;
				case SubscriptionResponseType.Error:
					var error = serdeJson.ToObject<ClientError>();
					_outputHelper.WriteLine($">> Error: {serdeJson}");
					lock (@lock) {
						errorCodes.Add(error.Code);
					}

					break;
				default:
					throw new EverClientException($"Unknown SubscriptionResponseType: {responseType}");
			}

			return Task.CompletedTask;
		});

		//act
		ResultOfSubscribeCollection handle1 = await subscriptionClient.Net.SubscribeCollection(
			                                      new ParamsOfSubscribeCollection {
				                                      Collection = "transactions",
				                                      Filter = new {
					                                      account_addr = new { eq = address },
					                                      status = new { eq = (int)TransactionProcessingStatus.Finalized }
				                                      }.ToJsonElement(),
				                                      Result = "id account_addr"
			                                      }, callback, TestContext.Current.CancellationToken);
		// send grams to create first transaction
		await _everClient.SendGramsFromLocalGiver(address, TestContext.Current.CancellationToken);

		// give some time for subscription to receive all data
		await Task.Delay(TimeSpan.FromSeconds(1), TestContext.Current.CancellationToken);

		int transactionCount1 = transactions.Count;

		// second handler
		ResultOfSubscribeCollection handle2 = await subscriptionClient.Net.SubscribeCollection(
			                                      new ParamsOfSubscribeCollection {
				                                      Collection = "transactions",
				                                      Filter = new {
					                                      account_addr = new { eq = address },
					                                      status = new { eq = (int)TransactionProcessingStatus.Finalized }
				                                      }.ToJsonElement(),
				                                      Result = "id account_addr"
			                                      }, callback, TestContext.Current.CancellationToken);
		// suspend subscription
		await subscriptionClient.Net.Suspend(TestContext.Current.CancellationToken);

		// deploy to create second transaction
		await _everClient.Processing.ProcessMessage(new ParamsOfProcessMessage {
			MessageEncodeParams = deployParams,
			SendEvents = false
		}, cancellationToken: TestContext.Current.CancellationToken);

		// give some time for subscription to receive all data
		await Task.Delay(TimeSpan.FromSeconds(2), TestContext.Current.CancellationToken);

		// check that second transaction is not received when subscription suspended
		int transactionCount2 = transactions.Count;

		// resume subscription
		await subscriptionClient.Net.Resume(TestContext.Current.CancellationToken);

		// run contract function to create third transaction
		await _everClient.Processing.ProcessMessage(new ParamsOfProcessMessage {
			MessageEncodeParams = new ParamsOfEncodeMessage {
				Abi = TestsEnv.Packages.Hello.Abi,
				Signer = new Signer.Keys { KeysAccessor = keys },
				Address = address,
				CallSet = new CallSet { FunctionName = "touch" }
			},
			SendEvents = false
		}, cancellationToken: TestContext.Current.CancellationToken);

		// give some time for subscription to receive all data
		await Task.Delay(TimeSpan.FromSeconds(2), TestContext.Current.CancellationToken);

		await Task.WhenAll(
			subscriptionClient.Net.Unsubscribe(new ResultOfSubscribeCollection { Handle = handle1.Handle },
				TestContext.Current.CancellationToken),
			subscriptionClient.Net.Unsubscribe(new ResultOfSubscribeCollection { Handle = handle2.Handle }, TestContext.Current.CancellationToken)
		);

		// check count before suspending 
		transactionCount1.ShouldBe(1);

		// check count before resume
		transactionCount2.ShouldBe(1);

		// ensure that all transactions have correct address
		addresses.ShouldAllBe(s => s == address);

		// check errors
		errorCodes.Count.ShouldBe(4);
		errorCodes.Take(2).ShouldAllBe(u => u == (uint)NetErrorCode.NetworkModuleSuspended);
		errorCodes.TakeLast(2).ShouldAllBe(u => u == (uint)NetErrorCode.NetworkModuleResumed);

		// check that second and third transaction are received by all handlers
		transactions.Count.ShouldBe(3);
	}

	[Fact]
	public async Task WaitFor() {
		long now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		// get new client here to avoid lock another tests
		var request = GetNewClient().Net.WaitForCollection(new ParamsOfWaitForCollection {
			Collection = "transactions",
			Filter = new { now = new { gt = now } }.ToJsonElement(),
			Result = "id now"
		}, TestContext.Current.CancellationToken);

		await Task.Delay(TimeSpan.FromSeconds(1), TestContext.Current.CancellationToken);

		await _everClient.SendGramsFromLocalGiver(cancellationToken: TestContext.Current.CancellationToken);

		ResultOfWaitForCollection result = await request;
		result.Result!.Get<long>("now").ShouldBeGreaterThan(now);
	}

	// todo: not working yet https://t.me/ton_sdk/7063?thread=7032
	// [Fact]
	// public async Task TestEndpoints()
	// {
	//     IEverClient client = _fixture.CreateClient(_outputHelper,
	//         configureOptions: options => options.Network.Endpoints = new[] {"cinet.tonlabs.io", "cinet2.tonlabs.io/"});
	//
	//     Func<Task> act = async () =>
	//     {
	//         EndpointsSet endpoints = await client.Net.FetchEndpoints();
	//         await client.Net.SetEndpoints(endpoints);
	//     };
	//
	//     await act.Should().NotThrowAsync();
	// }
}