using System.Collections;
using EverscaleNet.Client.Tests.Utils;
using EverscaleNet.TestsShared;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace EverscaleNet.Client.Tests.Modules;

public class AbiModuleTests : IClassFixture<EverClientTestsFixture> {
	public AbiModuleTests(EverClientTestsFixture fixture, ITestOutputHelper outputHelper) {
		_everClient = fixture.CreateClient(outputHelper);
	}

	private const uint Expire = 1599458404;
	private const ulong Time = 1599458364291;

	private static readonly KeyPair Keys = new() {
		Public = "4c7c408ff1ddebb8d6405ee979c716a14fdd6cc08124107a61d3c25597099499",
		Secret = "cc8929d635719612a9478b9cd17675a39cfad52d8959e8a177389b8c0b9122a7"
	};

	private static readonly JsonElement ZeroIdElement = JsonDocument.Parse("{ \"id\": 0 }").RootElement;
	private readonly IEverClient _everClient;

	[Theory]
	[ClassData(typeof(EncodeMessageTestsData))]
	public async Task EncodeMessageTests(ParamsOfEncodeMessage @params, string expectedMessage, string expectedDataToSign) {
		ResultOfEncodeMessage actual = await _everClient.Abi.EncodeMessage(@params);

		actual.Message.Should().Be(expectedMessage);
		actual.DataToSign.Should().Be(expectedDataToSign);
	}

