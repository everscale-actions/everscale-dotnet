using System;
using System.Threading.Tasks;
using ch1seL.TonNet.RustAdapter;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Serilog;
using Xunit;
using Xunit.Abstractions;

namespace ch1seL.TonNet.Client.Tests
{
    public class RustAdapterDisposingTests
    {
        private readonly ILogger<TonClientRustAdapter> _logger;

        public RustAdapterDisposingTests(ITestOutputHelper output)
        {
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddSerilog(new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.TestOutput(output)
                .CreateLogger()));
            _logger = loggerFactory.CreateLogger<TonClientRustAdapter>();
        }

        [Fact]
        public async Task TonClientDisposing()
        {
            var act = new Func<Task>(async () =>
            {
                var adapter = new TonClientRustAdapter(new TonClientOptions(), _logger);

                await new TonClient(adapter).Client.GetApiReference();

                adapter.Dispose();
            });

            await act.Should().NotThrowAsync();
        }
    }
}