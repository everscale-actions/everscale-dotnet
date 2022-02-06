namespace EverscaleNet.Client.PackageManager;

/// <summary>
///     Setup path to contracts folder
/// </summary>
public class FilePackageManagerOptions {
	/// <summary>
	///     Path that will be used to load packages. Default is "_contracts"
	/// </summary>
	public string PackagesPath { get; set; } = "_contracts";
}
