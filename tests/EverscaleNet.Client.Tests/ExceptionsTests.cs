using FluentAssertions;
using FluentAssertions.Specialized;
using Xunit;
using Xunit.Abstractions;

namespace EverscaleNet.Client.Tests;

public class ExceptionsTests : IClassFixture<EverClientTestsFixture> {
	private readonly IEverClient _everClient;

	public ExceptionsTests(EverClientTestsFixture fixture, ITestOutputHelper outputHelper) {
		_everClient = fixture.CreateClient(outputHelper);
	}

	[Fact(Timeout = 5000)]
	public async Task ThrowEverClientException() {
		Func<Task> act = async () => { await _everClient.Crypto.MnemonicFromRandom(new ParamsOfMnemonicFromRandom { WordCount = 111 }); };

		ExceptionAssertions<EverClientException> exceptionAssertions = await act.Should().ThrowAsync<EverClientException>();
		exceptionAssertions.Which.Code.Should().Be((uint)CryptoErrorCode.Bip39InvalidWordCount);
		exceptionAssertions.Which.Message.Should().StartWith("Invalid mnemonic word count");
	}
}
