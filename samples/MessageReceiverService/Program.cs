using MessageReceiverService;
using Serilog;

IHostBuilder builder = Host.CreateDefaultBuilder(args);
builder.ConfigureServices((_, services) => {
	services.AddHostedService<Worker>();
	services.AddEverClient(config => {
		config.Network.Endpoints = ["http://localhost"];
		config.Network.WaitForTimeout = 5000;
	});
});
builder.UseSerilog((_, configuration) => {
	configuration
		.MinimumLevel.Verbose()
		.Enrich.FromLogContext()
		.WriteTo.Console();
});

IHost host = builder.Build();
host.Run();
