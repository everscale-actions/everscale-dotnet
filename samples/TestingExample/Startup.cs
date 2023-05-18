using EverscaleNet.TestSuite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit.DependencyInjection.Logging;

namespace TestingExample;

public class Startup {
	public void ConfigureHost(IHostBuilder hostBuilder) {
		hostBuilder
			.ConfigureLogging(builder => {
				builder.SetMinimumLevel(LogLevel.Trace);
				builder.AddXunitOutput();
			})
			.ConfigureServices(services => {
				services.AddEverClient((sp, options) => {
					string endpoint = sp.GetRequiredService<NodeSeDockerContainer>().Endpoint;
					options.Network.Endpoints = new[] { endpoint };
				});

				services.AddSingleton<NodeSeDockerContainer>()
				        .AddHostedService<InitNodeSeService>();

				services.AddSingleton<IEverGiver, EverGiverV3>();
			});
	}
}
