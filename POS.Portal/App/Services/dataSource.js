"use strict";

define(["app"], function (app) {
    app.factory("dataSource", ["$http", "uiHeaderService", "$location", "resource", function ($http, ui, $location, resource) {
        var baseUrl, $resource;
        var dataSource = {};
        dataSource.initialize = function ($baseUrl) {
            dataSource.initialized = true;
            baseUrl = $baseUrl;
            $resource = resource.getAll();
        };
        dataSource.getList = function (param) {
            if (param) {
                return $http.get(baseUrl, { params: param });
            }
            else {
                return $http.get(baseUrl);
            }
        };

        dataSource.get = function (id) {
            return $http.get(baseUrl + "/" + id);
        };
        dataSource.getUrl = function (url, param) {
            if (param)
                return $http.get(url, { params: param });
            else
                return $http.get(url);
        };
        dataSource.insert = function (data, param) {
            if (!param)
                return $http.post(baseUrl, data);
            else
                return $http.post(baseUrl, data, { params: param });
        };

        dataSource.update = function (data) {
            return $http.put(baseUrl + "/" + data.Id, data);
        };

        dataSource.delete = function (name, id, list, item) {
            if (confirm($resource.ConfirmDelete + name + "?")) {
                return $http.delete(baseUrl + "/" + id).
                    success(function() {
                        ui.showMessage($resource.DeletedSuccessfully, "success");
                        if (item)
                            list.splice(list.indexOf(item), 1);
                        else
                            $location.path(list);
                    }).
                    error(function() {
                        ui.showMessage($resource.DeleteError, "error");
                    });
            } else return false;
        };

        dataSource.error = function (data) {
            ui.showMessage(data.Message, "error");
        };
        dataSource.success = function (title) {
            if(title)
                ui.showMessage(title + " " + $resource.SaveSuccessfully, "success");
            else
                ui.showMessage($resource.SaveSuccessfully, "success");

        };

        return dataSource;
    }]);
});
