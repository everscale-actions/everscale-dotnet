using System.Collections.Generic;
using EverscaleNet.Client.Models;

namespace EverscaleNet.Debot.Models;

#pragma warning disable CS1591

public class DebotCurrentStepData {
	public List<DebotAction> AvailableActions { get; set; } = new();
	public Queue<string> Outputs { get; } = new();
	public DebotStep Step { get; set; } = new();
}
