using EverscaleNet.Abstract;
using EverscaleNet.Adapter.Rust;
using EverscaleNet.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EverscaleNet.RustAdapter.Tests;

internal static class TestsHelpers {
	public static IEverClientAdapter CreateRustAdapter(ILogger<EverClientRustAdapter> logger) {
		return new EverClientRustAdapter(Options.Create(new EverClientOptions()), logger);
	}
}
