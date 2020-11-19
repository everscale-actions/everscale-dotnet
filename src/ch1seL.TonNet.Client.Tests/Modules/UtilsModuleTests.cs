using System.Threading.Tasks;
using ch1seL.TonNet.Client.Models;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace ch1seL.TonNet.Client.Tests.Modules
{
    public class UtilsModuleTests : IClassFixture<TonClientTestsFixture>
    {
        private readonly ITonClient _tonClient;
        public UtilsModuleTests(TonClientTestsFixture fixture, ITestOutputHelper outputHelper)
        {
            _tonClient = fixture.CreateClient(outputHelper);
        }

        [Fact]
        public async Task ConvertAddress()
        {
            ResultOfConvertAddress result = await _tonClient.Utils.ConvertAddress(new ParamsOfConvertAddress
            {
                Address = "fcb91a3a3816d0f7b8c2c76108b8a9bc5a6b7a55bd79f8ab101c52db29232260",
                OutputFormat = new AddressStringFormat.Hex()
            });

            result.Address.Should().Be("0:fcb91a3a3816d0f7b8c2c76108b8a9bc5a6b7a55bd79f8ab101c52db29232260");
        }

        [Fact]
        public async Task ConvertAddressHex()
        {
            ResultOfConvertAddress result = await _tonClient.Utils.ConvertAddress(new ParamsOfConvertAddress
            {
                Address = "fcb91a3a3816d0f7b8c2c76108b8a9bc5a6b7a55bd79f8ab101c52db29232260",
                OutputFormat = new AddressStringFormat.AccountId()
            });

            result.Address.Should().Be("fcb91a3a3816d0f7b8c2c76108b8a9bc5a6b7a55bd79f8ab101c52db29232260");
        }

        [Fact]
        public async Task ConvertAddressBase64()
        {
            ResultOfConvertAddress result = await _tonClient.Utils.ConvertAddress(new ParamsOfConvertAddress
            {
                Address = "-1:fcb91a3a3816d0f7b8c2c76108b8a9bc5a6b7a55bd79f8ab101c52db29232260",
                OutputFormat = new AddressStringFormat.Base64
                {
                    Bounce = false,
                    Test = false,
                    Url = false
                }
            });

            result.Address.Should().Be("Uf/8uRo6OBbQ97jCx2EIuKm8Wmt6Vb15+KsQHFLbKSMiYG+9");
        }

        [Fact]
        public async Task ConvertAddressBase64_2()
        {
            ResultOfConvertAddress result = await _tonClient.Utils.ConvertAddress(new ParamsOfConvertAddress
            {
                Address = "Uf/8uRo6OBbQ97jCx2EIuKm8Wmt6Vb15+KsQHFLbKSMiYG+9",
                OutputFormat = new AddressStringFormat.Base64
                {
                    Bounce = true,
                    Test = true,
                    Url = true
                }
            });

            result.Address.Should().Be("kf_8uRo6OBbQ97jCx2EIuKm8Wmt6Vb15-KsQHFLbKSMiYIny");
        }

        [Fact]
        public async Task ConvertAddressHexToHex()
        {
            ResultOfConvertAddress result = await _tonClient.Utils.ConvertAddress(new ParamsOfConvertAddress
            {
                Address = "kf_8uRo6OBbQ97jCx2EIuKm8Wmt6Vb15-KsQHFLbKSMiYIny",
                OutputFormat = new AddressStringFormat.Hex()
            });

            result.Address.Should().Be("-1:fcb91a3a3816d0f7b8c2c76108b8a9bc5a6b7a55bd79f8ab101c52db29232260");
        }
    }
}