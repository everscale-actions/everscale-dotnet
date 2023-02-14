using System.Text.Json.Serialization;
using EverscaleNet.Client.Models;

// ReSharper disable UnusedMember.Global

namespace EverscaleNet.Models;

/// <summary>
///     Everscale client options. See details https://docs.everos.dev/ever-sdk/reference/types-and-methods/mod_client#config
/// </summary>
public class EverClientOptions {
	private static readonly BindingConfig DefaultBindingConfig = new() {
		Library = Static.BindingName,
		Version = Static.SdkVersion
	};

	/// <summary>
	///     https://docs.everos.dev/ever-sdk/reference/types-and-methods/mod_client#bindingconfig
	/// </summary>
	[JsonPropertyName("binding")]
	public BindingConfig Binding => DefaultBindingConfig;

	/// <summary>
	///     https://docs.everos.dev/ever-sdk/reference/types-and-methods/mod_client#networkconfig
	/// </summary>
	[JsonPropertyName("network")]
	public NetworkConfig Network { get; set; } = new();

	/// <summary>
	///     https://docs.everos.dev/ever-sdk/reference/types-and-methods/mod_client#cryptoconfig
	/// </summary>
	[JsonPropertyName("crypto")]
	public CryptoConfig Crypto { get; set; } = new();

	/// <summary>
	///     https://docs.everos.dev/ever-sdk/reference/types-and-methods/mod_client#abiconfig
	/// </summary>
	[JsonPropertyName("abi")]
	public AbiConfig Abi { get; set; } = new();

	/// <summary>
	///     https://docs.everos.dev/ever-sdk/reference/types-and-methods/mod_client#bocconfig
	/// </summary>
	[JsonPropertyName("boc")]
	public BocConfig Boc { get; set; } = new();

	/// <summary>
	///     https://docs.everos.dev/ever-sdk/reference/types-and-methods/mod_client#proofsconfig
	/// </summary>
	[JsonPropertyName("proofs")]
	public ProofsConfig Proofs { get; set; } = new();

	/// <summary>
	///     For file based storage is a folder name where SDK will store its data. For browser based is a browser async storage key prefix. Default (recommended) value is "~/.tonclient" for native environments and ".tonclient" for web-browser.
	/// </summary>
	[JsonPropertyName("local_storage_path")]
	public string? LocalStoragePath { get; set; }
}
