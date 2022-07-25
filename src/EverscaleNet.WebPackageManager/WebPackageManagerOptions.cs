namespace EverscaleNet.WebPackageManager;

/// <summary>
/// </summary>
public class WebPackageManagerOptions {
	/// <summary>
	/// Path that will be used to load packages. Default is "_contracts". So _contracts will reflect to wwwroot/_contracts 
	/// </summary>
	public string PackagesPath { get; set; } = "_contracts";

	/// <summary>
	/// Package abi file name template (default: {0}.abi.json)
	/// </summary>
	public string AbiFileTemplate { get; set; } = "{0}.abi.json";

	/// <summary>
	/// Package tvc file name template (default: {0}.tvc)
	/// </summary>
	public string TvcFileTemplate { get; set; } = "{0}.tvc";
}
