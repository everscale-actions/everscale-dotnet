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
                new DebotStep {Choice = 1, Outputs = new Queue<string>(new[] {"Test Goto Action"})},
                new DebotStep {Choice = 1, Outputs = new Queue<string>(new[] {"Debot Tests"})},
                new DebotStep {Choice = 8}
            });

            await _debotTestBrowser.Execute(debotData.DebotAddr, debotData.Keys, steps);
        }

        [Fact]
        public async Task DebotPrint()
        {
            DebotData debotData = await _initDebotData.Value;

            var steps = new Queue<DebotStep>(new[]
            {
                new DebotStep {Choice = 2, Outputs = new Queue<string>(new[] {"Test Print Action", "test2: instant print", "test instant print"})},
                new DebotStep {Choice = 1, Outputs = new Queue<string>(new[] {"test simple print"})},
                new DebotStep {Choice = 2, Outputs = new Queue<string>(new[] {$"integer=1,addr={debotData.TargetAddr},string=test_string_1"})},
                new DebotStep {Choice = 3, Outputs = new Queue<string>(new[] {"Debot Tests"})},
                new DebotStep {Choice = 8}
            });

            await _debotTestBrowser.Execute(debotData.DebotAddr, debotData.Keys, steps);
        }

        [Fact]
        public async Task DebotRun()
        {
            DebotData debotData = await _initDebotData.Value;

            var steps = new Queue<DebotStep>(new[]
            {
                new DebotStep
                {
                    Choice = 3,
                    Inputs = new Queue<string>(new[] {"-1:1111111111111111111111111111111111111111111111111111111111111111"}),
                    Outputs = new Queue<string>(new[] {"Test Run Action", "test1: instant run 1", "test2: instant run 2"})
                },
                new DebotStep {Choice = 1, Inputs = new Queue<string>(new[] {"hello"})},
                new DebotStep
                {
                    Choice = 2,
                    Outputs = new Queue<string>(new[] {"integer=2,addr=-1:1111111111111111111111111111111111111111111111111111111111111111,string=hello"})
                },
                new DebotStep {Choice = 3, Outputs = new Queue<string>(new[] {"Debot Tests"})},
                new DebotStep {Choice = 8}
            });

            await _debotTestBrowser.Execute(debotData.DebotAddr, debotData.Keys, steps);
        }

        [Fact]
        public async Task DebotRunMethod()
        {
            DebotData debotData = await _initDebotData.Value;

            var steps = new Queue<DebotStep>(new[]
            {
                new DebotStep {Choice = 4, Outputs = new Queue<string>(new[] {"Test Run Method Action"})},
                new DebotStep {Choice = 1},
                new DebotStep {Choice = 2, Outputs = new Queue<string>(new[] {"data=64"})},
                new DebotStep {Choice = 3, Outputs = new Queue<string>(new[] {"Debot Tests"})},
                new DebotStep {Choice = 8}
            });

            await _debotTestBrowser.Execute(debotData.DebotAddr, debotData.Keys, steps);
        }

        [Fact]
        public async Task DebotSendMsg()
        {
            DebotData debotData = await _initDebotData.Value;

            var steps = new Queue<DebotStep>(new[]
            {
                new DebotStep {Choice = 5, Outputs = new Queue<string>(new[] {"Test Send Msg Action"})},
                new DebotStep {Choice = 1, Outputs = new Queue<string>(new[] {"Sending message {}", "Transaction succeeded."})},
                new DebotStep {Choice = 2},
                new DebotStep {Choice = 3, Outputs = new Queue<string>(new[] {"data=100"})},
                new DebotStep {Choice = 4, Outputs = new Queue<string>(new[] {"Debot Tests"})},
                new DebotStep {Choice = 8}
            });

            await _debotTestBrowser.Execute(debotData.DebotAddr, debotData.Keys, steps);
        }

        [Fact]
        public async Task DebotInvokeDebot()
        {
            DebotData debotData = await _initDebotData.Value;

            var steps = new Queue<DebotStep>(new[]
            {
                new DebotStep
                {
                    Choice = 6, Inputs = new Queue<string>(new[] {debotData.DebotAddr}),
                    Outputs = new Queue<string>(new[] {"Test Invoke Debot Action", "enter debot address:"})
                },
                new DebotStep
                {
                    Choice = 1,
                    Invokes = new Queue<DebotStep>(new[]
                        {new DebotStep {Choice = 1, Outputs = new Queue<string>(new[] {"Print test string", "Debot is invoked"})}})
                },
                new DebotStep {Choice = 2, Outputs = new Queue<string>(new[] {"Debot Tests"})},
                new DebotStep {Choice = 8}
            });

            await _debotTestBrowser.Execute(debotData.DebotAddr, debotData.Keys, steps);
        }
    }
}