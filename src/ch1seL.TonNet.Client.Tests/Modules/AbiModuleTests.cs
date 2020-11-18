using System.Threading.Tasks;
using ch1seL.TonNet.Client.Models;
using FluentAssertions;
using TestsShared;
using Xunit;
using Xunit.Abstractions;

namespace ch1seL.TonNet.Client.Tests.Modules
{
    public class AbiModuleTests : TonClientTestsBase
    {
        private readonly uint _expire;
        private readonly KeyPair _keys;
        private readonly ulong _time;

        public AbiModuleTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _keys = new KeyPair
            {
                Public = "4c7c408ff1ddebb8d6405ee979c716a14fdd6cc08124107a61d3c25597099499",
                Secret = "cc8929d635719612a9478b9cd17675a39cfad52d8959e8a177389b8c0b9122a7"
            };
            _time = 1599458364291;
            _expire = 1599458404;
        }

        [Fact]
        public async Task EncodeMessageConstructorSignerExternal()
        {
            TestPackage eventPackage = await TestPackage.GetPackage("Events", 2);
            var request = new EncodeMessageRequest
            {
                Abi = eventPackage.Abi,
                DeploySet = new DeploySet
                {
                    Tvc = eventPackage.Tvc
                },
                CallSet = new CallSet
                {
                    FunctionName = "constructor",
                    Header = new FunctionHeader
                    {
                        Time = _time,
                        Expire = _expire
                    }
                },
                Signer = new Signer.External
                {
                    PublicKey = "4c7c408ff1ddebb8d6405ee979c716a14fdd6cc08124107a61d3c25597099499"
                }
            };

            EncodeMessageResponse unsigned = await TonClient.Abi.EncodeMessage(request);

            unsigned.Should().NotBeNull();
            unsigned.Message.Should()
                .Be(
                    "te6ccgECFwEAA2gAAqeIAAt9aqvShfTon7Lei1PVOhUEkEEZQkhDKPgNyzeTL6YSEZTHxAj/Hd67jWQF7peccWoU/dbMCBJBB6YdPCVZcJlJkAAAF0ZyXLg19VzGRotV8/gGAQEBwAICA88gBQMBAd4EAAPQIABB2mPiBH+O713GsgL3S844tQp+62YECSCD0w6eEqy4TKTMAib/APSkICLAAZL0oOGK7VNYMPShCQcBCvSkIPShCAAAAgEgDAoByP9/Ie1E0CDXScIBjhDT/9M/0wDRf/hh+Gb4Y/hijhj0BXABgED0DvK91wv/+GJw+GNw+GZ/+GHi0wABjh2BAgDXGCD5AQHTAAGU0/8DAZMC+ELiIPhl+RDyqJXTAAHyeuLTPwELAGqOHvhDIbkgnzAg+COBA+iogggbd0Cgud6S+GPggDTyNNjTHwH4I7zyudMfAfAB+EdukvI83gIBIBINAgEgDw4AvbqLVfP/hBbo417UTQINdJwgGOENP/0z/TANF/+GH4Zvhj+GKOGPQFcAGAQPQO8r3XC//4YnD4Y3D4Zn/4YeLe+Ebyc3H4ZtH4APhCyMv/+EPPCz/4Rs8LAMntVH/4Z4AgEgERAA5biABrW/CC3Rwn2omhp/+mf6YBov/ww/DN8Mfwxb30gyupo6H0gb+j8IpA3SRg4b3whXXlwMnwAZGT9ghBkZ8KEZ0aCBAfQAAAAAAAAAAAAAAAAACBni2TAgEB9gBh8IWRl//wh54Wf/CNnhYBk9qo//DPAAxbmTwqLfCC3Rwn2omhp/+mf6YBov/ww/DN8Mfwxb2uG/8rqaOhp/+/o/ABkRe4AAAAAAAAAAAAAAAAIZ4tnwOfI48sYvRDnhf/kuP2AGHwhZGX//CHnhZ/8I2eFgGT2qj/8M8AIBSBYTAQm4t8WCUBQB/PhBbo4T7UTQ0//TP9MA0X/4Yfhm+GP4Yt7XDf+V1NHQ0//f0fgAyIvcAAAAAAAAAAAAAAAAEM8Wz4HPkceWMXohzwv/yXH7AMiL3AAAAAAAAAAAAAAAABDPFs+Bz5JW+LBKIc8L/8lx+wAw+ELIy//4Q88LP/hGzwsAye1UfxUABPhnAHLccCLQ1gIx0gAw3CHHAJLyO+Ah1w0fkvI84VMRkvI74cEEIoIQ/////byxkvI84AHwAfhHbpLyPN4=");
            unsigned.DataToSign.Should()
                .Be("KCGM36iTYuCYynk+Jnemis+mcwi3RFCke95i7l96s4Q=");
        }
    }
}