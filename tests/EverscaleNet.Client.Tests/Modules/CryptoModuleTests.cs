using System.Collections;
using EverscaleNet.Client.Tests.Utils;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace EverscaleNet.Client.Tests.Modules;

public class CryptoModuleTests : IClassFixture<EverClientTestsFixture> {
	private readonly IEverClient _everClient;

	public CryptoModuleTests(EverClientTestsFixture fixture, ITestOutputHelper outputHelper) {
		_everClient = fixture.CreateClient(outputHelper);
	}

	[Theory]
	[InlineData("Test Message", true)]
	[InlineData("Test Message 1", false)]
	public async Task NaclSignDetachedVerify(string message, bool success) {
		const string signature =
			"fb0cfe40eea5d6c960652e6ceb904da8a72ee2fcf6e05089cf835203179ff65bb48c57ecf31dcfcd26510bea67e64f3e6898b7c58300dc14338254268cade103";
		const string @public = "1869b7ef29d58026217e9cf163cbfbd0de889bdf1bf4daebf5433a312f5b8d6e";

		ResultOfNaclSignDetachedVerify result = await _everClient.Crypto.NaclSignDetachedVerify(
			                                        new ParamsOfNaclSignDetachedVerify {
				                                        Signature = signature,
				                                        Public = @public,
				                                        Unsigned = message.ToBase64()
			                                        });

		result.Succeeded.Should().Be(success);
	}

	[Theory]
	[ClassData(typeof(MnemonicFromRandomData))]
	public async Task MnemonicFromRandom(MnemonicDictionary? dictionary, byte? wordCount) {
		ResultOfMnemonicFromRandom result = await _everClient.Crypto.MnemonicFromRandom(
			                                    new ParamsOfMnemonicFromRandom {
				                                    Dictionary = dictionary,
				                                    WordCount = wordCount
			                                    });

		result.Phrase.Split(" ").Length.Should().Be(wordCount ?? 12);
	}

	[Theory]
	[ClassData(typeof(MnemonicFromRandomData))]
	public async Task MnemonicFromRandomVerify(MnemonicDictionary? dictionary, byte? wordCount) {
		ResultOfMnemonicFromRandom result = await _everClient.Crypto.MnemonicFromRandom(
			                                    new ParamsOfMnemonicFromRandom {
				                                    Dictionary = dictionary,
				                                    WordCount = wordCount
			                                    });

		ResultOfMnemonicVerify verifyResult = await _everClient.Crypto.MnemonicVerify(new ParamsOfMnemonicVerify {
			Phrase = result.Phrase,
			Dictionary = dictionary,
			WordCount = wordCount
		});

		verifyResult.Valid.Should().BeTrue();
	}

	[Fact]
	public async Task Chacha20() {
		//arrange
		string key = string.Join(null, Enumerable.Repeat("01", 32));
		string nonce = string.Join(null, Enumerable.Repeat("ff", 12));

		//act
		ResultOfChaCha20 encrypted = await _everClient.Crypto.Chacha20(new ParamsOfChaCha20 {
			Data = "Message".ToBase64(),
			Key = key,
			Nonce = nonce
		});

		//assert
		encrypted.Data.Should().Be("w5QOGsJodQ==");

		//act
		ResultOfChaCha20 decrypted = await _everClient.Crypto.Chacha20(new ParamsOfChaCha20 {
			Data = encrypted.Data,
			Key = key,
			Nonce = nonce
		});

		//assert
		decrypted.Data.Should().Be("TWVzc2FnZQ==");
	}

	[Fact]
	public async Task ConvertPublicKeyToTonSafeFormat() {
		ResultOfConvertPublicKeyToTonSafeFormat result = await _everClient.Crypto.ConvertPublicKeyToTonSafeFormat(
			                                                 new ParamsOfConvertPublicKeyToTonSafeFormat {
				                                                 PublicKey = "06117f59ade83e097e0fb33e5d29e8735bda82b3bf78a015542aaa853bb69600"
			                                                 });

		result.TonPublicKey.Should().Be("PuYGEX9Zreg-CX4Psz5dKehzW9qCs794oBVUKqqFO7aWAOTD");
	}

