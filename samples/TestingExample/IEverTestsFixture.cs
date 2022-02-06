namespace TestingExample;

public interface IEverTestsFixture : IAsyncDisposable {
	IEverClient Client { get; }
	IEverPackageManager PackageManager { get; }
	IEverGiver Giver { get; }
	Task Init(ITestOutputHelper output);
}