	private class EncodeMessageTestsData : IEnumerable<object[]> {
		// ReSharper disable once RedundantEmptyObjectCreationArgumentList
		private readonly List<object[]> _data = new() {
			new object[] {
				new ParamsOfEncodeMessage {
					Abi = TestsEnv.Packages.Events.Abi,
					DeploySet = new DeploySet { Tvc = TestsEnv.Packages.Events.Tvc },
					CallSet = new CallSet {
						FunctionName = "constructor",
						Header = new FunctionHeader { Time = Time, Expire = Expire }
					},
					Signer = new Signer.External {
						PublicKey = "4c7c408ff1ddebb8d6405ee979c716a14fdd6cc08124107a61d3c25597099499"
					}
				},
				"te6ccgECFwEAA2gAAqeIAAt9aqvShfTon7Lei1PVOhUEkEEZQkhDKPgNyzeTL6YSEZTHxAj/Hd67jWQF7peccWoU/dbMCBJBB6YdPCVZcJlJkAAAF0ZyXLg19VzGRotV8/gGAQEBwAICA88gBQMBAd4EAAPQIABB2mPiBH+O713GsgL3S844tQp+62YECSCD0w6eEqy4TKTMAib/APSkICLAAZL0oOGK7VNYMPShCQcBCvSkIPShCAAAAgEgDAoByP9/Ie1E0CDXScIBjhDT/9M/0wDRf/hh+Gb4Y/hijhj0BXABgED0DvK91wv/+GJw+GNw+GZ/+GHi0wABjh2BAgDXGCD5AQHTAAGU0/8DAZMC+ELiIPhl+RDyqJXTAAHyeuLTPwELAGqOHvhDIbkgnzAg+COBA+iogggbd0Cgud6S+GPggDTyNNjTHwH4I7zyudMfAfAB+EdukvI83gIBIBINAgEgDw4AvbqLVfP/hBbo417UTQINdJwgGOENP/0z/TANF/+GH4Zvhj+GKOGPQFcAGAQPQO8r3XC//4YnD4Y3D4Zn/4YeLe+Ebyc3H4ZtH4APhCyMv/+EPPCz/4Rs8LAMntVH/4Z4AgEgERAA5biABrW/CC3Rwn2omhp/+mf6YBov/ww/DN8Mfwxb30gyupo6H0gb+j8IpA3SRg4b3whXXlwMnwAZGT9ghBkZ8KEZ0aCBAfQAAAAAAAAAAAAAAAAACBni2TAgEB9gBh8IWRl//wh54Wf/CNnhYBk9qo//DPAAxbmTwqLfCC3Rwn2omhp/+mf6YBov/ww/DN8Mfwxb2uG/8rqaOhp/+/o/ABkRe4AAAAAAAAAAAAAAAAIZ4tnwOfI48sYvRDnhf/kuP2AGHwhZGX//CHnhZ/8I2eFgGT2qj/8M8AIBSBYTAQm4t8WCUBQB/PhBbo4T7UTQ0//TP9MA0X/4Yfhm+GP4Yt7XDf+V1NHQ0//f0fgAyIvcAAAAAAAAAAAAAAAAEM8Wz4HPkceWMXohzwv/yXH7AMiL3AAAAAAAAAAAAAAAABDPFs+Bz5JW+LBKIc8L/8lx+wAw+ELIy//4Q88LP/hGzwsAye1UfxUABPhnAHLccCLQ1gIx0gAw3CHHAJLyO+Ah1w0fkvI84VMRkvI74cEEIoIQ/////byxkvI84AHwAfhHbpLyPN4=",
				"KCGM36iTYuCYynk+Jnemis+mcwi3RFCke95i7l96s4Q="
			},
			new object[] {
				new ParamsOfEncodeMessage {
					Abi = TestsEnv.Packages.Events.Abi,
					DeploySet = new DeploySet { Tvc = TestsEnv.Packages.Events.Tvc },
					CallSet = new CallSet {
						FunctionName = "constructor",
						Header = new FunctionHeader {
							Time = Time,
							Expire = Expire
						}
					},
					Signer = new Signer.Keys {
						KeysAccessor = Keys
					}
				},
				"te6ccgECGAEAA6wAA0eIAAt9aqvShfTon7Lei1PVOhUEkEEZQkhDKPgNyzeTL6YSEbAHAgEA4bE5Gr3mWwDtlcEOWHr6slWoyQlpIWeYyw/00eKFGFkbAJMMFLWnu0mq4HSrPmktmzeeAboa4kxkFymCsRVt44dTHxAj/Hd67jWQF7peccWoU/dbMCBJBB6YdPCVZcJlJkAAAF0ZyXLg19VzGRotV8/gAQHAAwIDzyAGBAEB3gUAA9AgAEHaY+IEf47vXcayAvdLzji1Cn7rZgQJIIPTDp4SrLhMpMwCJv8A9KQgIsABkvSg4YrtU1gw9KEKCAEK9KQg9KEJAAACASANCwHI/38h7UTQINdJwgGOENP/0z/TANF/+GH4Zvhj+GKOGPQFcAGAQPQO8r3XC//4YnD4Y3D4Zn/4YeLTAAGOHYECANcYIPkBAdMAAZTT/wMBkwL4QuIg+GX5EPKoldMAAfJ64tM/AQwAao4e+EMhuSCfMCD4I4ED6KiCCBt3QKC53pL4Y+CANPI02NMfAfgjvPK50x8B8AH4R26S8jzeAgEgEw4CASAQDwC9uotV8/+EFujjXtRNAg10nCAY4Q0//TP9MA0X/4Yfhm+GP4Yo4Y9AVwAYBA9A7yvdcL//hicPhjcPhmf/hh4t74RvJzcfhm0fgA+ELIy//4Q88LP/hGzwsAye1Uf/hngCASASEQDluIAGtb8ILdHCfaiaGn/6Z/pgGi//DD8M3wx/DFvfSDK6mjofSBv6PwikDdJGDhvfCFdeXAyfABkZP2CEGRnwoRnRoIEB9AAAAAAAAAAAAAAAAAAIGeLZMCAQH2AGHwhZGX//CHnhZ/8I2eFgGT2qj/8M8ADFuZPCot8ILdHCfaiaGn/6Z/pgGi//DD8M3wx/DFva4b/yupo6Gn/7+j8AGRF7gAAAAAAAAAAAAAAAAhni2fA58jjyxi9EOeF/+S4/YAYfCFkZf/8IeeFn/wjZ4WAZPaqP/wzwAgFIFxQBCbi3xYJQFQH8+EFujhPtRNDT/9M/0wDRf/hh+Gb4Y/hi3tcN/5XU0dDT/9/R+ADIi9wAAAAAAAAAAAAAAAAQzxbPgc+Rx5YxeiHPC//JcfsAyIvcAAAAAAAAAAAAAAAAEM8Wz4HPklb4sEohzwv/yXH7ADD4QsjL//hDzws/+EbPCwDJ7VR/FgAE+GcActxwItDWAjHSADDcIccAkvI74CHXDR+S8jzhUxGS8jvhwQQighD////9vLGS8jzgAfAB+EdukvI83g==",
				null
			},
			new object[] {
				new ParamsOfEncodeMessage {
					Abi = TestsEnv.Packages.Events.Abi,
					Address = "0:05beb555e942fa744fd96f45a9ea9d0a8248208ca12421947c06e59bc997d309",
					CallSet = new CallSet {
						FunctionName = "returnValue",
						Header = new FunctionHeader {
							Time = Time,
							Expire = Expire
						},
						Input = ZeroIdElement
					},
					Signer = new Signer.External {
						PublicKey = Keys.Public
					}
				},
				"te6ccgEBAgEAeAABpYgAC31qq9KF9Oifst6LU9U6FQSQQRlCSEMo+A3LN5MvphIFMfECP8d3ruNZAXul5xxahT91swIEkEHph08JVlwmUmQAAAXRnJcuDX1XMZBW+LBKAQBAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=",
				"i4Hs3PB12QA9UBFbOIpkG3JerHHqjm4LgvF4MA7TDsY="
			},
			new object[] {
				new ParamsOfEncodeMessage {
					Abi = TestsEnv.Packages.Events.Abi,
					Address = "0:05beb555e942fa744fd96f45a9ea9d0a8248208ca12421947c06e59bc997d309",
					CallSet = new CallSet {
						FunctionName = "returnValue",
						Header = new FunctionHeader {
							Time = Time,
							Expire = Expire
						},
						Input = ZeroIdElement
					},
					Signer = new Signer.Keys {
						KeysAccessor = Keys
					}
				},
				"te6ccgEBAwEAvAABRYgAC31qq9KF9Oifst6LU9U6FQSQQRlCSEMo+A3LN5MvphIMAQHhrd/b+MJ5Za+AygBc5qS/dVIPnqxCsM9PvqfVxutK+lnQEKzQoRTLYO6+jfM8TF4841bdNjLQwIDWL4UVFdxIhdMfECP8d3ruNZAXul5xxahT91swIEkEHph08JVlwmUmQAAAXRnJcuDX1XMZBW+LBKACAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==",
				null
			},
			new object[] {
				new ParamsOfEncodeMessage {
					Abi = TestsEnv.Packages.Events.Abi,
					Address = "0:05beb555e942fa744fd96f45a9ea9d0a8248208ca12421947c06e59bc997d309",
					CallSet = new CallSet {
						FunctionName = "returnValue",
						Header = new FunctionHeader {
							Time = Time,
							Expire = Expire
						},
						Input = ZeroIdElement
					},
					Signer = new Signer.None()
				},
				"te6ccgEBAQEAVQAApYgAC31qq9KF9Oifst6LU9U6FQSQQRlCSEMo+A3LN5MvphIAAAAC6M5Llwa+q5jIK3xYJAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAB",
				null
			}
		};

