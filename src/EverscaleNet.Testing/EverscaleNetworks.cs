using EverscaleNet.Client.Models;

namespace EverscaleNet.Testing;

/// <summary>
/// 
/// </summary>
public static class EverscaleNetworks {
	/// <summary>
	/// 
	/// </summary>
	public static readonly IEverscaleNetwork Dev = new EverscaleDevNetwork();
	
	/// <summary>
	/// 
	/// </summary>
	public static readonly IEverscaleNetwork Se = new EverscaleSeNetwork();

	private class EverscaleDevNetwork : IEverscaleNetwork {
		public string[] Endpoints { get; } = {
			"https://eri01.net.everos.dev/",
			"https://rbx01.net.everos.dev/",
			"https://gra01.net.everos.dev/"
		};
		public KeyPair GiverKeyPair { get; } = new() {
			Public = "2ada2e65ab8eeab09490e3521415f45b6e42df9c760a639bcf53957550b25a16",
			Secret = "172af540e43a524763dd53b26a066d472a97c4de37d5498170564510608250c3"
		};
	}

	private class EverscaleSeNetwork : IEverscaleNetwork {
		public string[] Endpoints { get; } = {
			"http://localhost/",
			"http://127.0.0.1/"
		};
		public KeyPair GiverKeyPair { get; } = new() {
			Public = "2ada2e65ab8eeab09490e3521415f45b6e42df9c760a639bcf53957550b25a16",
			Secret = "172af540e43a524763dd53b26a066d472a97c4de37d5498170564510608250c3"
		};
	}
}
