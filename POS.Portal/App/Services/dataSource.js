"use strict";

define(["app"], function (app) {
    app.factory("dataSource", ["$http", "toastr", "$location", "resource", function ($http, toastr, $location, resource) {
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
        dataSource.loadList = function (list,url, param) {
            var result;
            if (param)
                result = $http.get(url, { params: param });
            else
                result = $http.get(url);
            result.success(function(data) {
                angular.copy(data, list);
            }).error(this.error);
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
                        toastr.success($resource.DeletedSuccessfully);
                        if (item)
                            list.splice(list.indexOf(item), 1);
                        else
                            $location.path(list);
                    }).
                    error(function() {
                        toastr.error($resource.DeleteError);
                    });
            } else return false;
        };

        dataSource.error = function (data) {
            toastr.error(data.Message);
        };
        dataSource.success = function (title) {
            if(title)
                toastr.success(title + " " + $resource.SaveSuccessfully);
            else
                toastr.success($resource.SaveSuccessfully);

        };

        return dataSource;
    }]);
});
