using EverscaleNet.Client.Models;

namespace EverscaleNet.Utils;

/// <summary>
///     EverOS static
/// </summary>
public static class EverOS {
	/// <summary>
	///     Default givers info
	/// </summary>
	public static class Givers {
		/// <summary>
		///     SE Giver address and keys
		/// </summary>
		public static class SE {
			/// <summary>
			///     SE Giver address
			/// </summary>
			public static string Address = "0:78fbd6980c10cf41401b32e9b51810415e7578b52403af80dae68ddf99714498";
			/// <summary>
			///     SE Giver public and secret key pair
			/// </summary>
			public static KeyPair KeyPair = new() {
				Public = "2ada2e65ab8eeab09490e3521415f45b6e42df9c760a639bcf53957550b25a16",
				Secret = "172af540e43a524763dd53b26a066d472a97c4de37d5498170564510608250c3"
			};
		}
	}

	/// <summary>
	///     Endpoints static
	/// </summary>
	public static class Endpoints {
		/// <summary>
		///     Main network endpoints
		/// </summary>
		public static string[] Everscale => [
			"https://eri01.main.everos.dev",
			"https://gra01.main.everos.dev",
			"https://gra02.main.everos.dev",
			"https://lim01.main.everos.dev",
			"https://rbx01.main.everos.dev"
		];

		/// <summary>
		///     Dev network endpoints
		/// </summary>
		public static string[] Development => [
			"https://eri01.net.everos.dev/",
			"https://rbx01.net.everos.dev/",
			"https://gra01.net.everos.dev/"
		];

		/// <summary>
		///     Node SE network endpoints
		/// </summary>
		public static string[] NodeSE => [
			"http://localhost",
			"http://127.0.0.1",
			"http://0.0.0.0"
		];
	}
}
