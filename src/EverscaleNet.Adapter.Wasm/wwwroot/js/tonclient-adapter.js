export async function init(dotnetAdapter) {
    const tonClient = await import('./tonclient.js');
    tonClient.libWebSetup({debugLog: console.debug, binaryURL: '/_content/EverscaleNet.Adapter.Wasm/tonclient.wasm'});

    const libWeb = await tonClient.libWeb();
    libWeb.setResponseParamsHandler((requestId, params, responseType, finished) => {
        dotnetAdapter.invokeMethod('ResponseHandler', requestId, JSON.stringify(params), responseType, finished);
    });
    libWeb.sendRequest = (context, requestId, functionName, functionParamsJson) => {
        libWeb.sendRequestParams(context, requestId, functionName, JSON.parse(functionParamsJson))
    }
    return libWeb;
}
