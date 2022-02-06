namespace TestingExample.Contracts;

internal class BomberContract : ContractBase {
	private readonly IEverClient _client;
	private readonly KeyPair _keyPair;

	public BomberContract(IEverClient client, IEverPackageManager packageManager, KeyPair keyPair) : base(client, packageManager, keyPair: keyPair) {
		_client = client;
		_keyPair = keyPair;
	}

	protected override string Name => "20_bomber";

	public async Task<ProcessAndWaitInternalMessagesResult> TestSend0(string address, CancellationToken cancellationToken = default) {
		return await _client.ProcessAndWaitInternalMessages(new ParamsOfEncodeMessage {
			Address = Address,
			Abi = await GetAbi(cancellationToken),
			CallSet = new CallSet {
				FunctionName = "testSend0",
				Input = new { addr = address }.ToJsonElement()
			},
			Signer = new Signer.Keys { KeysAccessor = _keyPair }
		}, cancellationToken);
	}

	public async Task<ProcessAndWaitInternalMessagesResult> TestSend1(string address, CancellationToken cancellationToken = default) {
		return await _client.ProcessAndWaitInternalMessages(new ParamsOfEncodeMessage {
			Address = Address,
			Abi = await GetAbi(cancellationToken),
			CallSet = new CallSet {
				FunctionName = "testSend1",
				Input = new { addr = address }.ToJsonElement()
			},
			Signer = new Signer.Keys { KeysAccessor = _keyPair }
		}, cancellationToken);
	}

	public async Task<ProcessAndWaitInternalMessagesResult> TestSend128(string address, CancellationToken cancellationToken = default) {
		return await _client.ProcessAndWaitInternalMessages(new ParamsOfEncodeMessage {
			Address = Address,
			Abi = await GetAbi(cancellationToken),
			CallSet = new CallSet {
				FunctionName = "testSend128",
				Input = new { addr = address }.ToJsonElement()
			},
			Signer = new Signer.Keys { KeysAccessor = _keyPair }
		}, cancellationToken);
	}

	public async Task<ProcessAndWaitInternalMessagesResult> TestSend160(string address, CancellationToken cancellationToken = default) {
		return await _client.ProcessAndWaitInternalMessages(new ParamsOfEncodeMessage {
			Address = Address,
			Abi = await GetAbi(cancellationToken),
			CallSet = new CallSet {
				FunctionName = "testSend160",
				Input = new { addr = address }.ToJsonElement()
			},
			Signer = new Signer.Keys { KeysAccessor = _keyPair }
		}, cancellationToken);
	}
}
