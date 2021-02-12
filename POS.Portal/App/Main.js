require.config({
    baseUrl: "/App",
    urlArgs: "v=1.0.0.1"
});

require(
    [
        "App",
        "Services/routeResolver",
        "Services/dataSource",
        "Services/contenteditable",
        "Services/convertToNumber",
        "Services/angucomplete-alt",
        "Services/loading-bar",
        "Services/resources",
        "Services/isteven-multi-select"
    ],
    function () {
        angular.bootstrap(document, ["POS"]);
    });
