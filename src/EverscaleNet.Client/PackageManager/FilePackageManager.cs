namespace EverscaleNet.Client.PackageManager;

/// <inheritdoc />
public class FilePackageManager : IEverPackageManager {
	private readonly FilePackageManagerOptions _options;

	/// <summary>
	///     Create FilePackageManager
	/// </summary>
	/// <param name="optionsAccessor"></param>
	public FilePackageManager(IOptions<FilePackageManagerOptions> optionsAccessor) {
		_options = optionsAccessor.Value;
	}

	/// <inheritdoc />
	public async Task<Abi?> LoadAbi(string name, CancellationToken cancellationToken = default) {
		string filePath = Path.Join(_options.PackagesPath, string.Format(_options.AbiFileTemplate, name));
		var fileInfo = new FileInfo(filePath);
		if (!fileInfo.Exists) {
			return null;
		}
		await using FileStream fs = fileInfo.OpenRead();
		var abiContract =
			await JsonSerializer.DeserializeAsync<AbiContract>(fs, JsonOptionsProvider.JsonSerializerOptions, cancellationToken);
		return new Abi.Contract { Value = abiContract };
	}

	/// <inheritdoc />
	public async Task<string?> LoadTvc(string name, CancellationToken cancellationToken = default) {
		string filePath = Path.Join(_options.PackagesPath, string.Format(_options.TvcFileTemplate, name));
		var fileInfo = new FileInfo(filePath);
		if (!fileInfo.Exists) {
			return null;
		}
		byte[] bytes = await File.ReadAllBytesAsync(filePath, cancellationToken);
		return Convert.ToBase64String(bytes);
	}

	/// <inheritdoc />
	public async Task<KeyPair?> LoadKeyPair(string name, CancellationToken cancellationToken = default) {
		string filePath = Path.Join(_options.PackagesPath, string.Format(_options.KeyPairFileTemplate, name));
		var fileInfo = new FileInfo(filePath);
		if (!fileInfo.Exists) {
			return null;
		}
		await using FileStream fs = fileInfo.OpenRead();
		return await JsonSerializer.DeserializeAsync<KeyPair>(fs, JsonOptionsProvider.JsonSerializerOptions, cancellationToken);
	}

	/// <inheritdoc />
	public async Task<string?> LoadCode(string name, CancellationToken cancellationToken = default) {
		string filePath = Path.Join(_options.PackagesPath, string.Format(_options.CodeFileTemplate, name));
		var fileInfo = new FileInfo(filePath);
		if (!fileInfo.Exists) {
			return null;
		}
		using StreamReader fs = fileInfo.OpenText();
		return await fs.ReadToEndAsync();
	}

	/// <inheritdoc />
	public async Task<string?> LoadBase64(string name, CancellationToken cancellationToken) {
		string filePath = Path.Join(_options.PackagesPath, string.Format(_options.Base64FileTemplate, name));
		var fileInfo = new FileInfo(filePath);
		if (!fileInfo.Exists) {
			return null;
		}
		using StreamReader fs = fileInfo.OpenText();
		return await fs.ReadToEndAsync();
	}
}
