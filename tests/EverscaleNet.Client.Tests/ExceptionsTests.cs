using Shouldly;

namespace EverscaleNet.Client.Tests;

public class ExceptionsTests(EverClientTestsFixture fixture, ITestOutputHelper outputHelper) : IClassFixture<EverClientTestsFixture> {
	private readonly IEverClient _everClient = fixture.CreateClient(outputHelper);

	[Fact(Timeout = 5000)]
	public async Task ThrowEverClientException() {
		var act = async () =>
		{
			await _everClient.Crypto.MnemonicDeriveSignKeys(new ParamsOfMnemonicDeriveSignKeys {
				Phrase = "abandon math mimic master filter design carbon crystal rookie group knife young",
				Dictionary = MnemonicDictionary.Ton
			}, TestContext.Current.CancellationToken);
		};

		var ex = await act.ShouldThrowAsync<EverClientException>();
		ex.Code.ShouldBe((uint)CryptoErrorCode.Bip39InvalidPhrase);
		ex.Message.ShouldStartWith("Invalid bip39 phrase:");
	}
}