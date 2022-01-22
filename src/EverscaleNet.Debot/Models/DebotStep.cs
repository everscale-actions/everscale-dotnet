using System;
using System.Collections.Generic;

namespace EverscaleNet.Debot.Models;

public class DebotStep {
	public DebotStep(byte choice = 0, IEnumerable<string> inputs = null, IEnumerable<string> outputs = null, IEnumerable<DebotStep[]> invokes = null) {
		Choice = choice;
		Inputs = new Queue<string>(inputs ?? Array.Empty<string>());
		Outputs = new Queue<string>(outputs ?? Array.Empty<string>());
		Invokes = new Queue<DebotStep[]>(invokes ?? Array.Empty<DebotStep[]>());
	}

	public byte Choice { get; set; }
	public Queue<string> Inputs { get; set; }
	public Queue<string> Outputs { get; set; }
	public Queue<DebotStep[]> Invokes { get; set; }
}