using System.Reflection;
using System.Text.Json.Serialization;
using EverscaleNet.Client;
using EverscaleNet.Client.Models;

namespace EverscaleNet.Models;

/// <summary>
///     Everscale client options. See details https://tonlabs.gitbook.io/ever-sdk/guides/installation/configure_sdk#configure-client
/// </summary>
public class EverClientOptions {
	private static readonly BindingConfig DefaultBindingConfig = new() {
		Library = "everscale-actions/everscale-dotnet",
		Version = Assembly.GetAssembly(typeof(EverClient))!.GetName().Version?.ToString() ?? "Undefined"
	};

	/// <summary>
	/// Bindings telemetry
	/// </summary>
	[JsonPropertyName("binding")]
	public BindingConfig Binding { get; internal set; } = DefaultBindingConfig;

	/// <summary>
	///     https://tonlabs.gitbook.io/ever-sdk/guides/installation/configure_sdk#network-config
	///     see available endpoint URLs https://tonlabs.gitbook.io/ever-sdk/reference/ton-os-api/networks#networks
	/// </summary>
	[JsonPropertyName("network")]
	public NetworkConfig Network { get; set; } = new();

	/// <summary>
	///     https://tonlabs.gitbook.io/ever-sdk/guides/installation/configure_sdk#crypto-config
	/// </summary>
	[JsonPropertyName("crypto")]
	public CryptoConfig Crypto { get; set; } = new();

	/// <summary>
	///     https://tonlabs.gitbook.io/ever-sdk/guides/installation/configure_sdk#abi-config
	/// </summary>
	[JsonPropertyName("abi")]
	public AbiConfig Abi { get; set; } = new();
}
