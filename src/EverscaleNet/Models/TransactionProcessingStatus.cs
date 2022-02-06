﻿#pragma warning disable CS1591
namespace EverscaleNet.Models;

public enum TransactionProcessingStatus {
	Unknown = 0,
	Preliminary = 1,
	Proposed = 2,
	Finalized = 3,
	Refused = 4
}
