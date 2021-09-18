(async () => {
    const module = await import('./tonclient.js');
    module.libWebSetup({debugLog: console.debug});
    window.jsAdapter = await module.libWeb();
})();

window.setAdapterResponseHandler = (dotnetAdapter) => {
    window.jsAdapter.setResponseHandler((requestId, paramsJson, responseType, finished) => {
        dotnetAdapter.invokeMethod('ResponseHandler', requestId, paramsJson, responseType, finished);
    });
}