	[Fact]
	public async Task EncryptDecryptWithNaclSecretBox() {
		const string nonce = "2a33564717595ebe53d91a785b9e068aba625c8453a76e45";
		const string key = "8f68445b4e78c000fe4d6b7fc826879c1e63e3118379219a754ae66327764bd8";
		const string text = "Text with \' and \" and : {}";

		ResultOfNaclBox e = await _everClient.Crypto.NaclSecretBox(
			                    new ParamsOfNaclSecretBox { Decrypted = text.ToBase64(), Nonce = nonce, Key = key });
		ResultOfNaclBoxOpen d = await _everClient.Crypto.NaclSecretBoxOpen(
			                        new ParamsOfNaclSecretBoxOpen { Encrypted = e.Encrypted, Nonce = nonce, Key = key });

		d.Decrypted.FromBase64().Should().Be(text);
	}

	[Fact]
	public async Task Factorize() {
		ResultOfFactorize result = await _everClient.Crypto.Factorize(new ParamsOfFactorize {
			Composite = "17ED48941A08F981"
		});

		result.Factors.Should().BeEquivalentTo("494C553B", "53911073");
	}

	[Fact]
	public async Task GenerateRandomBytes() {
		ResultOfGenerateRandomBytes result = await _everClient.Crypto.GenerateRandomBytes(
			                                     new ParamsOfGenerateRandomBytes {
				                                     Length = 32
			                                     });

		result.Bytes.Length.Should().Be(44);
	}

	[Fact]
	public async Task GenerateRandomSignKeys() {
		KeyPair result = await _everClient.Crypto.GenerateRandomSignKeys();

		result.Public.Length.Should().Be(64);
		result.Secret.Length.Should().Be(64);
		result.Secret.Should().NotBeEquivalentTo(result.Public);
	}

	[Fact]
	public async Task HdkeyDeriveFromXprvPath() {
		ResultOfHDKeyDeriveFromXPrvPath result = await _everClient.Crypto.HdkeyDeriveFromXprvPath(
			                                         new ParamsOfHDKeyDeriveFromXPrvPath {
				                                         Xprv =
					                                         "xprv9s21ZrQH143K25JhKqEwvJW7QAiVvkmi4WRenBZanA6kxHKtKAQQKwZG65kCyW5jWJ8NY9e3GkRoistUjjcpHNsGBUv94istDPXvqGNuWpC",
				                                         Path = "m/44'/60'/0'/0'"
			                                         });

		result.Xprv.Should()
		      .Be(
			      "xprvA1KNMo63UcGjmDF1bX39Cw2BXGUwrwMjeD5qvQ3tA3qS3mZQkGtpf4DHq8FDLKAvAjXsYGLHDP2dVzLu9ycta8PXLuSYib2T3vzLf3brVgZ");
	}

	[Fact]
	public async Task HdkeyPublicFromXprv() {
		ResultOfHDKeyPublicFromXPrv result = await _everClient.Crypto.HdkeyPublicFromXprv(
			                                     new ParamsOfHDKeyPublicFromXPrv {
				                                     Xprv =
					                                     "xprv9uZwtSeoKf1swgAkVVCEUmC2at6t7MCJoHnBbn1MWJZyxQ4cySkVXPyNh7zjf9VjsP4vEHDDD2a6R35cHubg4WpzXRzniYiy8aJh1gNnBKv"
			                                     });

		result.Public.Should().Be("b45e1297a5e767341a6eaaac9e20f8ccd7556a0106298316f1272e461b6fbe98");
	}

	[Fact]
	public async Task HdkeyPublicFromXprv2() {
		ResultOfHDKeyPublicFromXPrv result = await _everClient.Crypto.HdkeyPublicFromXprv(
			                                     new ParamsOfHDKeyPublicFromXPrv {
				                                     Xprv =
					                                     "xprvA1KNMo63UcGjmDF1bX39Cw2BXGUwrwMjeD5qvQ3tA3qS3mZQkGtpf4DHq8FDLKAvAjXsYGLHDP2dVzLu9ycta8PXLuSYib2T3vzLf3brVgZ"
			                                     });

		result.Public.Should().Be("302a832bad9e5c9906422a82c28b39ae465dcd60178480f7309e183ee34b5e83");
	}

