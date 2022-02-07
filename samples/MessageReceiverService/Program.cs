using Serilog;

namespace MessageReceiverService;

public static class Program {
	public static void Main(string[] args) {
		CreateHostBuilder(args).Build().Run();
	}

	private static IHostBuilder CreateHostBuilder(string[] args) {
		return Host.CreateDefaultBuilder(args)
		           .ConfigureServices((_, services) => {
			           services.AddHostedService<Worker>();
			           services.AddEverClient(config => {
				           config.Network.Endpoints = new[] { "http://localhost" };
				           config.Network.WaitForTimeout = 5000;
			           });
		           }).UseSerilog((_, configuration) => {
			           configuration
				           .MinimumLevel.Verbose()
				           .Enrich.FromLogContext()
				           .WriteTo.Console();
		           });
	}
}
