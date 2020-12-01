using System;
using System.Collections.Generic;
using ch1seL.TonNet.Client.Models;

namespace ch1seL.TonNet.Debot.Models
{
    public class DebotCurrentStepData
    {
        private readonly object _lock = new object();

        public List<DebotAction> AvailableActions { get; set; } = new List<DebotAction>();
        public Queue<string> Outputs { get; } = new Queue<string>();
        public DebotStep Step { get; set; } = new DebotStep();

        public void ActionWithLock(Action<DebotCurrentStepData> actionWithLock)
        {
            lock (_lock)
            {
                actionWithLock.Invoke(this);
            }
        }

        public T FuncWithLock<T>(Func<DebotCurrentStepData, T> funcWithLock)
        {
            lock (_lock)
            {
                return funcWithLock.Invoke(this);
            }
        }
    }
}