using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using EverscaleNet.Abstract;
using EverscaleNet.Client.Models;
using EverscaleNet.Serialization;
using Microsoft.Extensions.Options;

namespace EverscaleNet.WebClient.PackageManager;

/// <inheritdoc />
public class WebPackageManager : IEverPackageManager {
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

	/// <inheritdoc />
	public async Task<Abi> LoadAbi(string name, CancellationToken cancellationToken = default) {
		string fileUrl = Combine(_optionsAccessor.Value.PackagesPath, string.Format(IEverPackageManager.AbiFileTemplate, name));
		var abiContract =
			await _httpClient.GetFromJsonAsync<AbiContract>(fileUrl, JsonOptionsProvider.JsonSerializerOptions, cancellationToken);
		return new Abi.Contract { Value = abiContract };
	}

	/// <inheritdoc />
	public async Task<string> LoadTvc(string name, CancellationToken cancellationToken = default) {
		string fileUrl = Combine(_optionsAccessor.Value.PackagesPath, string.Format(IEverPackageManager.TvcFileTemplate, name));
		byte[] bytes = await _httpClient.GetByteArrayAsync(fileUrl, cancellationToken);
		return Convert.ToBase64String(bytes);
	}

	/// <inheritdoc />
	public async Task<KeyPair> LoadKeyPair(string name, CancellationToken cancellationToken = default) {
		string fileUrl = Combine(_optionsAccessor.Value.PackagesPath, string.Format(IEverPackageManager.KeyPairFileTemplate, name));
		var keyPair =
			await _httpClient.GetFromJsonAsync<KeyPair>(fileUrl, JsonOptionsProvider.JsonSerializerOptions, cancellationToken);
		return keyPair ?? throw new NullReferenceException("Key pair should not be null");
	}

	private static string Combine(string uri1, string uri2) {
		uri1 = uri1.TrimEnd('/');
		uri2 = uri2.TrimStart('/');
		return $"{uri1}/{uri2}";
	}
}
