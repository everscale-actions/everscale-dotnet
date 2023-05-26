namespace EverscaleNet.WebClient.PackageManager;

/// <inheritdoc />
public class WebPackageManager : IEverPackageManager {
	private readonly HttpClient _httpClient;
	private readonly WebPackageManagerOptions _options;

	/// <summary>
	/// </summary>
	/// <param name="httpClient"></param>
	/// <param name="optionsAccessor"></param>
	public WebPackageManager(HttpClient httpClient, IOptions<WebPackageManagerOptions> optionsAccessor) {
		_httpClient = httpClient;
		_options = optionsAccessor.Value;

		string basePath = _options.BasePath ?? throw new InvalidOperationException($"Provide {nameof(WebPackageManagerOptions)}.{nameof(WebPackageManagerOptions.BasePath)} to use WebPackageManager");
		_httpClient.BaseAddress = new Uri(basePath);
	}

	/// <inheritdoc />
	public async Task<Abi?> LoadAbi(string name, CancellationToken cancellationToken = default) {
		string fileUrl = Combine(_options.PackagesPath, string.Format(_options.AbiFileTemplate, name));
		var abiContract =
			await _httpClient.GetFromJsonAsync<AbiContract>(fileUrl, JsonOptionsProvider.JsonSerializerOptions, cancellationToken);
		return new Abi.Contract { Value = abiContract };
	}

	/// <inheritdoc />
	public async Task<string?> LoadTvc(string name, CancellationToken cancellationToken = default) {
		string fileUrl = Combine(_options.PackagesPath, string.Format(_options.TvcFileTemplate, name));
		byte[] bytes = await _httpClient.GetByteArrayAsync(fileUrl, cancellationToken);
		return Convert.ToBase64String(bytes);
	}

	/// <inheritdoc />
	public async Task<KeyPair?> LoadKeyPair(string name, CancellationToken cancellationToken = default) {
		string fileUrl = Combine(_options.PackagesPath, string.Format(_options.KeyPairFileTemplate, name));
		var keyPair =
			await _httpClient.GetFromJsonAsync<KeyPair>(fileUrl, JsonOptionsProvider.JsonSerializerOptions, cancellationToken);
		return keyPair ?? throw new NullReferenceException("Key pair should not be null");
	}

	/// <inheritdoc />
	public async Task<string?> LoadCode(string name, CancellationToken cancellationToken = default) {
		string fileUrl = Combine(_options.PackagesPath, string.Format(_options.CodeFileTemplate, name));
		return await _httpClient.GetStringAsync(fileUrl, cancellationToken);
	}

	/// <inheritdoc />
	public async Task<string?> LoadBase64(string name, CancellationToken cancellationToken) {
		string fileUrl = Combine(_options.PackagesPath, string.Format(_options.Base64FileTemplate, name));
		return await _httpClient.GetStringAsync(fileUrl, cancellationToken);
	}

	private static string Combine(string uri1, string uri2) {
		uri1 = uri1.TrimEnd('/');
		uri2 = uri2.TrimStart('/');
		return $"{uri1}/{uri2}";
	}
}
