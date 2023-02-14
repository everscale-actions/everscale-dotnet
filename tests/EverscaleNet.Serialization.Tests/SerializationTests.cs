using EverscaleNet.TestsShared;
using FluentAssertions;
using Xunit;

namespace EverscaleNet.Serialization.Tests;

public class SerializationTests {
	[Fact]
	public void BigIntegerConverting() {
		var objectWithBigInteger = new { bigIng = 1_231_231_231_231_123_123m };

		string json = JsonSerializer.Serialize(objectWithBigInteger, JsonOptionsProvider.JsonSerializerOptions);

		json.Should().Be("{\"bigIng\":1231231231231123123}");
	}

	[Fact]
	public void DoubleNoneField() {
		var signerNone = new Signer.None();
		var accountForExecutorNone = new AccountForExecutor.None();

		string signerNoneJson = JsonSerializer.Serialize(signerNone, JsonOptionsProvider.JsonSerializerOptions);
		string accountForExecutorNoneJson = JsonSerializer.Serialize(accountForExecutorNone, JsonOptionsProvider.JsonSerializerOptions);

		signerNoneJson.Should().Be(accountForExecutorNoneJson);
	}

	[Fact]
	public void ResultOfRunGetSerialization() {
		var str =
			@"{""output"":[[[""0x0101b6d65a384b9c70deb49fd6c43ffc0f60ed22fcc3a4966f7043794a749228"",""60138000000000""],[[""0x03de5d8590fe6ad191bf94d4136dfb630e9b3447bb2f1a6ae2d8e3e4cbee1d9f"",""61000000000000""],[[""0x0558f90c0682d677b46005ce2e04206c255ea9a05bfac0ff5aea9d7182a28913"",""60138000000000""],[[""0x07698228973a595751d79e1fafd5a4145b3d35349bf0b43322afb61b138f01eb"",""60138000000000""],[[""0x09d1ef8a40a9fbf1ca505f072258048ec15e0637baa085649d77b9a90220003e"",""60138000000000""],[[""0x0ac21ef27c8ed4487270f1c45e99dac091ca4007951217ece344452df7047e5a"",""60138000000000""],[[""0x0aed529418ab67a31a4b98c224f8fdb2fec11f0100c23751e67b312cba11fb23"",""60138000000000""],[[""0x0bd14cdade9067c523f44fd208dee5daa7d852151725d713b92c840a031018a6"",""60138000000000""],[[""0x0bddff0d98f42a3155e577a5579623a911e3b03401835166553f88375cbd9657"",""60138000000000""],[[""0x12893bbd649bf2e1e79cb084025638cdd7906eebca40efeee7fdbd548cc96391"",""60138000000000""],[[""0x15546bc7b5124f6d83d6c5a62b8890a48b933168e141c01229431a6c0c499780"",""60138000000000""],[[""0x1cbea6a399ba200958db255579cda2195006f3a3108b2d6ef7e258e42c101479"",""60138000000000""],[[""0x1f8ee6ba2902715804c769c3845b6b3a37802e462e8df63ba19f827a92dbbda0"",""60138000000000""],[[""0x1fdd556d84d1d9f24a739c2600ec72256cc00920d85ad3a2edb3e0d72146789d"",""60138000000000""],[[""0x20f20c2cfa4d72afb9c5f64d4735070962b3323b3629892c75f56427f175ebe4"",""60138000000000""],[[""0x219d32737b0f3769869b8fa750ba8e3cd9f19b21a4d669c7c79d420d7f7cdb24"",""60138000000000""],[[""0x2615b4aeb69140531228248a9d84593117b64e22d462e3968e39c1840a260523"",""60138000000000""],[[""0x26984c9f04bc1889061e98bd9caf6955f750219d8e8dbc0986feb9d770e5a15b"",""60138000000000""],[[""0x28bb07d80e20aa624ae47dfe53f915b23a666bf825ff283bac06c14bea1eaa74"",""60138000000000""],[[""0x2a23566008fd4f87105b09d02c739452e45187fbada5e7e52ada356264cd6751"",""60138000000000""],[[""0x2dcc70859876106b21b598ba9a10c9932259c36f44adeb95a178e67f6afd2f7a"",""60138000000000""],[[""0x30b854226ef943d738d2dfcc72ede3b39d08604ca7211abf3c76f488441c77fc"",""60138000000000""],[[""0x31bb74a5a53769d3db789d961375ea569d4da0bd6ac2b12f830dd6be81968ef1"",""60138000000000""],[[""0x334f22e0de2e24a070fec7c1d77d7a988a79d66b79e2d654310a963964edd337"",""60138000000000""],[[""0x36c44eddf773390cdb42f93f8454ea9c7ca45aa8948346df8f642a59ce44c442"",""60138000000000""],[[""0x3d29d2b5ceef46703255ce8cfe3aad3c4fefd3a2025e5a48ce78a63f20887eff"",""60138000000000""],[[""0x41b047a20ed691e9376f7f2f60d6571290e34ef4e1b85467dcc3d7c0cf7fae90"",""60138000000000""],[[""0x41e7541c377b58a0cfc4ca954731e971f6dc9fa6806eaa1709d011d3d32593ce"",""60138000000000""],[[""0x426a52d3b3d016451c46b3a0eacb382fbbb38739e00d041d4038f795a54e25b5"",""60138000000000""],[[""0x42f89915ca540af691f623f201b616caa7f5e104f8293698f8b46c4e7bb5b292"",""60138000000000""],[[""0x4449521e793b02b036ce698c3af951e9548cd5b862b704fa5cc9e80b171a3c61"",""60138000000000""],[[""0x492f4fee6a035a09e9ff09d65a65768899b04797cff08dc2c64ae11cd94d1968"",""60138000000000""],[[""0x4947018f9c0c9302b2783eb5edacb76ccae3b5c5a2f6355b5b51afd1a18075f8"",""60138000000000""],[[""0x4c27708e4ce81a0bbeb315ece024ba495f3e3fab5f83a2941b7731a58ad32160"",""60138000000000""],[[""0x5059d40f80f578c3c384239415f54af35ab4dbdea0251618d4c3c7b4937e7e69"",""60138000000000""],[[""0x5191f8cbfe1ce25a68c337ede75638321374112b868584092a335f83caad59a4"",""60138000000000""],[[""0x531296c32ea64d09dcb44ff0b99843dc9855143c70b9fad42deb33881525fb84"",""60138000000000""],[[""0x54c9860aa34ddba2a16e4f4271e1771f61f1e8a7a116fbbfa62f0e535b95559e"",""60138000000000""],[[""0x54ce2d6b35d0d670e37fcd533ff17c2116e0acead719194e46d478944b33108e"",""60138000000000""],[[""0x576af5e4af963a0caa957629d009906119e418e7f7778f5a55d41c0905b73a4f"",""60138000000000""],[[""0x59905476f4781f6a79359079bfa3fe295c65d6b918afadbc352edcdb558ad094"",""60138000000000""],[[""0x5a4e95cdf94bed240ebabded084b70b2548601686d94a751f240aedd2032e4f2"",""60138000000000""],[[""0x5a7500f11becf6741fe5624d2298f6b830ead261871a48a81f80bf9be09ed866"",""180000000000000""],[[""0x5c26942bf33c49485db3b2693e5d582708b44705f712c4e24af1ee84744c079e"",""60138000000000""],[[""0x5fcdcc107e81ef4399c9d603a25fcba75cb78f1fa1bafd3acb39e3521d7fc9ce"",""60138000000000""],[[""0x6107f5b2974fabf6f0aa1a7898340b3f76c4ba272b95a3e4bb809c1d529b6997"",""60138000000000""],[[""0x647b9a476f733ec5ee9cbc0bfb021335cd3166b9aaa8ad27ed0f88d9f6bf9dbe"",""60138000000000""],[[""0x658c461d8dad54a5a9cbbbb2711920a541ba58003e7029cc228dfdfbc17ede3f"",""60138000000000""],[[""0x661336351b889e0124fbc19f9f35f6a0f6e8c4fa9b89e9ef527718bd6aa254be"",""60138000000000""],[[""0x6852746bbfb41e556daae99d375b2839ad62b35355c3b9fbbd54b4946ae2050f"",""60138000000000""],[[""0x68ad3d98642913848b605dbad3f1df971f21908d360c37af4a493e9b4646b45e"",""60138000000000""],[[""0x6c07c6be93940a83b30514b21531fd3dd204bb89e7f77b5a2421a41d4e85c74d"",""60138000000000""],[[""0x6d4ad504054f292b7f66c7ed32f3b123bfa5c7be9c45faf26d77ad85efa64a38"",""60138000000000""],[[""0x6e77d45d07651565be5cdb11b80c91fa18def0a434f246c0b25bb50fc4877dd4"",""60138000000000""],[[""0x738600c570c19ef1b91bf2cd83709d71899c246bfabb5b08dc70fe32b5c81f7d"",""60138000000000""],[[""0x7469b663b9fa7be185aa1819bfb48a4eda6e4d8af33e1955d95fa5e156d50f12"",""60138000000000""],[[""0x77410e09363239b0999198a701e37f75775cc55049ba541497967f5d8ba74ef4"",""60138000000000""],[[""0x781c96175cf45b791142326964347095fd0fbdd3c8579c42cea108798e025152"",""60138000000000""],[[""0x79b43e9c18241636ae7c554097bb4bc5da03249bee67abe5366a5b093b708cab"",""60138000000000""],[[""0x7ad807b91790868497768476e8c8e6b53ff9b1a91fbfe6a7edae8de0307a8157"",""60138000000000""],[[""0x8308ff2b214d509d3781d7361a7ccb5f4fb976f8e386ce3c9082bcad8805d13d"",""60138000000000""],[[""0x845a0fff44669c941475eb3f3ffd6e065ee94cfbfdcb820877744d6f9647a5d0"",""49899000000000""],[[""0x88700f083f3bc7971c348de8357ee36b2551d8cdb7ea4b4e4e8aae558d67a231"",""60138000000000""],[[""0x8b08c457cac18642f49ab7de0ef7551b93e11dbc2979062f22b271b890e8d2f0"",""60138000000000""],[[""0x8bc840e0c5a98e608e70307ada41aa94a745a51f6065111942021a4e601dc328"",""60138000000000""],[[""0x93e518529faa2244ee1bdc24a5459d4b3d2047f8756b12636e2cba3b766ec201"",""60138000000000""],[[""0x993d90fac526bdad11549104105452e9198da8d485dbb4af17b044a721fa8b82"",""60138000000000""],[[""0x9997880b1dcc011ce4fefeb587eca16c027c81aafeb4305d3a1755182c269b5e"",""60138000000000""],[[""0x9d998de650f13c85da4ab08de0fa7960771d4269081fa1ed1f9940c5cd8bb57c"",""60138000000000""],[[""0x9fab138505d28c3c2d68509c5414abe933ab7de90610d8cc84edaf380e739f48"",""60138000000000""],[[""0x9fd585f4d71c50ff54b69255ddbaa4a30eae31cde2d02ba6d4c0f87faf288f9a"",""60138000000000""],[[""0xa42d598e3d6c051880488bfd139705c9853ff2e93046c6e096eba5f5b8dd714e"",""60138000000000""],[[""0xa6e3ff7b1f340f7d02a1b64ade185c9039cd2751ef47ac5d7950b527b377d566"",""60138000000000""],[[""0xa79d52472a9343b4f91c61b7e065cd736844064c11188fb86fef32447b163462"",""60138000000000""],[[""0xa82bdb918a99f7192b0ebe745f04217991d2077dc43ffe75956782f55c7c9805"",""60138000000000""],[[""0xa87f60cfad2f10ec420d4660d98a43a1105a867aac63a2724075f155b991fd35"",""60138000000000""],[[""0xad1e503c43f7f62bb672b234eb1510b8ccef4d23b6de1f53a8a0d738c961cee2"",""60138000000000""],[[""0xad8dd15447ac5c3b0ac9ed9ebae3b32cfe3cda5442bfce7843443a353701eb34"",""60138000000000""],[[""0xade82619842d2257fb19097c990b77818f2352e3809b9c179b3b66989b8e01c3"",""60138000000000""],[[""0xb739a017b3b9c9577eeba0d3b94fe2027333ccdad378f0aa67b441eb8cbd675a"",""60138000000000""],[[""0xb85ed5c5a48abadc5bf4a85185f781aa60eebe7ef20642f660c7e90d481984cf"",""60138000000000""],[[""0xbcb7406f71b46a5171b822f609d50df1a485bbae832f76db0a356b243616ca8e"",""60138000000000""],[[""0xbedc1da66f906866cf8af7e57cda645018c39d1b028e9ed4682643941c940348"",""60138000000000""],[[""0xc2660177ec158c05676b396baab45f8f8a63f74a0eaf1a7cfe011c7eea0cd8a4"",""57180000000000""],[[""0xc536058376e87f6481ce31b0e088235b4be9df00145c97081c45a28cce64c684"",""60138000000000""],[[""0xc8fd550fcf32a9ea6aef295e788e4394e744f0939ab5fa8b9009577e274a477e"",""60138000000000""],[[""0xcbfe056a9e9fafd246a8fa3025c3d870dac6b01f68adc847f03277eac906452f"",""60138000000000""],[[""0xd023735d89cb9d29c5301b87f00d3f7a42aa6f0320086473e50b7e3b8b9acb12"",""60138000000000""],[[""0xd17002d5872d62876fc4cae771c472ececae0b50820d760718a753acf431f31c"",""60138000000000""],[[""0xd847bac558e925bd87b15a9e8c077df36537e6fc52d5c2019004c0c570fd0266"",""60138000000000""],[[""0xdab17536c875995ce144f17771c79e7e9d6adcaaa66cce64947c8d17a363a2dd"",""60138000000000""],[[""0xdf0b5c031ece9dadfd23c63a41e4e7f1ae4138b157ff7588c21083853d585789"",""60138000000000""],[[""0xe087dae3faaf4748c8bcd237ca7ece5f8bcddf6b60db216252c5e29a7f6a33a4"",""60138000000000""],[[""0xe09755d90d62160409b67ff28584f2800087f9063d76b3240c4d3f94f0880c41"",""60138000000000""],[[""0xe23c47f9c2e9d2d87d9f1fcc0352ef28d13f322f8003d4210bb33692e77fd988"",""60138000000000""],[[""0xeb2269b0ea934046a59399bd824f6a7fae4c7d696bb163e1bd235cbc21aa2b55"",""60138000000000""],[[""0xedf8d203bebac7d629840ca0a704cbff92607d6bf538bc99ab65fada6a7b3c65"",""60138000000000""],[[""0xf179ca30b1a9c8c33e33863b04d8d0078dddbf974a1e8666d072e0403c997f21"",""60138000000000""],[[""0xf199c75a842c96c459272502b7010a78f29e7d00ad649f7756dd11c8d321a97c"",""60138000000000""],[[""0xf19a880d58384bfaf5c839e7b6502ff7e6dc11cc38a77f651c948ea8475a37f7"",""60138000000000""],[[""0xf25841168bb223f03cc01f5934474e56ae3ac0307a048ab167326c7d655c25db"",""86829000000000""],[[""0xf25e305cc44404dae89a8f3b577cf94c367ab28a8ebc81a5c551d39303e254c2"",""60138000000000""],[[""0xf3c0eed928d059ec9c99fd55ef4df9ccdaa30626e14b833f064936099b8088e1"",""60138000000000""],[[""0xf6fe4424c9df211b1d2e92f7a91889aac643c605de458fa8f2af90534b885654"",""154754000000000""],[[""0xf8a26375e76f1f8ff763787507fe2e01ed257b1a6b1772e48338606862a80da4"",""60138000000000""],[[""0xfb471071aa87f25465da8c98bdeb1b24165b4a1694c5a6ab9f59eb57ce9e451d"",""60138000000000""],[[""0xfb66f351a3b27e1702a21bfd189f3db5053f9f0089b26e7f05218fa87b925e2e"",""60138000000000""],[[""0xfbcd15956e466a3c945c8bee6e6bc6bdab6b1b2ec0c07a3ba431091795751bef"",""60138000000000""],[[""0xfced4379f1cb13157b34d50301a65ab47dc3452f4cd0e2a2d8e0b33a07350f43"",""60138000000000""],null]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]}";

		var res = JsonSerializer.Deserialize<ResultOfRunGet>(str, JsonOptionsProvider.JsonSerializerOptions);

		res.Should().NotBeNull();
	}

