namespace EverscaleNet;

/// <summary>
///     Get contract abi, tvc, keypair or code
/// </summary>
public abstract class PackageManagerOptions {
	/// <summary>
	///     Path that will be used to load packages. Default is "_contracts". Blazor WASM will reflect to wwwroot/_contracts
	/// </summary>
	public string PackagesPath { get; set; } = "_contracts";

	/// <summary>
	///     Abi file name template
	/// </summary>
	public string AbiFileTemplate { get; set; } = "{0}.abi.json";

	/// <summary>
	///     Tvc file name template
	/// </summary>
	public string TvcFileTemplate { get; set; } = "{0}.tvc";

	/// <summary>
	///     Tvc file name template
	/// </summary>
	public string KeyPairFileTemplate { get; set; } = "{0}.keys.json";

	/// <summary>
	///     Code file name template
	/// </summary>
	public string CodeFileTemplate { get; set; } = "{0}.code";
}
