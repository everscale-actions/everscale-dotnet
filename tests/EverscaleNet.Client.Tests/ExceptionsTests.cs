namespace EverscaleNet.Client.Tests;

public class ExceptionsTests : IClassFixture<EverClientTestsFixture> {
	private readonly IEverClient _everClient;

	public ExceptionsTests(EverClientTestsFixture fixture, ITestOutputHelper outputHelper) {
		_everClient = fixture.CreateClient(outputHelper);
	}

	[Fact(Timeout = 5000)]
	public async Task ThrowEverClientException() {
		Func<Task> act = async () => {
			await _everClient.Crypto.MnemonicDeriveSignKeys(new ParamsOfMnemonicDeriveSignKeys {
				Phrase = "abandon math mimic master filter design carbon crystal rookie group knife young",
				Dictionary = MnemonicDictionary.Ton
			});
		};

		ExceptionAssertions<EverClientException> exceptionAssertions = await act.Should().ThrowAsync<EverClientException>();
		exceptionAssertions.Which.Code.Should().Be((uint)CryptoErrorCode.Bip39InvalidPhrase);
		exceptionAssertions.Which.Message.Should().StartWith("Invalid bip39 phrase:");
	}
}