	[Fact]
	public void SerializeAbi() {
		string json = JsonSerializer.Serialize(TestsEnv.Packages.Events.Abi, JsonOptionsProvider.JsonSerializerOptions);

		json.Should().NotBeNull();
	}

	[Fact]
	public void SerializeEncodeMessageRequestTest() {
		var messageRequest = new ParamsOfEncodeMessage {
			Abi = TestsEnv.Packages.Events.Abi,
			DeploySet = new DeploySet {
				Tvc = TestsEnv.Packages.Events.Tvc
			},
			CallSet = new CallSet {
				FunctionName = "constructor",
				Header = new FunctionHeader {
					Time = 123123,
					Expire = 123123
				}
			},
			Signer = new Signer.External {
				PublicKey = "4c7c408ff1ddebb8d6405ee979c716a14fdd6cc08124107a61d3c25597099499"
			}
		};

		var jsonElement = messageRequest.ToJsonElement();

		jsonElement.Should().NotBeNull();

		jsonElement.TryGetProperty("abi", out JsonElement abi).Should().BeTrue();
		abi.TryGetProperty("type", out JsonElement apiType).Should().BeTrue();
		apiType.GetString().Should().Be("Contract");

		jsonElement.TryGetProperty("signer", out JsonElement signer).Should().BeTrue();
		signer.TryGetProperty("type", out JsonElement signerType).Should().BeTrue();
		signerType.GetString().Should().Be("External");
		signer.Get<string>("public_key").Should().Be("4c7c408ff1ddebb8d6405ee979c716a14fdd6cc08124107a61d3c25597099499");
	}

