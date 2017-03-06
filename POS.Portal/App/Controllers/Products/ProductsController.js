"use strict";
/* Controller that manage a list of products of certain type */

define(["app"], function (app) {
    app.register.controller("ProductsController", ["$scope", "dataSource", "$location",
        function ($scope, dataSource, $location) {
            $scope.products = [];
            $scope.product = {};
            //Create
            $scope.new = function () {
                $scope.product = (1, 0, { Id: 0, Name: "", BarCode: "" });
                $("#productModal").modal("show");
            }
            //Edit
            $scope.edit = function () {
                $scope.product = this.product;
                $("#productModal").modal("show");
            }
            //Delete
            $scope.delete = function () {
                dataSource.delete(this.unit.Name, this.unit.Id, $scope.products, this.unit);
            };
            //Cancel
            $scope.cancel = function () {
                if (!this.unit.Id)
                    $scope.products.shift();
                else {
                    angular.extend(this.unit, $scope.unit);
                    this.editable = false;
                }
            }
            //Save
            $scope.save = function () {
                var scope = this;
                if (!scope.unit.Id)//New unit
                    dataSource.insert(scope.unit).success(function (data) {
                        angular.extend(scope.unit, data);
                        dataSource.success(scope.unit.Name);
                        scope.editable = false;
                    }).error(dataSource.error);
                else
                    dataSource.update(scope.unit).success(function () {
                        dataSource.success(scope.unit.Name);
                        scope.editable = false;
                    }).error(dataSource.error);
            };
            //List
            function list() {
                dataSource.getList()
                    .success(function (data) {
                        $scope.products = data;
                    })
                    .error(dataSource.error);
            };
            //Initialize
            function initialize() {
                dataSource.initialize("/api/Products");
                $("#productModal").modal({ show: false });
                list();
            };

            initialize();

        }]);
});
//Controller
