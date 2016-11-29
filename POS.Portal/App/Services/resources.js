﻿"use strict";

define(["app"], function (app) {
    app.service("resource",["$http", function ($http) {
        var resource = {};
      
        this.fill = function () {
            $http.get("/api/Resource").success(function(data) {
                resource = data;
            });
        };
        this.fillAsync = function(callback) {
            $http.get("/api/Resource").success(function (data) {
                resource = data;
                callback();
            });
        };
        this.getValue = function (key) {
            return resource[key];
        };
        this.getAll = function () {
            return resource;
        };
    }]);
});
