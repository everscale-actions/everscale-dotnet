using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace ch1seL.TonNet.Client.Tests
{
    public abstract class TonClientTestsBase //: IDisposable todo: ??  freeze on linux WTF
    {
        private readonly ServiceProvider _serviceProvider;

        protected TonClientTestsBase(ITestOutputHelper testOutputHelper, bool localhostNode = false)
        {
            _serviceProvider = new ServiceCollection()
                .AddLogging(builder => builder.AddXUnit(testOutputHelper)
                    .AddFilter(level => level == LogLevel.Trace))
                .AddTonClient(config =>
                {
                    if (localhostNode) config.ServerAddress = "http://localhost:5555";
                })
                .BuildServiceProvider();

            TonClient = _serviceProvider.GetRequiredService<ITonClient>();
        }

        protected ITonClient TonClient { get; }

        public void Dispose()
        {
            _serviceProvider?.Dispose();
        }
    }
}