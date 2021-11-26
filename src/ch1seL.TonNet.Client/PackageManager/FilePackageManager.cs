using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Models;
using ch1seL.TonNet.Serialization;
using Microsoft.Extensions.Options;

namespace ch1seL.TonNet.Client.PackageManager; 

public class FilePackageManager : ITonPackageManager {
	private const string AbiFileTemplate = "{0}.abi.json";
	private const string TvcFileTemplate = "{0}.tvc";
	private readonly FilePackageManagerOptions _options;

	public FilePackageManager(IOptions<FilePackageManagerOptions> optionsAccessor) {
		_options = optionsAccessor.Value;
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
		string filePath = Path.Join(_options.PackagesPath, string.Format(AbiFileTemplate, name));
		var fileInfo = new FileInfo(filePath);
		await using FileStream fs = fileInfo.OpenRead();
		var abiContract =
			await JsonSerializer.DeserializeAsync<AbiContract>(fs, JsonOptionsProvider.JsonSerializerOptions);

		return new Abi.Contract { Value = abiContract };
	}

	public async Task<string> LoadTvc(string name) {
		string filePath = Path.Join(_options.PackagesPath, string.Format(TvcFileTemplate, name));
		byte[] bytes = await File.ReadAllBytesAsync(filePath);
		return Convert.ToBase64String(bytes);
	}
}