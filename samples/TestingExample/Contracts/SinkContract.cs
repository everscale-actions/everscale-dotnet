namespace TestingExample.Contracts;

internal class SinkContract : ContractBase {
	public SinkContract(IEverClient client, IEverPackageManager packageManager, KeyPair keyPair) : base(client, packageManager, keyPair: keyPair) { }

	protected override string Name => "20_sink";
}
