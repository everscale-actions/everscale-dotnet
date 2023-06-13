using EverscaleNet.Abstract;
using EverscaleNet.Client.Models;

namespace TestingExample.Accounts;

internal class Sink : AccountBase {
	public Sink(IEverClient client, IEverPackageManager packageManager) : base(client, packageManager) { }

	protected override string Name => "20_sink";

	public async Task<ResultOfProcessMessage> Deploy(CancellationToken cancellationToken = default) {
		return await base.Deploy(cancellationToken: cancellationToken);
	}
}
