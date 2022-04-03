export async function init(dotnetAdapter) {
  const eversdk = await import('./eversdk.js');
  eversdk.libWebSetup({debugLog: console.debug, binaryURL: '/_content/EverscaleNet.Adapter.Wasm/eversdk.wasm'});

  const libWeb = await eversdk.libWeb();
  libWeb.setResponseParamsHandler((requestId, params, responseType, finished) => {
    dotnetAdapter.invokeMethod('ResponseHandler', requestId, JSON.stringify(params), responseType, finished);
  });
  libWeb.sendRequest = (context, requestId, functionName, functionParamsJson) => {
    libWeb.sendRequestParams(context, requestId, functionName, JSON.parse(functionParamsJson))
  }
  return libWeb;
}
