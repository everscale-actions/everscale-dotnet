namespace TestingExample.Contracts;

internal class SinkAccount : AccountBase {
	public SinkAccount(IEverClient client, IEverPackageManager packageManager) : base(client, packageManager) { }

	protected override string Name => "20_sink";

	public async Task<ResultOfProcessMessage> Deploy(CancellationToken cancellationToken = default) {
		return await base.Deploy(cancellationToken: cancellationToken);
	}
}
