﻿namespace EverscaleNet.Client.PackageManager;

/// <summary>
///     Setup path to contracts folder
/// </summary>
public class FilePackageManagerOptions {
	/// <summary>
	/// Path that will be used to load packages. Default is "_contracts"
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
