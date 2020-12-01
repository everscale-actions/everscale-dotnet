using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ch1seL.TonNet.Abstract;
using ch1seL.TonNet.Client.Models;
using ch1seL.TonNet.Debot;
using ch1seL.TonNet.Debot.Models;
using FluentAssertions;

namespace ch1seL.TonNet.Client.Tests.Modules.Debot
{
    internal class DebotTestsBrowser : DefaultDebotBrowser
    {
        private readonly ITonClient _tonClient;

        public DebotTestsBrowser(ITonClient tonClient) : base(tonClient)
        {
            _tonClient = tonClient;
        }

        protected override async Task ExecuteFromState(DebotBrowserData state, Func<Action<JsonElement, uint>, Task<RegisteredDebot>> debotFunction)
        {
            RegisteredDebot registeredDebot = null;
            try
            {
                registeredDebot = await debotFunction(async (@params, responseType) => await Callback(state, @params, (ResponseType) responseType));
                while (!state.Finished)
                {
                    DebotAction action = null;

                    state.ActionWithLock(data =>
                    {
                        DebotCurrentStepData step = data.Current;
                        step.Step = state.Next.Dequeue();
                        step.Outputs.Clear();
                        action = step.AvailableActions[step.Step.Choice - 1];
                    });

                    await _tonClient.Debot.Execute(new ParamsOfExecute
                    {
                        Action = action,
                        DebotHandle = registeredDebot.DebotHandle
                    });

                    state.Current.ActionWithLock(step =>
                    {
                        step.Outputs.Count.Should().Be(step.Step.Outputs.Count);

                        foreach (var (outs0, outs1) in step.Outputs
                            .Zip(step.Step.Outputs, Tuple.Create))
                        {
                            var pos = outs1.LastIndexOf("{}", StringComparison.Ordinal);
                            if (pos >= 0)
                                outs0.Substring(0, pos).Should().Be(outs1.Substring(0, pos));
                            else
                                outs0.Should().BeEquivalentTo(outs1);
                        }

                        step.Step.Inputs.Count.Should().Be(0);
                        step.Step.Invokes.Count.Should().Be(0);
                    });

                    if (state.Current.AvailableActions.Count == 0) break;
                }

                state.ActionWithLock(stateDate => { stateDate.Next.Count.Should().Be(0); });
            }
            finally
            {
                if (registeredDebot != null) await _tonClient.Debot.Remove(registeredDebot);
            }
        }
    }
}