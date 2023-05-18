namespace EverscaleNet.Abstract;

/// <summary>
///     Provide core methods used by client
/// </summary>
public interface IEverClientAdapter : IAsyncDisposable {
	/// <summary>
	///     Call core client method
	/// </summary>
	/// <param name="method">Method name</param>
	/// <param name="cancellationToken"></param>
	Task Request(string method, CancellationToken cancellationToken = default);

	/// <summary>
	///     Call core client method and return result
	/// </summary>
	/// <param name="method">Method name</param>
	/// <param name="cancellationToken"></param>
	/// <typeparam name="TResponse">Response type</typeparam>
	/// <returns></returns>
	Task<TResponse> Request<TResponse>(string method, CancellationToken cancellationToken = default)
		where TResponse : new();

	/// <summary>
	/// </summary>
	/// <param name="method">Method name</param>
	/// <param name="request">Request</param>
	/// <param name="cancellationToken"></param>
	/// <typeparam name="TRequest">Request type</typeparam>
	/// <returns></returns>
	Task Request<TRequest>(string method, TRequest request, CancellationToken cancellationToken = default)
		where TRequest : new();

	/// <summary>
	/// </summary>
	/// <param name="method">Method name</param>
	/// <param name="request">Request</param>
	/// <param name="cancellationToken"></param>
	/// <typeparam name="TRequest">Request type</typeparam>
	/// <typeparam name="TResponse">Response type</typeparam>
	/// <returns></returns>
	Task<TResponse> Request<TRequest, TResponse>(string method, TRequest request,
	                                             CancellationToken cancellationToken = default)
		where TRequest : new()
		where TResponse : new();

	/// <summary>
	/// </summary>
	/// <param name="method">Method name</param>
	/// <param name="callback"></param>
	/// <param name="cancellationToken"></param>
	/// <typeparam name="TResponse">Response type</typeparam>
	/// <typeparam name="TEvent"></typeparam>
	/// <returns></returns>
	Task<TResponse> Request<TResponse, TEvent>(string method, Func<TEvent, uint, CancellationToken, Task>? callback,
	                                           CancellationToken cancellationToken = default)
		where TResponse : new();

	/// <summary>
	/// </summary>
	/// <param name="method">Method name</param>
	/// <param name="request">Request</param>
	/// <param name="callback"></param>
	/// <param name="cancellationToken"></param>
	/// <typeparam name="TRequest">Request type</typeparam>
	/// <typeparam name="TResponse">Response type</typeparam>
	/// <typeparam name="TEvent"></typeparam>
	/// <returns></returns>
	Task<TResponse> Request<TRequest, TResponse, TEvent>(string method, TRequest request,
	                                                     Func<TEvent, uint, CancellationToken, Task>? callback,
	                                                     CancellationToken cancellationToken = default)
		where TRequest : new()
		where TResponse : new();
}
