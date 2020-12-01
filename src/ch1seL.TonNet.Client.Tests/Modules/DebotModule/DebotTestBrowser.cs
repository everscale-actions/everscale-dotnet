using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ch1seL.TonNet.Client.Models;
using ch1seL.TonNet.Serialization;
using FluentAssertions;

namespace ch1seL.TonNet.Client.Tests.Modules.DebotModule
{
    public class DebotTestBrowser
    {
        private readonly ITonClient _tonClient;

        public DebotTestBrowser(ITonClient tonClient)
        {
            _tonClient = tonClient;
        }

        public async Task Execute(string address, KeyPair keys, Queue<DebotStep> steps)
        {
            var state = new DebotBrowserData
            {
                Current = new DebotCurrentStepData(),
                Next = steps,
                Keys = keys,
                Address = address,
                Finished = false
            };

            await ExecuteFromState(state, callback => _tonClient.Debot.Start(new ParamsOfStart {Address = address}, callback));
        }

        private async Task ExecuteFromState(DebotBrowserData state, Func<Action<JsonElement, uint>, Task<RegisteredDebot>> debotFunction)
        {
            var callback = new Action<JsonElement, uint>(async (@params, responseType) =>
            {
                ParamsOfAppDebotBrowser paramsOfAppDebotBrowser;
                switch ((ResponseType) responseType)
                {
                    case ResponseType.AppRequest:
                        var paramsOfAppRequest = @params.ToObject<ParamsOfAppRequest>();
                        paramsOfAppDebotBrowser = PolymorphicSerializer.Deserialize<ParamsOfAppDebotBrowser>(paramsOfAppRequest.RequestData!.Value);
                        ResultOfAppDebotBrowser result = await ProcessCall(state, paramsOfAppDebotBrowser);
                        await _tonClient.Client.ResolveAppRequest(new ParamsOfResolveAppRequest
                        {
                            AppRequestId = paramsOfAppRequest.AppRequestId,
                            Result = new AppRequestResult.Ok {Result = result.ToJsonElement()}
                        });
                        break;
                    case ResponseType.AppNotify:
                        paramsOfAppDebotBrowser = PolymorphicSerializer.Deserialize<ParamsOfAppDebotBrowser>(@params);
                        await ProcessNotification(state, paramsOfAppDebotBrowser);
                        break;
                }
            });

            RegisteredDebot registeredDebot = null;
            try
            {
                registeredDebot = await debotFunction(callback);
                var @break = false;

                while (!state.Finished && !@break)
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

                        if (step.AvailableActions.Count == 0) @break = true;
                    });
                }

                state.ActionWithLock(stateDate => { stateDate.Next.Count.Should().Be(0); });
            }
            finally
            {
                if (registeredDebot != null) await _tonClient.Debot.Remove(registeredDebot);
            }
        }

        private async Task<ResultOfAppDebotBrowser> ProcessCall(DebotBrowserData state, ParamsOfAppDebotBrowser @params)
        {
            switch (@params)
            {
                case ParamsOfAppDebotBrowser.Input:
                    string value;
                    value = state.Current.FuncWithLock(data => data.Step.Inputs.Dequeue());
                    return new ResultOfAppDebotBrowser.Input
                    {
                        Value = value
                    };
                case ParamsOfAppDebotBrowser.GetSigningBox:
                    RegisteredSigningBox signingBox = await _tonClient.Crypto.GetSigningBox(state.Keys);
                    return new ResultOfAppDebotBrowser.GetSigningBox {SigningBox = signingBox.Handle};
                case ParamsOfAppDebotBrowser.InvokeDebot paramsOfAppDebotBrowser:
                    DebotStep invokeStep = null;
                    state.Current.ActionWithLock(step =>
                    {
                        step.Step.Invokes.Peek().Choice = 1;
                        invokeStep = step.Step.Invokes.Dequeue();
                    });

                    var current = new DebotCurrentStepData {AvailableActions = new List<DebotAction> {paramsOfAppDebotBrowser.Action}};

                    var fetchState = new DebotBrowserData
                    {
                        Current = current,
                        Next = new Queue<DebotStep>(new[] {invokeStep}),
                        Keys = state.Keys,
                        Address = paramsOfAppDebotBrowser.DebotAddr,
                        Finished = false
                    };
                    await ExecuteFromState(fetchState, callback => _tonClient.Debot.Fetch(new ParamsOfFetch {Address = state.Address}, callback));
                    return new ResultOfAppDebotBrowser.InvokeDebot();

                default:
                    throw new ArgumentOutOfRangeException($"Invalid call: {JsonSerializer.Serialize(@params)}");
            }
        }

        private static async Task ProcessNotification(DebotBrowserData state, ParamsOfAppDebotBrowser @params)
        {
            switch (@params)
            {
                case ParamsOfAppDebotBrowser.Log log:
                    var msg = log.Msg;
                    state.Current.ActionWithLock(data => data.Outputs.Enqueue(msg));
                    break;
                case ParamsOfAppDebotBrowser.Switch @switch:
                    var contextId = @switch.ContextId;
                    if (contextId == (byte) DebotStates.StateExit) state.Finished = true;
                    state.Current.ActionWithLock(data => data.AvailableActions.Clear());
                    break;
                case ParamsOfAppDebotBrowser.ShowAction showAction:
                    state.Current.ActionWithLock(data => data.AvailableActions.Add(showAction.Action));
                    break;
                default:
                    throw new ArgumentException($"invalid notification {JsonSerializer.Serialize(@params)}");
            }

            await Task.CompletedTask;
        }
    }
}