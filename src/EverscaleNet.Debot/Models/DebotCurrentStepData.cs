using System.Collections.Generic;
using EverscaleNet.Client.Models;

namespace EverscaleNet.Debot.Models;

public class DebotCurrentStepData {
	public List<DebotAction> AvailableActions { get; set; } = new();
	public Queue<string> Outputs { get; } = new();
	public DebotStep Step { get; set; } = new();
}