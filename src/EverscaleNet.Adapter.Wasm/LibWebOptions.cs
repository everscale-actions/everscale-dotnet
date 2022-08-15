using System.Text.Json.Serialization;

namespace EverscaleNet.Adapter.Wasm;

/// <summary>
/// Setup core ever sdk js lib options
/// https://github.com/tonlabs/ever-sdk-js#setup-library
/// </summary>
public class LibWebOptions {
	/// <summary>
	/// By default, lib web starts a separate worker that will utilize core (wasm).
	/// So main thread never freezes – it is fine for UI. But in some cases (e.g. when worker already exists in application or extension) separate worker is a bad approach.
	/// </summary>
	[JsonPropertyName("disableSeparateWorker")]
	public bool DisableSeparateWorker { get; set; }

	/// <summary>
	/// Set custom path to eversdk.wasm binary
	/// Default: "/_content/EverscaleNet.Adapter.Wasm/eversdk.wasm"
	/// </summary>
	[JsonPropertyName("binaryURL")]
	public string BinaryUrl { get; set; } = "/_content/EverscaleNet.Adapter.Wasm/eversdk.wasm";
}
