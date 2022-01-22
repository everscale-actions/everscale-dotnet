namespace EverscaleNet.Models;

public enum ResponseType {
	Success = 0,
	Error = 1,
	Nop = 2,
	AppRequest = 3,
	AppNotify = 4,
	Custom = 100
}