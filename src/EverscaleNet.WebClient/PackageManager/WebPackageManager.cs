using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EverscaleNet.Abstract;
using EverscaleNet.Client.Models;
using EverscaleNet.Models;
using EverscaleNet.Serialization;
using Microsoft.Extensions.Options;

namespace EverscaleNet.WebClient.PackageManager;

public class WebPackageManager : IEverPackageManager {
	private const string AbiFileTemplate = "{0}.abi.json";
	private const string TvcFileTemplate = "{0}.tvc";
	private readonly HttpClient _httpClient;
	private readonly IOptions<WebPackageManagerOptions> _optionsAccessor;

	public WebPackageManager(HttpClient httpClient, IOptions<WebPackageManagerOptions> optionsAccessor) {
		_httpClient = httpClient;
		_optionsAccessor = optionsAccessor;
	}

	private static string Combine(string uri1, string uri2) {
		uri1 = uri1.TrimEnd('/');
		uri2 = uri2.TrimStart('/');
		return $"{uri1}/{uri2}";
	}

	public async Task<Package> LoadPackage(string name) {
		Task<Abi> getAbiTask = LoadAbi(name);
		Task<string> getTvcTask = LoadTvc(name);
		// do it parallel 
		await Task.WhenAll(getAbiTask, getTvcTask);

		Abi abi = await getAbiTask;
		string tvc = await getTvcTask;

		return new Package(abi, tvc);
	}

	public async Task<Abi> LoadAbi(string name) {
		string fileUrl = Combine(_optionsAccessor.Value.PackagesPath, string.Format(AbiFileTemplate, name));
		var abiContract =
			await _httpClient.GetFromJsonAsync<AbiContract>(fileUrl, JsonOptionsProvider.JsonSerializerOptions);
		return new Abi.Contract { Value = abiContract };
	}

	public async Task<string> LoadTvc(string name) {
		string fileUrl = Combine(_optionsAccessor.Value.PackagesPath, string.Format(TvcFileTemplate, name));
		byte[] bytes = await _httpClient.GetByteArrayAsync(fileUrl);
		return Convert.ToBase64String(bytes);
	}
}