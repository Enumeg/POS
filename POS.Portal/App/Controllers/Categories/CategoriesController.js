"use strict";

define(["app"], function (app) {
    app.register.controller("CategoriesController", ["$scope", "dataSource",
        function ($scope, dataSource) {

            $scope.categories = [];
            $scope.units = [];
            $scope.properties = [];
            $scope.category = {};
            //Create
            $scope.new = function () {
                $scope.categories.splice(0, 0, { Id: 0, Name: "", Units: [], Properties: [], allUnits: $scope.units, allProperties: $scope.properties });
            }
            //Edit
            $scope.edit = function () {

                angular.merge($scope.category, this.category);
                this.editable = true;

            }
            //Delete
            $scope.delete = function () {
                dataSource.delete(this.category.Name, this.category.Id, $scope.categories, this.category);
            };
            //Cancel
            $scope.cancel = function () {
                if (!this.category.Id)
                    $scope.categories.shift();
                else {
                    $scope.categories.splice($scope.categories.indexOf(this.category), 1,  $scope.category);
                    this.editable = false;
                }
            }
            //Save
            $scope.save = function () {
                var scope = this;
                if (!scope.category.Id)//New Category
                    dataSource.insert(scope.category).success(function (data) {
                        angular.extend(scope.category, data);
                        dataSource.success(scope.category.Name);
                        scope.editable = false;
                    }).error(dataSource.error);
                else
                    dataSource.update(scope.category).success(function () {
                        dataSource.success(scope.category.Name);
                        scope.editable = false;
                    }).error(dataSource.error);
            };
            //Initialize
            function initialize() {
                dataSource.initialize("/api/Categories");
                prepareLookups();
            }

            function setSelectedItems(items, selectedItems) {
                items.forEach(function (item) {
                    item.selected = false;
                    selectedItems.forEach(function (selected) {
                        if (selected.Id === item.Id)
                            item.selected = true;
                        return;
                    });
                });
            }

            function setCategory(category) {
                category.allUnits = angular.copy($scope.units);
                category.allProperties = angular.copy($scope.properties);
                setSelectedItems(category.allProperties, category.Properties);
                setSelectedItems(category.allUnits, category.Units);
            }
            //PrepareLookups
            function prepareLookups() {
                dataSource.getUrl("/api/Units").success(function (data) {
                    $scope.units = data;
                    dataSource.getUrl("/api/Properties").success(function (data) {
                        $scope.properties = data;
                        list();
                    }).error(dataSource.error);
                }).error(dataSource.error);
            }
            //List
            function list() {
                dataSource.getList().success(function (data) {
                    data.forEach(function (item) {
                        setCategory(item);
                    });
                    $scope.categories = data;
                }).error(dataSource.error);
            };
            initialize();

        }]);
});
//Controller
