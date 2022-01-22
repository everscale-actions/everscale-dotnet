using System.Collections.Generic;
using EverscaleNet.Client.Models;

namespace EverscaleNet.Debot.Models;

public class ExpectedTransaction {
	public string Dst { get; set; }
	public List<Spending> Out { get; set; }
	public bool SetCode { get; set; }
	public string SignKey { get; set; }
	public bool Approved { get; set; }
}