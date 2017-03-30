"use strict";
/* Controller that manage a list of products of certain type */

define(["app"], function (app) {
    app.register.controller("ProductsController", ["$scope", "dataSource", "$location",
        function ($scope, dataSource, $location) {
            $scope.products = [];
            $scope.product = {};
            $scope.setValue = function (selected) {
                this.$parent.property.Value = angular.isString(selected.originalObject) ? selected.originalObject : selected.originalObject.Value;
            }
            $scope.listProperties = function () {
                dataSource.getUrl("/api/Properties", { categoryId: $scope.product.CategoryId }).success(function (list) {
                    list.forEach(function (proprty) {
                        $scope.product.Properties.push({
                            PropertyId: proprty.Id,
                            Name: proprty.Name,
                            Values: proprty.Products,
                            Value: ""
                        });
                    });
                }).error(dataSource.error);

            }
            //Create
            $scope.new = function () {
                $scope.product = (1, 0, { Id: 0, Name: "", BarCode: "", Properties: [] });
                $("#productModal").modal("show");
            }
            //Edit
            $scope.edit = function () {
                angular.extend($scope.product, this.product);
                dataSource.getUrl("/api/Properties", { categoryId: $scope.product.CategoryId }).success(function (list) {
                    list.forEach(function (proprty, index) {
                        angular.extend($scope.product.Properties[index], { Name: proprty.Name, Values: proprty.Products, ProductId: $scope.product.Id });
                        $scope.product.Properties[index].Property = null;
                    });
                    $("#productModal").modal("show");

                }).error(dataSource.error);

            }
            $scope.add = function () {
                var product = this.product;
                $scope.product = { Id: 0, Name: product.Name, BarCode: product.BarCode,SalePrice : product.SalePrice, CategoryId: product.CategoryId, UnitId: product.UnitId, Properties: [] }
                dataSource.getUrl("/api/Properties", { categoryId: $scope.product.CategoryId }).success(function (list) {
                    list.forEach(function (proprty, index) {
                        $scope.product.Properties.push({
                            PropertyId: proprty.Id,
                            Name: proprty.Name,
                            Values: proprty.Products,
                            Value: product.Properties[index].Value
                        });
                    });
                    $("#productModal").modal("show");

                }).error(dataSource.error);

            }
            //Delete
            $scope.delete = function () {
                dataSource.delete(this.product.Name, this.product.Id, $scope.products, this.product);
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

                if (!scope.product.Id)//New product
                    dataSource.insert(scope.product).success(function (data) {
                        dataSource.success(scope.product.Name);
                        list();
                        $("#productModal").modal("hide");

                    }).error(dataSource.error);
                else
                    dataSource.update(scope.product).success(function () {
                        dataSource.success(scope.product.Name);
                        list();
                        $("#productModal").modal("hide");

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
                dataSource.getUrl("/api/categories").success(function (list) {
                    $scope.categories = list;
                }).error(dataSource.error);
                dataSource.getUrl("/api/units").success(function (list) {
                    $scope.units = list;
                }).error(dataSource.error);
                $("#productModal").modal({ show: false });
                list();
            };

            initialize();

        }]);
});
//Controller
