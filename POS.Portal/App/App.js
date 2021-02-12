"use strict";

define(["Services/routeResolver"], function () {

    var app = angular.module("POS", ["ngRoute", "ngCookies", "routeResolverServices", "ui.bootstrap", "angular-loading-bar", "angucomplete-alt", "isteven-multi-select", "toastr"]);
    app.run(function ($location, $rootScope, $document) {
        $rootScope.$on("$routeChangeStart", function (event, nextRoute , prevRoute) {
            if (!prevRoute)
                $document.loggedIn = false;
            $document.referrer = prevRoute ? nextRoute.$$route.originalPath : "login";
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
                .when("/Income", route.resolve("Income"))
                .when("/Stock", route.resolve("Stock"))
                .when("/Expenses", route.resolve("Expenses"))
                .otherwise({ redirectTo: "/Products" });

        }]);
    return app;
});





