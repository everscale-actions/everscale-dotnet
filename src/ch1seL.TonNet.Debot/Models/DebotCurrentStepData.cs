using System.Collections.Generic;
using ch1seL.TonNet.Client.Models;

namespace ch1seL.TonNet.Debot.Models;

public class DebotCurrentStepData {
	public List<DebotAction> AvailableActions { get; set; } = new List<DebotAction>();
	public Queue<string> Outputs { get; } = new Queue<string>();
	public DebotStep Step { get; set; } = new DebotStep();
}