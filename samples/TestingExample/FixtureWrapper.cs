namespace TestingExample;

public class FixtureWrapper {
	private readonly IEverTestsFixture _fixture;

	public FixtureWrapper() {
		_fixture = new EverNodeSeTestsFixture();
	}

	public IEverTestsFixture GetFixture() {
		return _fixture;
	}
}
