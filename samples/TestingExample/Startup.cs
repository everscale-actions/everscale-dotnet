using EverscaleNet.Abstract;
using EverscaleNet.Models;

namespace TestingExample;

public class Startup {
	public IHostBuilder CreateHostBuilder() {
		return new HostBuilder();
	}

	public void ConfigureHost(IHostBuilder hostBuilder) {
		hostBuilder
			.ConfigureLogging(builder => {
				builder.SetMinimumLevel(LogLevel.Trace);
				builder.AddXunitOutput();
			})
			.ConfigureServices(services => {
				services.AddSingleton<NodeSeDockerContainer>()
				        .AddHostedService<InitNodeSeService>();

				// todo: die to https://github.com/pengweiqhca/Xunit.DependencyInjection/issues/85
				// services.AddEverClient((sp, options) => {
				// 	string endpoint = sp.GetRequiredService<NodeSeDockerContainer>().Endpoint;
				// 	options.Network.Endpoints = new[] { endpoint };
				// });

				AddEverClientWithSerilog(services);

				services.AddSingleton<IEverGiver, GiverV3>();
			});
	}

	private static void AddEverClientWithSerilog(IServiceCollection services) {
		services.AddOptions();
		services.AddSingleton<IConfigureOptions<EverClientOptions>>(provider => new ConfigureOptions<EverClientOptions>(options => {
			        string endpoint = provider.GetRequiredService<NodeSeDockerContainer>().Endpoint;
			        options.Network.Endpoints = [endpoint];
		        }))
		        .AddSingleton<IEverClientAdapter>(provider => {
			        var optionsAccessor = provider.GetRequiredService<IOptions<EverClientOptions>>();
			        var output = provider.GetRequiredService<ITestOutputHelperAccessor>();
			        var loggerFactory = new LoggerFactory([
				        new SerilogLoggerProvider(new LoggerConfiguration()
				                                  .MinimumLevel.Verbose()
				                                  .WriteTo.TestOutput(output.Output)
				                                  .CreateLogger(), true)
			        ]);
			        return new EverClientRustAdapter(optionsAccessor, loggerFactory.CreateLogger<EverClientRustAdapter>());
		        })
		        .AddTransient<IEverClient, EverClient>()
		        .AddTransient<IEverPackageManager, FilePackageManager>();
	}
}
