﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace EverscaleNet.Abstract;

/// <summary>
///     The most importantly interface. It provide all that needed to clients implementation
/// </summary>
public interface IEverClientAdapter : IAsyncDisposable {
	Task Request(string method, CancellationToken cancellationToken = default);

	Task<TResponse> Request<TResponse>(string method, CancellationToken cancellationToken = default)
		where TResponse : new();

	Task Request<TRequest>(string method, TRequest request, CancellationToken cancellationToken = default)
		where TRequest : new();

	Task<TResponse> Request<TRequest, TResponse>(string method, TRequest request,
	                                             CancellationToken cancellationToken = default)
		where TRequest : new()
		where TResponse : new();

	Task<TResponse> Request<TResponse, TEvent>(string method, Action<TEvent, uint> callback,
	                                           CancellationToken cancellationToken = default)
		where TResponse : new();

	Task<TResponse> Request<TRequest, TResponse, TEvent>(string method, TRequest request,
	                                                     Action<TEvent, uint> callback,
	                                                     CancellationToken cancellationToken = default)
		where TRequest : new()
		where TResponse : new();
}