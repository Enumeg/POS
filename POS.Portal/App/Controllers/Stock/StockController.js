"use strict";

define(["app"], function (app) {
    app.register.controller("StockController", ["$scope", "dataSource",
        function ($scope, dataSource) {

            $scope.stocks = [];
            $scope.products = [];
            $scope.points = {};
            $scope.total = 0;
          //Initialize
            function initialize() {
                dataSource.initialize("/api/Stock");
                list();
                prepareLookups();
            }

            //PrepareLookups
            function prepareLookups() {
                dataSource.loadList($scope.points, "api/points");
                dataSource.loadList($scope.products, "api/products");
            }
            //List
            function list() {
                dataSource.getList().success(function (data) {
                    $scope.total = 0;
                    data.forEach(function (item) {
                        $scope.total += item.Amount;
                    });
                    $scope.stocks = data;
                }).error(dataSource.error);
            };
            initialize();

        }]);
});
//Controller