	[Fact]
	public void TypeWithNullableProperties() {
		var @params = new ParamsOfMnemonicFromRandom();

		string json = JsonSerializer.Serialize(@params, JsonOptionsProvider.JsonSerializerOptions);

		json.Should().Be("{}");
	}

	[Fact]
	public void EnumIntValueType() {
		var @params = new ParamsOfMnemonicFromRandom { Dictionary = 1 };

		string json = JsonSerializer.Serialize(@params, JsonOptionsProvider.JsonSerializerOptions);

		json.Should().Be("{\"dictionary\":1}");
	}

	[Fact]
	public void EnumStringValueType() {
		var @params = new NetworkConfig { QueriesProtocol = NetworkQueriesProtocol.WS };

		string json = JsonSerializer.Serialize(@params, JsonOptionsProvider.JsonSerializerOptions);

		json.Should().Be("{\"queries_protocol\":\"WS\"}");
	}

	[Theory]
	[ClassData(typeof(AccountTypeTestData))]
	public void AccountTypeGetPropertyTest(string json, AccountType? expectedAccountType) {
		AccountType? accountType = JsonDocument.Parse(json).RootElement.TryGet("acc_type", out AccountType result) ? result : null;

		accountType.Should().Be(expectedAccountType);
	}

	[Theory]
	[ClassData(typeof(AccountTypeTestData))]
	public void AccountTypeAnonymousTypeTest(string json, AccountType? expectedAccountType) {
		var prototype = new { acc_type = default(AccountType?) };

		var result = JsonSerializerExtensions.DeserializePrototype(json, prototype);

		result.acc_type.Should().Be(expectedAccountType);
	}

