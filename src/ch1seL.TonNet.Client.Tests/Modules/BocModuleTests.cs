using System.Threading.Tasks;
using ch1seL.TonNet.Client.Models;
using ch1seL.TonNet.Client.Tests;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace TonSdk.Tests.Modules
{
    public class BocTests : TonClientTestsBase
    {
        public BocTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
        }

        [Fact]
        public async Task GetBocHash()
        {
            var request = new GetBocHashRequest
            {
                Boc = "te6ccgEBAQEAWAAAq2n+AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAE/zMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzSsG8DgAAAAAjuOu9NAL7BxYpA"
            };

            GetBocHashResponse response = await TonClient.Boc.GetBocHash(request);

            response.Hash.Should().Be("dfd47194f3058ee058bfbfad3ea40cbbd9ad17ca77cd0904d4d9f18a48c2fbca");
        }

        [Fact]
        public async Task ShouldParseMessage()
        {
            var request = new ParseRequest
            {
                Boc = "te6ccgEBAQEAWAAAq2n+AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAE/zMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzMzSsG8DgAAAAAjuOu9NAL7BxYpA"
            };

            ParseResponse response = await TonClient.Boc.ParseMessage(request);

            response.Parsed.GetProperty("id").GetString().Should().Be("dfd47194f3058ee058bfbfad3ea40cbbd9ad17ca77cd0904d4d9f18a48c2fbca");
            response.Parsed.GetProperty("src").GetString().Should().Be("-1:0000000000000000000000000000000000000000000000000000000000000000");
            response.Parsed.GetProperty("dst").GetString().Should().Be("-1:3333333333333333333333333333333333333333333333333333333333333333");
        }
    }
}