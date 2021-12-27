export async function init(dotnetAdapter) {
    const tonClient = await import('./tonclient.js');
    tonClient.libWebSetup({debugLog: console.debug, binaryURL: '/_content/ch1seL.TonNet.Adapter.Web/tonclient.wasm'});

    const libWeb = await tonClient.libWeb();
    libWeb.setResponseParamsHandler((requestId, params, responseType, finished) => {
        dotnetAdapter.invokeMethod('ResponseHandler', requestId, JSON.stringify(params), responseType, finished);
    });
    libWeb.sendRequest = (context, requestId, functionName, functionParamsJson) => {
        libWeb.sendRequestParams(context, requestId, functionName, JSON.parse(functionParamsJson))
    }
    return libWeb;
}