	[Fact]
	public async Task HdkeySecretFromXprv2() {
		ResultOfHDKeySecretFromXPrv result = await _everClient.Crypto.HdkeySecretFromXprv(
			                                     new ParamsOfHDKeySecretFromXPrv {
				                                     Xprv =
					                                     "xprvA1KNMo63UcGjmDF1bX39Cw2BXGUwrwMjeD5qvQ3tA3qS3mZQkGtpf4DHq8FDLKAvAjXsYGLHDP2dVzLu9ycta8PXLuSYib2T3vzLf3brVgZ"
			                                     });

		result.Secret.Should().Be("1c566ade41169763b155761406d3cef08b29b31cf8014f51be08c0cb4e67c5e1");
	}

	[Fact]
	public async Task MnemonicDeriveSignKeys() {
		KeyPair result = await _everClient.Crypto.MnemonicDeriveSignKeys(new ParamsOfMnemonicDeriveSignKeys {
			Phrase = "foil despair dish fitness start seat hobby hood eight organ want wrong",
			Dictionary = MnemonicDictionary.Ton
		});

		ResultOfConvertPublicKeyToTonSafeFormat anotherResult =
			await _everClient.Crypto.ConvertPublicKeyToTonSafeFormat(
				new ParamsOfConvertPublicKeyToTonSafeFormat {
					PublicKey = result.Public
				});

		anotherResult.TonPublicKey.Should().Be("PuYPI2kinsEy2cc8K42Ro4_tPlL1uFLyKLpCCqkEBFbw1XAH");
	}

	[Fact]
	public async Task MnemonicDeriveSignKeysWithDictWithWord() {
		KeyPair result = await _everClient.Crypto.MnemonicDeriveSignKeys(new ParamsOfMnemonicDeriveSignKeys {
			Phrase =
				"unit follow zone decline glare flower crisp vocal adapt magic much mesh cherry teach mechanic rain float vicious solution assume hedgehog rail sort chuckle",
			Dictionary = 0,
			WordCount = 24
		});

		ResultOfConvertPublicKeyToTonSafeFormat anotherResult =
			await _everClient.Crypto.ConvertPublicKeyToTonSafeFormat(
				new ParamsOfConvertPublicKeyToTonSafeFormat {
					PublicKey = result.Public
				});

		anotherResult.TonPublicKey.Should().Be("PuYTvCuf__YXhp-4jv3TXTHL0iK65ImwxG0RGrYc1sP3H4KS");
	}

	[Fact]
	public async Task MnemonicDeriveSignKeysWithDictWithWordWithPath() {
		KeyPair result = await _everClient.Crypto.MnemonicDeriveSignKeys(new ParamsOfMnemonicDeriveSignKeys {
			Phrase =
				"unit follow zone decline glare flower crisp vocal adapt magic much mesh cherry teach mechanic rain float vicious solution assume hedgehog rail sort chuckle",
			Path = "m",
			Dictionary = 0,
			WordCount = 24
		});

		ResultOfConvertPublicKeyToTonSafeFormat anotherResult =
			await _everClient.Crypto.ConvertPublicKeyToTonSafeFormat(
				new ParamsOfConvertPublicKeyToTonSafeFormat {
					PublicKey = result.Public
				});

		anotherResult.TonPublicKey.Should().Be("PubDdJkMyss2qHywFuVP1vzww0TpsLxnRNnbifTCcu-XEgW0");
	}

	[Fact]
	public async Task MnemonicFromEntropy() {
		ResultOfMnemonicFromEntropy result = await _everClient.Crypto.MnemonicFromEntropy(
			                                     new ParamsOfMnemonicFromEntropy {
				                                     Entropy = "00112233445566778899AABBCCDDEEFF",
				                                     Dictionary = MnemonicDictionary.English,
				                                     WordCount = 12
			                                     });

		result.Phrase.Should()
		      .Be("abandon math mimic master filter design carbon crystal rookie group knife young");
	}

