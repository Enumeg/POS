"use strict";

define([], function () {

    var services = angular.module("routeResolverServices", []);

    //Must be a provider since it will be injected into module.config()    
    services.provider("routeResolver", function () {

        this.$get = function () {
            return this;
        };

        this.routeConfig = function () {
            var controllersDirectory = "/App/Controllers/",

            setControllersDirectory = function (controllersDir) {
                controllersDirectory = controllersDir;
            },

            getControllersDirectory = function () {
                return controllersDirectory;
            };

            return {
                setControllersDirectory: setControllersDirectory,
                getControllersDirectory: getControllersDirectory
            };
        }();

        this.route = function (routeConfig) {
            var resolveDependencies = function ($q, $rootScope, dependencies) {
                var defer = $q.defer();
                require(dependencies, function () {
                    defer.resolve();
                    $rootScope.$apply();
                });

                return defer.promise;
            };
            var resolve = function (controller, view) {
                var routeDef = {};
                routeDef.label = {};
                routeDef.path = !view ? controller : controller.replace(view, "");
                routeDef.templateUrl = !view ? controller : routeDef.path + "/" + view;
                routeDef.controller = controller + "Controller";
                routeDef.resolve = {
                    load: ["$q", "$rootScope", function ($q, $rootScope) {
                        var dependencies = [routeConfig.getControllersDirectory() + routeDef.path + "/" + controller + "Controller.js"];
                        return resolveDependencies($q, $rootScope, dependencies);
                    }]
                };

                return routeDef;
            };

            return {
                resolve: resolve
            };
        }(this.routeConfig);
    });

});
