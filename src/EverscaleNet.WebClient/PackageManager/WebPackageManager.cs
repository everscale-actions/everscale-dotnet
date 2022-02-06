using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using EverscaleNet.Abstract;
using EverscaleNet.Client.Models;
using EverscaleNet.Models;
using EverscaleNet.Serialization;
using Microsoft.Extensions.Options;

namespace EverscaleNet.WebClient.PackageManager;

/// <inheritdoc />
public class WebPackageManager : IEverPackageManager {
	private const string AbiFileTemplate = "{0}.abi.json";
	private const string TvcFileTemplate = "{0}.tvc";
	private readonly HttpClient _httpClient;
	private readonly IOptions<WebPackageManagerOptions> _optionsAccessor;

	/// <summary>
	/// </summary>
	/// <param name="httpClient"></param>
	/// <param name="optionsAccessor"></param>
	public WebPackageManager(HttpClient httpClient, IOptions<WebPackageManagerOptions> optionsAccessor) {
		_httpClient = httpClient;
		_optionsAccessor = optionsAccessor;
	}

	private static string Combine(string uri1, string uri2) {
		uri1 = uri1.TrimEnd('/');
		uri2 = uri2.TrimStart('/');
		return $"{uri1}/{uri2}";
	}

	/// <inheritdoc />
	public async Task<Package> LoadPackage(string name, CancellationToken cancellationToken = default) {
		Task<Abi> getAbiTask = LoadAbi(name, cancellationToken);
		Task<string> getTvcTask = LoadTvc(name, cancellationToken);
		// do it parallel 
		await Task.WhenAll(getAbiTask, getTvcTask);

		Abi abi = await getAbiTask;
		string tvc = await getTvcTask;

		return new Package(abi, tvc);
	}

	/// <inheritdoc />
	public async Task<Abi> LoadAbi(string name, CancellationToken cancellationToken = default) {
		string fileUrl = Combine(_optionsAccessor.Value.PackagesPath, string.Format(AbiFileTemplate, name));
		var abiContract =
			await _httpClient.GetFromJsonAsync<AbiContract>(fileUrl, JsonOptionsProvider.JsonSerializerOptions, cancellationToken: cancellationToken);
		return new Abi.Contract { Value = abiContract };
	}

	/// <inheritdoc />
	public async Task<string> LoadTvc(string name, CancellationToken cancellationToken = default) {
		string fileUrl = Combine(_optionsAccessor.Value.PackagesPath, string.Format(TvcFileTemplate, name));
		byte[] bytes = await _httpClient.GetByteArrayAsync(fileUrl, cancellationToken);
		return Convert.ToBase64String(bytes);
	}
}
