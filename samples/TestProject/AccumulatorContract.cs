using EverscaleNet.Testing;

namespace TestProject;

public class AccumulatorContract : ContractBase {
	private readonly IEverClient _client;

	public AccumulatorContract(IEverClient client, IEverPackageManager packageManager, string? address = null) : base(client, packageManager, address) {
		_client = client;
	}

	protected override string Name { get; } = "1_Accumulator";
	
	

	public async Task Add(uint value, CancellationToken cancellationToken) {
		
		
		await _client.ProcessEncodeMessageAndWaitTransaction(new ParamsOfEncodeMessage() {
			Abi = await GetAbi(cancellationToken),
			
		}, cancellationToken)
	}
}
