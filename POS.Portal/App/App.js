﻿"use strict";

define(["Services/routeResolver"], function () {

    var app = angular.module("POS", ["ngRoute", "ngCookies", "routeResolverServices", "ui.bootstrap", "angular-loading-bar", "angucomplete-alt", "isteven-multi-select"]);
    app.run(function ($location, $rootScope, resource) {
            resource.fillAsync(function() {
                $rootScope.$on("$routeChangeSuccess", function(event, current) {
                    $rootScope.title = resource.getValue( current.$$route.originalPath.split("/")[1]) + " - " + resource.getValue("Application");
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
                .when("/Customers", route.resolve("Coding", "Person"))
                .when("/Suppliers", route.resolve("Coding", "Person"))
                .when("/Points", route.resolve("Coding", "Points"))
                .otherwise({ redirectTo: "/Properties" });

        }]);
    return app;
});