		public IEnumerator<object[]> GetEnumerator() {
			return _data.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}

	[Fact]
	public async Task AttachSignature() {
		string signature = await _everClient.SignDetached(Keys, "KCGM36iTYuCYynk+Jnemis+mcwi3RFCke95i7l96s4Q=");

		signature.Should()
		         .Be("6272357bccb601db2b821cb0f5f564ab519212d242cf31961fe9a3c50a30b236012618296b4f769355c0e9567cd25b366f3c037435c498c82e5305622adbc70e");

		ResultOfAttachSignature signed = await _everClient.Abi.AttachSignature(new ParamsOfAttachSignature {
			Abi = TestsEnv.Packages.Events.Abi,
			PublicKey = Keys.Public,
			Message =
				"te6ccgECFwEAA2gAAqeIAAt9aqvShfTon7Lei1PVOhUEkEEZQkhDKPgNyzeTL6YSEZTHxAj/Hd67jWQF7peccWoU/dbMCBJBB6YdPCVZcJlJkAAAF0ZyXLg19VzGRotV8/gGAQEBwAICA88gBQMBAd4EAAPQIABB2mPiBH+O713GsgL3S844tQp+62YECSCD0w6eEqy4TKTMAib/APSkICLAAZL0oOGK7VNYMPShCQcBCvSkIPShCAAAAgEgDAoByP9/Ie1E0CDXScIBjhDT/9M/0wDRf/hh+Gb4Y/hijhj0BXABgED0DvK91wv/+GJw+GNw+GZ/+GHi0wABjh2BAgDXGCD5AQHTAAGU0/8DAZMC+ELiIPhl+RDyqJXTAAHyeuLTPwELAGqOHvhDIbkgnzAg+COBA+iogggbd0Cgud6S+GPggDTyNNjTHwH4I7zyudMfAfAB+EdukvI83gIBIBINAgEgDw4AvbqLVfP/hBbo417UTQINdJwgGOENP/0z/TANF/+GH4Zvhj+GKOGPQFcAGAQPQO8r3XC//4YnD4Y3D4Zn/4YeLe+Ebyc3H4ZtH4APhCyMv/+EPPCz/4Rs8LAMntVH/4Z4AgEgERAA5biABrW/CC3Rwn2omhp/+mf6YBov/ww/DN8Mfwxb30gyupo6H0gb+j8IpA3SRg4b3whXXlwMnwAZGT9ghBkZ8KEZ0aCBAfQAAAAAAAAAAAAAAAAACBni2TAgEB9gBh8IWRl//wh54Wf/CNnhYBk9qo//DPAAxbmTwqLfCC3Rwn2omhp/+mf6YBov/ww/DN8Mfwxb2uG/8rqaOhp/+/o/ABkRe4AAAAAAAAAAAAAAAAIZ4tnwOfI48sYvRDnhf/kuP2AGHwhZGX//CHnhZ/8I2eFgGT2qj/8M8AIBSBYTAQm4t8WCUBQB/PhBbo4T7UTQ0//TP9MA0X/4Yfhm+GP4Yt7XDf+V1NHQ0//f0fgAyIvcAAAAAAAAAAAAAAAAEM8Wz4HPkceWMXohzwv/yXH7AMiL3AAAAAAAAAAAAAAAABDPFs+Bz5JW+LBKIc8L/8lx+wAw+ELIy//4Q88LP/hGzwsAye1UfxUABPhnAHLccCLQ1gIx0gAw3CHHAJLyO+Ah1w0fkvI84VMRkvI74cEEIoIQ/////byxkvI84AHwAfhHbpLyPN4=",
			Signature = signature
		});

		signed.Message.Should()
		      .Be(
			      "te6ccgECGAEAA6wAA0eIAAt9aqvShfTon7Lei1PVOhUEkEEZQkhDKPgNyzeTL6YSEbAHAgEA4bE5Gr3mWwDtlcEOWHr6slWoyQlpIWeYyw/00eKFGFkbAJMMFLWnu0mq4HSrPmktmzeeAboa4kxkFymCsRVt44dTHxAj/Hd67jWQF7peccWoU/dbMCBJBB6YdPCVZcJlJkAAAF0ZyXLg19VzGRotV8/gAQHAAwIDzyAGBAEB3gUAA9AgAEHaY+IEf47vXcayAvdLzji1Cn7rZgQJIIPTDp4SrLhMpMwCJv8A9KQgIsABkvSg4YrtU1gw9KEKCAEK9KQg9KEJAAACASANCwHI/38h7UTQINdJwgGOENP/0z/TANF/+GH4Zvhj+GKOGPQFcAGAQPQO8r3XC//4YnD4Y3D4Zn/4YeLTAAGOHYECANcYIPkBAdMAAZTT/wMBkwL4QuIg+GX5EPKoldMAAfJ64tM/AQwAao4e+EMhuSCfMCD4I4ED6KiCCBt3QKC53pL4Y+CANPI02NMfAfgjvPK50x8B8AH4R26S8jzeAgEgEw4CASAQDwC9uotV8/+EFujjXtRNAg10nCAY4Q0//TP9MA0X/4Yfhm+GP4Yo4Y9AVwAYBA9A7yvdcL//hicPhjcPhmf/hh4t74RvJzcfhm0fgA+ELIy//4Q88LP/hGzwsAye1Uf/hngCASASEQDluIAGtb8ILdHCfaiaGn/6Z/pgGi//DD8M3wx/DFvfSDK6mjofSBv6PwikDdJGDhvfCFdeXAyfABkZP2CEGRnwoRnRoIEB9AAAAAAAAAAAAAAAAAAIGeLZMCAQH2AGHwhZGX//CHnhZ/8I2eFgGT2qj/8M8ADFuZPCot8ILdHCfaiaGn/6Z/pgGi//DD8M3wx/DFva4b/yupo6Gn/7+j8AGRF7gAAAAAAAAAAAAAAAAhni2fA58jjyxi9EOeF/+S4/YAYfCFkZf/8IeeFn/wjZ4WAZPaqP/wzwAgFIFxQBCbi3xYJQFQH8+EFujhPtRNDT/9M/0wDRf/hh+Gb4Y/hi3tcN/5XU0dDT/9/R+ADIi9wAAAAAAAAAAAAAAAAQzxbPgc+Rx5YxeiHPC//JcfsAyIvcAAAAAAAAAAAAAAAAEM8Wz4HPklb4sEohzwv/yXH7ADD4QsjL//hDzws/+EbPCwDJ7VR/FgAE+GcActxwItDWAjHSADDcIccAkvI74CHXDR+S8jzhUxGS8jvhwQQighD////9vLGS8jzgAfAB+EdukvI83g==");
	}

