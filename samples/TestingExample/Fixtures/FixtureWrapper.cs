namespace TestingExample.Fixtures;

public class FixtureWrapper : IAsyncLifetime {
	private readonly IEverTestsFixture _fixture;

	public FixtureWrapper() {
		_fixture = new EverNodeSeTestsFixture();
	}

	public IEverTestsFixture GetFixture() {
		return _fixture;
	}

	public Task InitializeAsync() {
		return Task.CompletedTask;
	}

	public Task DisposeAsync() {
		return _fixture.DisposeAsync().AsTask();
	}
}
