using System;
using System.Threading.Tasks;
using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Models;
using FluentAssertions;
using FluentAssertions.Specialized;
using Xunit;
using Xunit.Abstractions;

namespace ch1seL.TonNet.Client.Tests;

public class ExceptionsTests : IClassFixture<TonClientTestsFixture> {
	public ExceptionsTests(TonClientTestsFixture fixture, ITestOutputHelper outputHelper) {
		_tonClient = fixture.CreateClient(outputHelper);
	}

	private readonly ITonClient _tonClient;

	[Fact(Timeout = 5000)]
	public async Task ThrowTonClientException() {
		Func<Task> act = async () => { await _tonClient.Crypto.MnemonicFromRandom(new ParamsOfMnemonicFromRandom { WordCount = 111 }); };

		ExceptionAssertions<TonClientException> exceptionAssertions = await act.Should().ThrowAsync<TonClientException>();
		exceptionAssertions.Which.Code.Should().Be((uint)CryptoErrorCode.Bip39InvalidWordCount);
		exceptionAssertions.Which.Message.Should().StartWith("Invalid mnemonic word count");
	}
}