namespace TestingExample.Contracts;

internal class BomberAccount : AccountBase {
	public BomberAccount(IEverClient client, IEverPackageManager packageManager) : base(client, packageManager) { }

	protected override string Name => "20_bomber";

	public async Task<ResultOfProcessMessage> TestSend0(string address, CancellationToken cancellationToken = default) {
		return await Run(new CallSet {
			FunctionName = "testSend0",
			Input = new { addr = address }.ToJsonElement()
		}, cancellationToken);
	}

	public async Task<ResultOfProcessMessage> TestSend1(string address, CancellationToken cancellationToken = default) {
		return await Run(new CallSet {
			FunctionName = "testSend1",
			Input = new { addr = address }.ToJsonElement()
		}, cancellationToken);
	}

	public async Task<ResultOfProcessMessage> TestSend128(string address, CancellationToken cancellationToken = default) {
		return await Run(new CallSet {
			FunctionName = "testSend128",
			Input = new { addr = address }.ToJsonElement()
		}, cancellationToken);
	}

	public async Task<ResultOfProcessMessage> TestSend160(string address, CancellationToken cancellationToken = default) {
		return await Run(new CallSet {
			FunctionName = "testSend160",
			Input = new { addr = address }.ToJsonElement()
		}, cancellationToken);
	}

	public async Task<ResultOfProcessMessage> Deploy(CancellationToken cancellationToken = default) {
		return await base.Deploy(cancellationToken: cancellationToken);
	}
}
