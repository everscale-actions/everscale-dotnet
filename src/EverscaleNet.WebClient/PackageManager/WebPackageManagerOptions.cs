namespace EverscaleNet.WebClient.PackageManager;

/// <summary>
/// </summary>
public class WebPackageManagerOptions {
	/// <summary>
	///     Path that will be used to load packages. Default is "_contracts". So _contracts will reflect to wwwroot/_contracts
	/// </summary>
	public string PackagesPath { get; init; } = "_contracts";
}