	[Fact]
	public async Task MnemonicVerifyInvalidPhrase() {
		ResultOfMnemonicVerify result = await _everClient.Crypto.MnemonicVerify(new ParamsOfMnemonicVerify {
			Phrase = "one two"
		});

		result.Valid.Should().BeFalse();
	}

	[Fact]
	public async Task MnemonicWords() {
		ResultOfMnemonicWords result = await _everClient.Crypto.MnemonicWords(new ParamsOfMnemonicWords());

		result.Words.Split(" ").Length.Should().Be(2048);
	}

	[Fact]
	public async Task ClientReturnsEnglishMnemonicAsDefault() {
		ResultOfMnemonicFromRandom mnemonicFromRandom = await _everClient.Crypto.MnemonicFromRandom(new ParamsOfMnemonicFromRandom());

		ResultOfMnemonicVerify result = await _everClient.Crypto.MnemonicVerify(new ParamsOfMnemonicVerify { Phrase = mnemonicFromRandom.Phrase, Dictionary = MnemonicDictionary.English });

		result.Valid.Should().BeTrue();
	}

	[Fact]
	public async Task ModularPower() {
		ResultOfModularPower result = await _everClient.Crypto.ModularPower(new ParamsOfModularPower {
			Base = "0123456789ABCDEF",
			Exponent = "0123",
			Modulus = "01234567"
		});

		result.ModularPower.Should().Be("63bfdf");
	}

	[Fact]
	public async Task NaclBox() {
		ResultOfNaclBox result = await _everClient.Crypto.NaclBox(new ParamsOfNaclBox {
			Decrypted = "Test Message".ToBase64(),
			Nonce = "cd7f99924bf422544046e83595dd5803f17536f5c9a11746",
			TheirPublic = "c4e2d9fe6a6baf8d1812b799856ef2a306291be7a7024837ad33a8530db79c6b",
			Secret = "d9b9dc5033fb416134e5d2107fdbacab5aadb297cb82dbdcd137d663bac59f7f"
		});

		result.Encrypted.Should().Be("li4XED4kx/pjQ2qdP0eR2d/K30uN94voNADxwA==");
	}

	[Fact]
	public async Task NaclBoxKeypair() {
		KeyPair result = await _everClient.Crypto.NaclBoxKeypair();

		result.Public.Length.Should().Be(64);
		result.Secret.Length.Should().Be(64);
		result.Public.Should().NotBe(result.Secret);
	}

	[Fact]
	public async Task NaclBoxKeypairFromSecretKey() {
		KeyPair result = await _everClient.Crypto.NaclBoxKeypairFromSecretKey(new ParamsOfNaclBoxKeyPairFromSecret {
			Secret = "e207b5966fb2c5be1b71ed94ea813202706ab84253bdf4dc55232f82a1caf0d4"
		});
		result.Public.Should().Be("a53b003d3ffc1e159355cb37332d67fc235a7feb6381e36c803274074dc3933a");
	}

	[Fact]
	public async Task NaclBoxOpen() {
		ResultOfNaclBoxOpen result = await _everClient.Crypto.NaclBoxOpen(new ParamsOfNaclBoxOpen {
			Encrypted = "962e17103e24c7fa63436a9d3f4791d9dfcadf4b8df78be83400f1c0".HexToBase64(),
			Nonce = "cd7f99924bf422544046e83595dd5803f17536f5c9a11746",
			TheirPublic = "c4e2d9fe6a6baf8d1812b799856ef2a306291be7a7024837ad33a8530db79c6b",
			Secret = "d9b9dc5033fb416134e5d2107fdbacab5aadb297cb82dbdcd137d663bac59f7f"
		});

		result.Decrypted.FromBase64().Should().Be("Test Message");
	}

