using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Models;
using ch1seL.TonNet.Serialization;
using Microsoft.Extensions.Options;

namespace ch1seL.TonNet.Client.Blazor.PackageManager
{
    public class BlazorPackageManager : ITonPackageManager
    {
        private const string AbiFileTemplate = "{0}.abi.json";
        private const string TvcFileTemplate = "{0}.tvc";
        private readonly HttpClient _httpClient;
        private readonly IOptions<BlazorPackageManagerOptions> _optionsAccessor;

        public BlazorPackageManager(HttpClient httpClient, IOptions<BlazorPackageManagerOptions> optionsAccessor)
        {
            _httpClient = httpClient;
            _optionsAccessor = optionsAccessor;
        }

        public async Task<Package> LoadPackage(string name)
        {
            var getAbiTask = LoadAbi(name);
            var getTvcTask = LoadTvc(name);
            // do it parallel 
            await Task.WhenAll(getAbiTask, getTvcTask);

            Abi abi = await getAbiTask;
            var tvc = await getTvcTask;

            return new Package(abi, tvc);
        }

        public async Task<Abi> LoadAbi(string name)
        {
            var fileUrl = Combine(_optionsAccessor.Value.PackagesPath, string.Format(AbiFileTemplate, name));
            var abiContract =
                await _httpClient.GetFromJsonAsync<AbiContract>(fileUrl, JsonOptionsProvider.JsonSerializerOptions);
            return new Abi.Contract { Value = abiContract };
        }

        public async Task<string> LoadTvc(string name)
        {
            var fileUrl = Combine(_optionsAccessor.Value.PackagesPath, string.Format(TvcFileTemplate, name));
            var bytes = await _httpClient.GetByteArrayAsync(fileUrl);
            return Convert.ToBase64String(bytes);
        }

        private static string Combine(string uri1, string uri2)
        {
            uri1 = uri1.TrimEnd('/');
            uri2 = uri2.TrimStart('/');
            return $"{uri1}/{uri2}";
        }
    }
}