using System.Collections.Generic;

namespace ch1seL.TonNet.Client.Tests.Modules.DebotModule
{
    public class DebotStep
    {
        public byte Choice { get; set; }
        public Queue<string> Inputs { get; init; } = new Queue<string>();
        public Queue<string> Outputs { get; init; } = new Queue<string>();
        public Queue<DebotStep> Invokes { get; init; } = new Queue<DebotStep>();
    }
}