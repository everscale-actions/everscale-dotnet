export async function init(dotnetAdapter, options) {
  const eversdk = await import('./eversdk.js');
  console.debug("libWebSetup:", options);
  options.debugLog = console.debug;
  eversdk.libWebSetup(options);
  const libWeb = await eversdk.libWeb();
  libWeb.setResponseParamsHandler((requestId, params, responseType, finished) => {
    dotnetAdapter.invokeMethodAsync('ResponseHandler', requestId, JSON.stringify(params), responseType, finished);
  });
  libWeb.sendRequest = (context, requestId, functionName, functionParamsJson) => {
    libWeb.sendRequestParams(context, requestId, functionName, JSON.parse(functionParamsJson))
  }
  return libWeb;
}
