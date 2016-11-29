require.config({
    baseUrl: '/App',
    urlArgs: 'v=1.0'
});

require(
    [
        'App',
        'Services/routeResolver',        
        'Services/dataSource',
        'Services/contenteditable',
        'Services/angucomplete',
        'Services/loading-bar',
        'Services/resources',
        'Services/uiHeaderService',
        'Services/isteven-multi-select',
        'Controllers/uiHeaderController'        
    ],
    function () {
        angular.bootstrap(document, ['POS']);
    });