	[Fact]
	public async Task NaclSecretBox() {
		ResultOfNaclBox result = await _everClient.Crypto.NaclSecretBox(new ParamsOfNaclSecretBox {
			Decrypted = "Test Message".ToBase64(),
			Nonce = "2a33564717595ebe53d91a785b9e068aba625c8453a76e45",
			Key = "8f68445b4e78c000fe4d6b7fc826879c1e63e3118379219a754ae66327764bd8"
		});

		result.Encrypted.Should().Be("JL7ejKWe2KXmrsns41yfXoQF0t/C1Q8RGyzQ2A==");
	}

	[Fact]
	public async Task NaclSecretBoxOpen() {
		ResultOfNaclBoxOpen result = await _everClient.Crypto.NaclSecretBoxOpen(new ParamsOfNaclSecretBoxOpen {
			Encrypted = "24bede8ca59ed8a5e6aec9ece35c9f5e8405d2dfc2d50f111b2cd0d8".HexToBase64(),
			Nonce = "2a33564717595ebe53d91a785b9e068aba625c8453a76e45",
			Key = "8f68445b4e78c000fe4d6b7fc826879c1e63e3118379219a754ae66327764bd8"
		});

		result.Decrypted.FromBase64().Should().Be("Test Message");
	}

	[Fact]
	public async Task NaclSign() {
		ResultOfNaclSign result = await _everClient.Crypto.NaclSign(new ParamsOfNaclSign {
			Unsigned = "Test Message".ToBase64(),
			Secret =
				"56b6a77093d6fdf14e593f36275d872d75de5b341942376b2a08759f3cbae78f1869b7ef29d58026217e9cf163cbfbd0de889bdf1bf4daebf5433a312f5b8d6e"
		});

		result.Signed.Should()
		      .Be(
			      "+wz+QO6l1slgZS5s65BNqKcu4vz24FCJz4NSAxef9lu0jFfs8x3PzSZRC+pn5k8+aJi3xYMA3BQzglQmjK3hA1Rlc3QgTWVzc2FnZQ==");
	}

	[Fact]
	public async Task NaclSignDetached() {
		ResultOfNaclSignDetached result = await _everClient.Crypto.NaclSignDetached(new ParamsOfNaclSign {
			Unsigned = "Test Message".ToBase64(),
			Secret =
				"56b6a77093d6fdf14e593f36275d872d75de5b341942376b2a08759f3cbae78f1869b7ef29d58026217e9cf163cbfbd0de889bdf1bf4daebf5433a312f5b8d6e"
		});

		result.Signature.Should()
		      .Be(
			      "fb0cfe40eea5d6c960652e6ceb904da8a72ee2fcf6e05089cf835203179ff65bb48c57ecf31dcfcd26510bea67e64f3e6898b7c58300dc14338254268cade103");
	}

	[Fact]
	public async Task NaclSignKeypairFromSecretKey() {
		KeyPair result = await _everClient.Crypto.NaclSignKeypairFromSecretKey(new ParamsOfNaclSignKeyPairFromSecret {
			Secret = "8fb4f2d256e57138fb310b0a6dac5bbc4bee09eb4821223a720e5b8e1f3dd674"
		});

		result.Public.Should().Be("aa5533618573860a7e1bf19f34bd292871710ed5b2eafa0dcdbb33405f2231c6");
	}

	[Fact]
	public async Task NaclSignOpen() {
		ResultOfNaclSignOpen result = await _everClient.Crypto.NaclSignOpen(new ParamsOfNaclSignOpen {
			Signed =
				"fb0cfe40eea5d6c960652e6ceb904da8a72ee2fcf6e05089cf835203179ff65bb48c57ecf31dcfcd26510bea67e64f3e6898b7c58300dc14338254268cade10354657374204d657373616765"
					.HexToBase64(),
			Public = "1869b7ef29d58026217e9cf163cbfbd0de889bdf1bf4daebf5433a312f5b8d6e"
		});

		result.Unsigned.FromBase64().Should().Be("Test Message");
	}

