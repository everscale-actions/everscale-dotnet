export async function init(dotnetAdapter) {
    const tonClient = await import('./tonclient.js');
    tonClient.libWebSetup({debugLog: console.debug, binaryURL: '/_content/ch1seL.TonNet.Adapter.Web/tonclient.wasm'});

    const libWeb = await tonClient.libWeb();
    libWeb.setResponseHandler((requestId, paramsJson, responseType, finished) => {
        dotnetAdapter.invokeMethod('ResponseHandler', requestId, paramsJson, responseType, finished);
    });
    return libWeb;
}
