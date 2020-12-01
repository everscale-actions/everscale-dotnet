using System.Collections.Generic;

namespace ch1seL.TonNet.Debot.Models
{
    public class DebotStep
    {
        public byte Choice { get; set; }
        public Queue<string> Inputs { get; set; } = new Queue<string>();
        public Queue<string> Outputs { get; set; } = new Queue<string>();
        public Queue<DebotStep> Invokes { get; set; } = new Queue<DebotStep>();
    }
}