	[Fact]
	//hmm useless?? see AttachSignature
	public async Task AttachSignature2() {
		string signature = await _everClient.SignDetached(Keys, "i4Hs3PB12QA9UBFbOIpkG3JerHHqjm4LgvF4MA7TDsY=");

		signature.Should()
		         .Be("5bbfb7f184f2cb5f019400b9cd497eeaa41f3d5885619e9f7d4fab8dd695f4b3a02159a1422996c1dd7d1be67898bc79c6adba6c65a18101ac5f0a2a2bb8910b");

		ResultOfAttachSignature signed = await _everClient.Abi.AttachSignature(new ParamsOfAttachSignature {
			Abi = TestsEnv.Packages.Events.Abi,
			PublicKey = Keys.Public,
			Message =
				"te6ccgEBAgEAeAABpYgAC31qq9KF9Oifst6LU9U6FQSQQRlCSEMo+A3LN5MvphIFMfECP8d3ruNZAXul5xxahT91swIEkEHph08JVlwmUmQAAAXRnJcuDX1XMZBW+LBKAQBAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=",
			Signature = signature
		});

		signed.Message.Should()
		      .Be(
			      "te6ccgEBAwEAvAABRYgAC31qq9KF9Oifst6LU9U6FQSQQRlCSEMo+A3LN5MvphIMAQHhrd/b+MJ5Za+AygBc5qS/dVIPnqxCsM9PvqfVxutK+lnQEKzQoRTLYO6+jfM8TF4841bdNjLQwIDWL4UVFdxIhdMfECP8d3ruNZAXul5xxahT91swIEkEHph08JVlwmUmQAAAXRnJcuDX1XMZBW+LBKACAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==");
	}