	[Fact]
	public async Task Scrypt() {
		ResultOfScrypt result = await _everClient.Crypto.Scrypt(new ParamsOfScrypt {
			Password = "Test Password".ToBase64(),
			Salt = "Test Salt".ToBase64(),
			LogN = 10,
			R = 8,
			P = 16,
			DkLen = 64
		});

		result.Key.Should()
		      .Be(
			      "52e7fcf91356eca55fc5d52f16f5d777e3521f54e3c570c9bbb7df58fc15add73994e5db42be368de7ebed93c9d4f21f9be7cc453358d734b04a057d0ed3626d");
	}

	[Fact]
	public async Task Sha256Encoded() {
		ResultOfHash result = await _everClient.Crypto.Sha256(new ParamsOfHash {
			Data = "Message to hash with sha 256".ToBase64()
		});

		result.Hash.Should()
		      .Be("16fd057308dd358d5a9b3ba2de766b2dfd5e308478fc1f7ba5988db2493852f5");
	}

	[Fact]
	public async Task Sha256Hex() {
		ResultOfHash result = await _everClient.Crypto.Sha256(new ParamsOfHash {
			Data = "4d65737361676520746f206861736820776974682073686120323536".HexToBase64()
		});

		result.Hash.Should()
		      .Be("16fd057308dd358d5a9b3ba2de766b2dfd5e308478fc1f7ba5988db2493852f5");
	}

	[Fact]
	public async Task Sha256Raw() {
		ResultOfHash result = await _everClient.Crypto.Sha256(new ParamsOfHash {
			Data = "TWVzc2FnZSB0byBoYXNoIHdpdGggc2hhIDI1Ng=="
		});

		result.Hash.Should()
		      .Be("16fd057308dd358d5a9b3ba2de766b2dfd5e308478fc1f7ba5988db2493852f5");
	}

	[Fact]
	public async Task Sha512() {
		ResultOfHash result = await _everClient.Crypto.Sha512(new ParamsOfHash {
			Data = "Message to hash with sha 512".ToBase64()
		});

		result.Hash.Should()
		      .Be(
			      "2616a44e0da827f0244e93c2b0b914223737a6129bc938b8edf2780ac9482960baa9b7c7cdb11457c1cebd5ae77e295ed94577f32d4c963dc35482991442daa5");
	}

	[Fact]
	public async Task Sign() {
		ResultOfSign result = await _everClient.Crypto.Sign(new ParamsOfSign {
			Unsigned = "Test Message".ToBase64(),
			Keys = new KeyPair {
				Public = "1869b7ef29d58026217e9cf163cbfbd0de889bdf1bf4daebf5433a312f5b8d6e",
				Secret = "56b6a77093d6fdf14e593f36275d872d75de5b341942376b2a08759f3cbae78f"
			}
		});

		result.Signed.Should()
		      .Be(
			      "+wz+QO6l1slgZS5s65BNqKcu4vz24FCJz4NSAxef9lu0jFfs8x3PzSZRC+pn5k8+aJi3xYMA3BQzglQmjK3hA1Rlc3QgTWVzc2FnZQ==");
		result.Signature.Should()
		      .Be(
			      "fb0cfe40eea5d6c960652e6ceb904da8a72ee2fcf6e05089cf835203179ff65bb48c57ecf31dcfcd26510bea67e64f3e6898b7c58300dc14338254268cade103");
	}

