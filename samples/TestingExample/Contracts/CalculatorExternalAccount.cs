namespace TestingExample.Contracts;

internal class CalculatorExternalAccount : AccountBase {
	private readonly IEverClient _client;

	public CalculatorExternalAccount(IEverClient client, IEverPackageManager packageManager, KeyPair keyPair) : base(
		client, packageManager, keyPair) {
		_client = client;
	}

	protected override string Name => "CalculatorExternal";

	public async Task<ResultOfProcessMessage> Add(int value, CancellationToken cancellationToken = default) {
		return await Run(new CallSet {
			FunctionName = "add",
			Input = new { value }.ToJsonElement()
		}, cancellationToken);
	}

	public async Task<ResultOfProcessMessage> Subtract(int value, CancellationToken cancellationToken = default) {
		return await Run(new CallSet {
			FunctionName = "subtract",
			Input = new { value }.ToJsonElement()
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

	public async Task<ResultOfProcessMessage> Deploy(CancellationToken cancellationToken = default) {
		return await base.Deploy(cancellationToken: cancellationToken);
	}
}
