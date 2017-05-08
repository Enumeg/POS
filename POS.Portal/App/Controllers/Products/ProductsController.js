"use strict";
/* Controller that manage a list of products of certain type */

define(["app"], function (app) {
    app.register.controller("ProductsController", ["$scope", "dataSource", "$location",
        function ($scope, dataSource, $location) {
            $scope.products = [];
            $scope.product = {};
            $scope.setValue = function (selected) {
                if (!selected)
                    return;
                var value = angular.isString(selected.originalObject) ? selected.originalObject : selected.originalObject.Value;
                if ($scope.isMultiple) {
                    if (this.$parent.property.values.indexOf(value) === -1)
                        this.$parent.property.values.push(value);
                    $scope.$broadcast("angucomplete-alt:clearInput");

                }
                else
                    this.$parent.property.Value = value;

            }
            $scope.listProperties = function () {
                dataSource.getUrl("/api/Properties", { categoryId: $scope.product.CategoryId }).success(function (list) {
                    list.forEach(function (property) {
                        $scope.product.Properties.push({
                            PropertyId: property.Id,
                            Name: property.Name,
                            Values: property.Values,
                            values: [],
                            Value: ""
                        });
                    });
                }).error(dataSource.error);
            }
            //Create
            $scope.new = function (isMultiple) {
                $scope.isMultiple = isMultiple;
                $scope.product = (1, 0, { Id: 0, Name: "", BarCode: "", Properties: [] });
                $("#productModal").modal("show");
            }
            //Edit
            $scope.edit = function () {
                $scope.isMultiple = false;
                angular.extend($scope.product, this.product);
                dataSource.getUrl("/api/Properties", { categoryId: $scope.product.CategoryId }).success(function (list) {
                    list.forEach(function (property, index) {
                        angular.extend($scope.product.Properties[index], { Name: property.Name, Values: property.Values });
                    });
                    $("#productModal").modal("show");

                }).error(dataSource.error);
            }
            $scope.add = function () {
                $scope.isMultiple = false;
                var product = this.product;
                $scope.product = { Id: 0, Name: product.Name, BarCode: product.BarCode, SalePrice: product.SalePrice, CategoryId: product.CategoryId, UnitId: product.UnitId, Properties: [] }
                dataSource.getUrl("/api/Properties", { categoryId: $scope.product.CategoryId }).success(function (list) {
                    list.forEach(function (property, index) {
                        $scope.product.Properties.push({
                            PropertyId: property.Id,
                            Name: property.Name,
                            Values: property.Values,
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

                if ($scope.isMultiple) {
                    dataSource.insert($scope.newProducts, null, "/api/products/Add").success(function (data) {
                        dataSource.success("");
                        list();
                        $("#productModal").modal("hide");

                    }).error(dataSource.error);
                }
                else {
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
                }
            };

            $scope.generate = function () {
                $scope.newProducts = [];

                $scope.product.Properties.forEach(function (property) {
                    if ($scope.newProducts.length === 0) {
                        property.values.forEach(function (value) {
                            var product = {};
                            angular.extend(product, $scope.product);
                            product.Name += " " + value;
                            product.Properties = [];
                            product.Properties.push({
                                PropertyId: property.PropertyId,
                                Value: value
                            });
                            $scope.newProducts.push(product);
                        });
                    }
                    else {
                        var products = [];
                        $scope.newProducts.forEach(function (prd) {
                            property.values.forEach(function (value, i) {
                                var product = {};
                                angular.merge(product, prd);
                                product.Name += " " + value;
                                product.Properties.push({
                                    PropertyId: property.PropertyId,
                                    Value: value
                                });

                                products.push(product);
                            });
                        });
                        if (products.length > 0) {
                            $scope.newProducts = [];
                            angular.copy(products, $scope.newProducts);
                        }
                    }
                });

            }
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
