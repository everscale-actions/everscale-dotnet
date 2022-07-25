using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using EverscaleNet.Abstract;
using EverscaleNet.Client.Models;
using EverscaleNet.Serialization;
using Microsoft.Extensions.Options;

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
	public async Task<Abi> LoadAbi(string name, CancellationToken cancellationToken = default) {
		string filePath = Path.Join(_options.PackagesPath, string.Format(_options.AbiFileTemplate, name));
		var fileInfo = new FileInfo(filePath);
		await using FileStream fs = fileInfo.OpenRead();
		var abiContract =
			await JsonSerializer.DeserializeAsync<AbiContract>(fs, JsonOptionsProvider.JsonSerializerOptions, cancellationToken);
		return new Abi.Contract { Value = abiContract };
	}

	/// <inheritdoc />
	public async Task<string> LoadTvc(string name, CancellationToken cancellationToken = default) {
		string filePath = Path.Join(_options.PackagesPath, string.Format(_options.TvcFileTemplate, name));
		byte[] bytes = await File.ReadAllBytesAsync(filePath, cancellationToken);
		return Convert.ToBase64String(bytes);
	}
}
