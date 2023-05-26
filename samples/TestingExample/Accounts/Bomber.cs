namespace TestingExample.Accounts;

internal class Bomber : AccountBase {
	public Bomber(IEverClient client, IEverPackageManager packageManager) : base(client, packageManager) { }

	protected override string Name => "20_bomber";

	public async Task<ResultOfProcessMessage> TestSend0(string address, CancellationToken cancellationToken = default) {
		return await Run("testSend0", new { addr = address }, cancellationToken);
	}

	public async Task<ResultOfProcessMessage> TestSend1(string address, CancellationToken cancellationToken = default) {
		return await Run("testSend1", new { addr = address }, cancellationToken);
	}

	public async Task<ResultOfProcessMessage> TestSend128(string address, CancellationToken cancellationToken = default) {
		return await Run("testSend128", new { addr = address }, cancellationToken);
	}

	public async Task<ResultOfProcessMessage> TestSend160(string address, CancellationToken cancellationToken = default) {
		return await Run("testSend160", new { addr = address }, cancellationToken);
	}

	public async Task<ResultOfProcessMessage> Deploy(CancellationToken cancellationToken = default) {
		return await base.Deploy(cancellationToken: cancellationToken);
	}
}
