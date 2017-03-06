"use strict";
/* Controller that manage a list of Price of certain type */

define(["app"], function (app) {
    app.register.controller("PriceController", ["$scope", "$location", "dataSource", "uiHeaderService", "resource",
        function ($scope, $location, dataSource, ui, resource) {

            var apiBaseUrl = "/api/Price";
            $scope.$resource = resource.getAll();
            $scope.prices = [];
            $scope.price = {};
            //Create
            $scope.new = function () {
                $scope.prices.push({ ComponentId: 0, Value: 0 });
                window.scrollTo(0, document.body.scrollHeight);
            }
            //Edit
            $scope.edit = function () {
                angular.extend($scope.price, this.price);               
                this.editable = true;
            }
            //List
            $scope.list = function () {
                dataSource.getList()
                    .success(function (data) {
                        $scope.prices = data;
                    })
                    .error(dataSource.error);
            };
            //Delete
            $scope.delete = function () {
                if (confirm($scope.$resource.ConfirmDelete + $scope.$resource.Price + "?"))
                    dataSource.delete(this.price.Id, $scope.prices, this.price);
            };
            //Cancel
            $scope.cancel = function () {
                if (!this.price.Id)
                    $scope.prices.pop();
                else {
                    angular.extend(this.price, $scope.price);
                    this.editable = false;
                }
            }
            //Save
            $scope.save = function () {
                var scope = this;                
                if (!scope.price.Id)//New Price
                    dataSource.insert(scope.price).success(function (data) {
                        angular.extend(scope.price, data);
                        dataSource.success($scope.$resource.Price);
                        scope.editable = false;
                    }).error(dataSource.error);
                else
                    dataSource.update(scope.price).success(function (data) {
                        dataSource.success($scope.$resource.Price);
                        scope.editable = false;
                    }).error(dataSource.error);
            };
            //Initialize
            $scope.initialize = function initialize() {
                dataSource.initialize(apiBaseUrl);
                $scope.list();
                $scope.prepareLookups();
            };
            //PrepareLookups
            $scope.prepareLookups = function () {
                dataSource.getUrl("/api/Component").success(function (data) { $scope.Components = data; }).error(dataSource.error);
                dataSource.getUrl("/api/Supplier").success(function (data) { $scope.Suppliers = data; }).error(dataSource.error);
            }

            $scope.initialize();

        }]);
});
//Controller
