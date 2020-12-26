using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace SampleWorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddTonClient(config =>
                    {
                        config.Network.Endpoints = new[] {"http://localhost"};
                        config.Network.WaitForTimeout = 5000;
                    });
                }).UseSerilog((context, configuration) =>
                {
                    configuration
                        .MinimumLevel.Verbose()
                        .Enrich.FromLogContext()
                        .WriteTo.Console();
                });
        }
    }
}