	[Fact]
	public void AccountTypeDeserializationKeyNotFoundExceptionTest() {
		var getAccountType = new Func<AccountType>(() => JsonDocument.Parse("{}").RootElement.Get<AccountType>("acc_type"));

		getAccountType.Should().ThrowExactly<KeyNotFoundException>("acc_type is not presented");
	}

	[Fact]
	public void NestedPolymorphicTypeContainsTypeFieldTest() {
		var appRequestResult = new AppRequestResult.Ok {
			Result = new ResultOfAppSigningBox.GetPublicKey {
				PublicKey = "Pubkey"
			}.ToJsonElement()
		};

		string json = JsonSerializer.Serialize<AppRequestResult>(appRequestResult, JsonOptionsProvider.JsonSerializerOptions);

		json.Should().Be("{\"type\":\"Ok\",\"result\":{\"type\":\"GetPublicKey\",\"public_key\":\"Pubkey\"}}");
	}

	private class AccountTypeTestData : TheoryData<string, AccountType?> {
		public AccountTypeTestData() {
			// Add("{ }", null); // be careful JsonSerializer returns 0 value if not nullable enum type provided
			Add("{ \"acc_type\": 0 }", AccountType.Uninit);
			Add("{ \"acc_type\": 1 }", AccountType.Active);
			Add("{ \"acc_type\": 2 }", AccountType.Frozen);
			Add("{ \"acc_type\": 3 }", AccountType.NonExist);
			Add("{ \"acc_type\": 4 }", (AccountType)4);
		}
	}
}
