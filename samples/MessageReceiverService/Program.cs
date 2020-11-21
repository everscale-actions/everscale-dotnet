using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Events;

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
                        config.ServerAddress = "http://localhost";
                        config.WaitForTimeout = 5000;
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