	[Fact]
	public async Task DecodeMessageWithBody() {
		DecodedMessageBody result = await _everClient.Abi.DecodeMessageBody(new ParamsOfDecodeMessageBody {
			Abi = TestsEnv.Packages.Events.Abi,
			Body =
				"te6ccgEBAgEAlgAB4a3f2/jCeWWvgMoAXOakv3VSD56sQrDPT76n1cbrSvpZ0BCs0KEUy2Duvo3zPExePONW3TYy0MCA1i+FFRXcSIXTHxAj/Hd67jWQF7peccWoU/dbMCBJBB6YdPCVZcJlJkAAAF0ZyXLg19VzGQVviwSgAQBAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=",
			IsInternal = false
		});

		result.BodyType.Should().Be(MessageBodyType.Input);
		result.Name.Should().Be("returnValue");
		result.Value.Should().NotBeNull();
		result.Value.Get<string>("id").Should().Be("0x0000000000000000000000000000000000000000000000000000000000000000");
		result.Header.Expire.Should().Be(Expire);
		result.Header.Time.Should().Be(Time);
		result.Header.Pubkey.Should().Be("4c7c408ff1ddebb8d6405ee979c716a14fdd6cc08124107a61d3c25597099499");
	}

	[Fact]
	public async Task DecodeMessageWithEvent() {
		DecodedMessageBody result = await _everClient.Abi.DecodeMessage(new ParamsOfDecodeMessage {
			Abi = TestsEnv.Packages.Events.Abi,
			Message =
				"te6ccgEBAQEAVQAApeACvg5/pmQpY4m61HmJ0ne+zjHJu3MNG8rJxUDLbHKBu/AAAAAAAAAMJL6z6ro48sYvAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABA"
		});

		result.BodyType.Should().Be(MessageBodyType.Event);
		result.Name.Should().Be("EventThrown");
		result.Value.Should().NotBeNull();
		result.Value.Get<string>("id").Should().Be("0x0000000000000000000000000000000000000000000000000000000000000000");
		result.Header.Should().BeNull();
	}

