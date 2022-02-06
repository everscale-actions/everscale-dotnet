using System;
using System.Collections.Generic;
using EverscaleNet.Client.Models;

#pragma warning disable CS1591

namespace EverscaleNet.Debot.Models;

public class DebotBrowserData {
	private readonly object _lock = new();

	public DebotCurrentStepData Current { get; set; }

	public Queue<DebotStep> Next { get; set; } = new();

	public KeyPair Keys { get; set; }

	public string Address { get; set; }

	public bool Finished { get; set; }

	public bool SwitchStarted { get; set; }
	public DebotInfo Info { get; set; }

	public Queue<ExpectedTransaction> Activity { get; set; } = new();

	public void ActionWithLock(Action<DebotBrowserData> actionWithLock) {
		lock (_lock) {
			actionWithLock.Invoke(this);
		}
	}

	public T FuncWithLock<T>(Func<DebotBrowserData, T> funcWithLock) {
		lock (_lock) {
			return funcWithLock.Invoke(this);
		}
	}
}