	[Fact]
	public async Task TestSigningBox() {
		KeyPair keys = await _everClient.Crypto.GenerateRandomSignKeys();
		RegisteredSigningBox registeredSigningBox = await _everClient.Crypto.GetSigningBox(keys);
		uint keyBoxHandle = registeredSigningBox.Handle;

		async Task Callback(JsonElement request, uint _) {
			var paramsOfAppRequest = request.ToObject<ParamsOfAppRequest>();

			switch (PolymorphicSerializer.Deserialize<ParamsOfAppSigningBox>(paramsOfAppRequest.RequestData!.Value)) {
				case ParamsOfAppSigningBox.GetPublicKey: {
					ResultOfSigningBoxGetPublicKey resultOfSigningBoxGetPublicKey = await _everClient.Crypto.SigningBoxGetPublicKey(new RegisteredSigningBox { Handle = keyBoxHandle });
					await _everClient.Client.ResolveAppRequest(new ParamsOfResolveAppRequest {
						AppRequestId = paramsOfAppRequest.AppRequestId,
						Result = new AppRequestResult.Ok {
							Result = new ResultOfAppSigningBox.GetPublicKey {
								PublicKey = resultOfSigningBoxGetPublicKey.Pubkey
							}.ToJsonElement()
						}
					});
					break;
				}
				case ParamsOfAppSigningBox.Sign sign: {
					ResultOfSigningBoxSign resultOfSigningBoxSign = await _everClient.Crypto.SigningBoxSign(new ParamsOfSigningBoxSign { SigningBox = keyBoxHandle, Unsigned = sign.Unsigned });
					await _everClient.Client.ResolveAppRequest(new ParamsOfResolveAppRequest {
						AppRequestId = paramsOfAppRequest.AppRequestId,
						Result = new AppRequestResult.Ok {
							Result = new ResultOfAppSigningBox.Sign {
								Signature = resultOfSigningBoxSign.Signature
							}.ToJsonElement()
						}
					});
					break;
				}
			}
		}

		// act
		RegisteredSigningBox externalBox = await _everClient.Crypto.RegisterSigningBox(Callback);
		ResultOfSigningBoxGetPublicKey boxPubkey =
			await _everClient.Crypto.SigningBoxGetPublicKey(new RegisteredSigningBox
				                                                { Handle = externalBox.Handle });

		// assert
		boxPubkey.Pubkey.Should().Be(keys.Public);

		// arrange
		string unsigned = "Test Message".ToBase64();

		// act
		ResultOfSigningBoxSign boxSign = await _everClient.Crypto.SigningBoxSign(new ParamsOfSigningBoxSign {
			SigningBox = externalBox.Handle,
			Unsigned = unsigned
		});
		ResultOfSign keysSign = await _everClient.Crypto.Sign(new ParamsOfSign {
			Keys = keys,
			Unsigned = unsigned
		});

		// assert
		boxSign.Signature.Should().Be(keysSign.Signature);

		// remove boxes 
		await _everClient.Crypto.RemoveSigningBox(new RegisteredSigningBox {
			Handle = externalBox.Handle
		});
		await _everClient.Crypto.RemoveSigningBox(new RegisteredSigningBox {
			Handle = keyBoxHandle
		});
	}

	[Fact]
	public async Task TonCrc16() {
		ResultOfTonCrc16 result = await _everClient.Crypto.TonCrc16(new ParamsOfTonCrc16 {
			Data = "0123456789abcdef".HexToBase64()
		});

		result.Crc.Should().Be(43349);
	}

	[Fact]
	public async Task VerifySignature() {
		ResultOfVerifySignature verified = await _everClient.Crypto.VerifySignature(new ParamsOfVerifySignature {
			Public = "1869b7ef29d58026217e9cf163cbfbd0de889bdf1bf4daebf5433a312f5b8d6e",
			Signed =
				"+wz+QO6l1slgZS5s65BNqKcu4vz24FCJz4NSAxef9lu0jFfs8x3PzSZRC+pn5k8+aJi3xYMA3BQzglQmjK3hA1Rlc3QgTWVzc2FnZQ=="
		});

		verified.Unsigned.FromBase64().Should().Be("Test Message");
	}

	private class MnemonicFromRandomData : IEnumerable<object[]> {
		// ReSharper disable once RedundantEmptyObjectCreationArgumentList
		private readonly List<object[]> _data = GetData();

		public IEnumerator<object[]> GetEnumerator() {
			return _data.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		private static List<object[]> GetData() {
			MnemonicDictionary?[] dict = Enum.GetValues(typeof(MnemonicDictionary)).Cast<MnemonicDictionary?>().Append(null).ToArray();
			byte?[] words = { null, 12, 15, 18, 21, 24 };

			return (from d in dict
			        from w in words
			        select new object[] {
				        d, w
			        }).ToList();
		}
	}
}