	[Fact]
	public async Task DecodeMessageWithInput() {
		DecodedMessageBody result = await _everClient.Abi.DecodeMessage(new ParamsOfDecodeMessage {
			Abi = TestsEnv.Packages.Events.Abi,
			Message =
				"te6ccgEBAwEAvAABRYgAC31qq9KF9Oifst6LU9U6FQSQQRlCSEMo+A3LN5MvphIMAQHhrd/b+MJ5Za+AygBc5qS/dVIPnqxCsM9PvqfVxutK+lnQEKzQoRTLYO6+jfM8TF4841bdNjLQwIDWL4UVFdxIhdMfECP8d3ruNZAXul5xxahT91swIEkEHph08JVlwmUmQAAAXRnJcuDX1XMZBW+LBKACAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=="
		});

		result.BodyType.Should().Be(MessageBodyType.Input);
		result.Name.Should().Be("returnValue");
		result.Value.Should().NotBeNull();
		result.Value.Get<string>("id").Should().Be("0x0000000000000000000000000000000000000000000000000000000000000000");
		result.Header.Expire.Should().Be(Expire);
		result.Header.Time.Should().Be(Time);
		result.Header.Pubkey.Should().Be("4c7c408ff1ddebb8d6405ee979c716a14fdd6cc08124107a61d3c25597099499");
	}
}
