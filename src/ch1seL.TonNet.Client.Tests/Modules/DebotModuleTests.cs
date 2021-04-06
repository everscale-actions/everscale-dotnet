using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ch1seL.TonNet.Debot;
using ch1seL.TonNet.Debot.Models;
using Xunit;
using Xunit.Abstractions;

namespace ch1seL.TonNet.Client.Tests.Modules
{
    public class DebotModuleTests : IClassFixture<DebotTestsFixture>
    {
        private readonly IDebotBrowser _debotTestBrowser;
        private readonly Lazy<Task<DebotData>> _initDebotData;

        public DebotModuleTests(DebotTestsFixture fixture, ITestOutputHelper outputHelper)
        {
            (var initDebotData, IDebotBrowser debotTestBrowser) = fixture.Init(outputHelper, true);
            _initDebotData = initDebotData;
            _debotTestBrowser = debotTestBrowser;
        }

        [Fact]
        public async Task DebotGoto()
        {
            DebotData debotData = await _initDebotData.Value;

            var steps = new Queue<DebotStep>(new[]
            {
                new DebotStep(1, outputs: new[] {"Test Goto Action"}),
                new DebotStep(1, outputs: new[] {"Debot Tests"}),
                new DebotStep(8)
            });

            await _debotTestBrowser.Execute(debotData.DebotAddr, debotData.Keys, steps);
        }

        [Fact]
        public async Task DebotPrint()
        {
            DebotData debotData = await _initDebotData.Value;

            var steps = new Queue<DebotStep>(new[]
            {
                new DebotStep(2, outputs: new[] {"Test Print Action", "test2: instant print", "test instant print"}),
                new DebotStep(1, outputs: new[] {"test simple print"}),
                new DebotStep(2, outputs: new[] {$"integer=1,addr={debotData.TargetAddr},string=test_string_1"}),
                new DebotStep(3, outputs: new[] {"Debot Tests"}),
                new DebotStep(8)
            });

            await _debotTestBrowser.Execute(debotData.DebotAddr, debotData.Keys, steps);
        }

        [Fact]
        public async Task DebotRunAct()
        {
            DebotData debotData = await _initDebotData.Value;

            var steps = new Queue<DebotStep>(new[]
            {
                new DebotStep(3, outputs: new[] {"Test Run Action"}),
                new DebotStep(1, new[] {"-1:1111111111111111111111111111111111111111111111111111111111111111"},
                    new[] {"Test Instant Run", "test1: instant run 1", "test2: instant run 2"}),
                new DebotStep(1, outputs: new[] {"Test Run Action"}),
                new DebotStep(2, new[] {"hello"}),
                new DebotStep(3, outputs: new[] {"integer=2,addr=-1:1111111111111111111111111111111111111111111111111111111111111111,string=hello"}),
                new DebotStep(4, outputs: new[] {"Debot Tests"}),
                new DebotStep(8)
            });

            await _debotTestBrowser.Execute(debotData.DebotAddr, debotData.Keys, steps);
        }

        [Fact]
        public async Task DebotRunMethod()
        {
            DebotData debotData = await _initDebotData.Value;

            var steps = new Queue<DebotStep>(new[]
            {
                new DebotStep(4, outputs: new[] {"Test Run Method Action"}),
                new DebotStep(1),
                new DebotStep(2, outputs: new[] {"data=64"}),
                new DebotStep(3, outputs: new[] {"Debot Tests"}),
                new DebotStep(8)
            });

            await _debotTestBrowser.Execute(debotData.DebotAddr, debotData.Keys, steps);
        }

        [Fact]
        public async Task DebotSendMsg()
        {
            DebotData debotData = await _initDebotData.Value;

            var steps = new Queue<DebotStep>(new[]
            {
                new DebotStep(5, outputs: new[] {"Test Send Msg Action"}),
                new DebotStep(1, outputs: new[] {"Sending message {}", "Transaction succeeded."}),
                new DebotStep(2),
                new DebotStep(3, outputs: new[] {"data=100"}),
                new DebotStep(4, outputs: new[] {"Debot Tests"}),
                new DebotStep(8)
            });

            await _debotTestBrowser.Execute(debotData.DebotAddr, debotData.Keys, steps);
        }

        [Fact]
        public async Task DebotInvokeDebot()
        {
            DebotData debotData = await _initDebotData.Value;

            var steps = new Queue<DebotStep>(new[]
            {
                new DebotStep(6, new[] {debotData.DebotAddr}, new[]
                {
                    "Test Invoke Debot Action", "enter debot address:"
                }),
                new DebotStep(1, new[] {debotData.DebotAddr}, new[]
                    {
                        "Test Invoke Debot Action", "enter debot address:"
                    },
                    new[]
                    {
                        new[]
                        {
                            new DebotStep(1, outputs: new[] {"Print test string", "Debot is invoked"}),
                            new DebotStep(1, outputs: new[] {"Sending message {}", "Transaction succeeded."})
                        }
                    }),
                new DebotStep(2, outputs: new[] {"Debot Tests"}),
                new DebotStep(8)
            });

            await _debotTestBrowser.Execute(debotData.DebotAddr, debotData.Keys, steps);
        }

        [Fact]
        public async Task DebotEngineCall()
        {
            DebotData debotData = await _initDebotData.Value;

            var steps = new Queue<DebotStep>(new[]
            {
                new DebotStep(7, outputs: new[] {"Test Engine Calls"}),
                new DebotStep(1),
                new DebotStep(2),
                //todo: doesn't work for me https://t.me/ton_sdk/5363
                //new DebotStep (3),
                new DebotStep(4),
                new DebotStep(5),
                new DebotStep(6, outputs: new[] {"Debot Tests"}),
                new DebotStep(8)
            });

            await _debotTestBrowser.Execute(debotData.DebotAddr, debotData.Keys, steps);
        }
    }
}