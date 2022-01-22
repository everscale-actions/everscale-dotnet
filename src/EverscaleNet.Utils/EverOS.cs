namespace EverscaleNet.Utils;

public static class EverOS {
	public static class Endpoints {
		public static string[] Everscale => new[] {
			"https://eri01.main.everos.dev",
			"https://gra01.main.everos.dev",
			"https://gra02.main.everos.dev",
			"https://lim01.main.everos.dev",
			"https://rbx01.main.everos.dev"
		};

		public static string[] Development => new[] {
			"https://eri01.net.everos.dev/",
			"https://rbx01.net.everos.dev/",
			"https://gra01.net.everos.dev/"
		};

		public static string[] NodeSE => new[] {
			"http://localhost",
			"http://127.0.0.1",
			"http://0.0.0.0"
		};
	}
}