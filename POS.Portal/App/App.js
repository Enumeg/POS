"use strict";

define(["Services/routeResolver"], function () {

    var app = angular.module("POS", ["ngRoute", "ngCookies", "routeResolverServices", "ui.bootstrap", "angular-loading-bar", "angucomplete-alt", "isteven-multi-select", "toastr"]);
    app.run(function ($location, $rootScope, resource) {
        resource.fillAsync(function () {
            $rootScope.$on("$routeChangeSuccess", function (event, current) {
                var application = resource.getValue("Application");
                var page = resource.getValue(current.$$route.originalPath.split("/")[1]);
                if (application)
                    $rootScope.title = application ;
                if (page)
                    $rootScope.title += " - " + page;
            });
        });
    });


    app.config(["$routeProvider", "routeResolverProvider", "$controllerProvider", "$compileProvider", "$filterProvider", "$provide",
        function ($routeProvider, routeResolverProvider, $controllerProvider, $compileProvider, $filterProvider, $provide) {
            app.register =
            {
                controller: $controllerProvider.register,
                directive: $compileProvider.directive,
                filter: $filterProvider.register,
                factory: $provide.factory,
                service: $provide.service
            };

            var route = routeResolverProvider.route;

            $routeProvider
                .when("/Categories", route.resolve("Categories"))
                .when("/Products", route.resolve("Products"))
                .when("/Units", route.resolve("Coding", "Object"))
                .when("/Properties", route.resolve("Coding", "Object"))
                .when("/Banks", route.resolve("Coding", "Object"))
                .when("/People", route.resolve("Coding", "Person"))
                .when("/Points", route.resolve("Coding", "Points"))
                .when("/BankAccounts", route.resolve("Coding", "BankAccounts"))
                .when("/Purchases", route.resolve("Transactions", "Purchases"))
                .when("/Sales", route.resolve("Transactions", "Sales"))
                .otherwise({ redirectTo: "/Products" });

        }]);
    return app;
});





