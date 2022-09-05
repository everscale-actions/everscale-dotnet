using EverscaleNet;

namespace BlazorApp.Contracts;

internal class AccumulatorContract : ContractBase {
	private readonly IEverClient _client;
	private readonly KeyPair _keyPair;

	public AccumulatorContract(IEverClient client, IEverPackageManager packageManager, KeyPair keyPair) : base(client, packageManager, keyPair: keyPair) {
		_client = client;
		_keyPair = keyPair;
	}

	protected override string Name => "1_Accumulator";

	public async Task Add(int value, CancellationToken cancellationToken = default) {
		await _client.ProcessAndWaitInternalMessages(new ParamsOfEncodeMessage {
			Address = Address,
			Abi = await GetAbi(cancellationToken),
			CallSet = new CallSet {
				FunctionName = "add",
				Input = new { value }.ToJsonElement()
			},
			Signer = new Signer.Keys { KeysAccessor = _keyPair }
		}, cancellationToken);
	}

	public async Task Subtract(int value, CancellationToken cancellationToken = default) {
		await _client.ProcessAndWaitInternalMessages(new ParamsOfEncodeMessage {
			Address = Address,
			Abi = await GetAbi(cancellationToken),
			CallSet = new CallSet {
				FunctionName = "subtract",
				Input = new { value }.ToJsonElement()
			},
			Signer = new Signer.Keys { KeysAccessor = _keyPair }
		}, cancellationToken);
	}

	public async Task<long> GetSum(CancellationToken cancellationToken = default) {
		ResultOfQueryCollection accountBocResult = await _client.Net.QueryCollection(new ParamsOfQueryCollection {
			Collection = "accounts",
			Filter = new { id = new { eq = Address } }.ToJsonElement(),
			Result = "boc",
			Limit = 1
		}, cancellationToken);

		var boc = accountBocResult.Result[0].Get<string>("boc");

		ResultOfParse parse = await _client.Boc.ParseAccount(new ParamsOfParse {
			Boc = boc
		}, cancellationToken);

		ResultOfDecodeAccountData data = await _client.Abi.DecodeAccountData(new ParamsOfDecodeAccountData {
			Abi = await GetAbi(cancellationToken),
			Data = parse.Parsed.Get<string>("data")
		}, cancellationToken);

		return long.Parse(data.Data.Get<string>("sum"));
	}
}
