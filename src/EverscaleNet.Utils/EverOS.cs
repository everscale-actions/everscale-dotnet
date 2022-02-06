namespace EverscaleNet.Utils;

/// <summary>
///     EverOS static
/// </summary>
public static class EverOS {
	/// <summary>
	///     Endpoints static
	/// </summary>
	public static class Endpoints {
		/// <summary>
		///     Main network endpoints
		/// </summary>
		public static string[] Everscale => new[] {
			"https://eri01.main.everos.dev",
			"https://gra01.main.everos.dev",
			"https://gra02.main.everos.dev",
			"https://lim01.main.everos.dev",
			"https://rbx01.main.everos.dev"
		};

		/// <summary>
		///     Dev network endpoints
		/// </summary>
		public static string[] Development => new[] {
			"https://eri01.net.everos.dev/",
			"https://rbx01.net.everos.dev/",
			"https://gra01.net.everos.dev/"
		};

		/// <summary>
		///     Node SE network endpoints
		/// </summary>
		public static string[] NodeSE => new[] {
			"http://localhost",
			"http://127.0.0.1",
			"http://0.0.0.0"
		};
	}
}
