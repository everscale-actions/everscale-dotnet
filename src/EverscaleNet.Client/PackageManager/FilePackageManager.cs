using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using EverscaleNet.Abstract;
using EverscaleNet.Client.Models;
using EverscaleNet.Models;
using EverscaleNet.Serialization;
using Microsoft.Extensions.Options;

namespace EverscaleNet.Client.PackageManager;

/// <inheritdoc />
public class FilePackageManager : IEverPackageManager {
	private const string AbiFileTemplate = "{0}.abi.json";
	private const string TvcFileTemplate = "{0}.tvc";
	private readonly FilePackageManagerOptions _options;

	/// <summary>
	///     Create FilePackageManager
	/// </summary>
	/// <param name="optionsAccessor"></param>
	public FilePackageManager(IOptions<FilePackageManagerOptions> optionsAccessor) {
		_options = optionsAccessor.Value;
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
		string filePath = Path.Join(_options.PackagesPath, string.Format(AbiFileTemplate, name));
		var fileInfo = new FileInfo(filePath);
		await using FileStream fs = fileInfo.OpenRead();
		var abiContract =
			await JsonSerializer.DeserializeAsync<AbiContract>(fs, JsonOptionsProvider.JsonSerializerOptions, cancellationToken);

		return new Abi.Contract { Value = abiContract };
	}

	/// <inheritdoc />
	public async Task<string> LoadTvc(string name, CancellationToken cancellationToken = default) {
		string filePath = Path.Join(_options.PackagesPath, string.Format(TvcFileTemplate, name));
		byte[] bytes = await File.ReadAllBytesAsync(filePath, cancellationToken);
		return Convert.ToBase64String(bytes);
	}
}
