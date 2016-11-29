'use strict';
/* Controller that manage a list of Model of certain type */

define(['app'], function (app) {
    app.register.controller('ModelEditController', ['$scope', '$location', 'dataSource', 'uiHeaderService', 'resource', '$modal', '$routeParams',
        function ($scope, $location, dataSource, ui, resource, $modal, $routeParams) {

            var apiBaseUrl = "/api/Model";
            $scope.$resource = resource.getAll();
            var modelId = ($routeParams.id) ? parseInt($routeParams.id) : 0;
            $scope.model = { Total: 0 };
            $scope.model.Components = [];
            $scope.Components = [];
            //Delete
            $scope.delete = function () {
                if (confirm($scope.$resource.ConfirmDelete + $scope.model.Name + "?"))
                    dataSource.delete($scope.model.Id, '/Models');
            };
            //Cancel 
            $scope.cancel = function () {
                $location.path('/Models');
            }
            //add
            $scope.add = function () {
                var component = this.Component;
                var old = false;
                $scope.model.Components.forEach(function (com) {
                    if (com.Component.Id == component.Component.Id && com.Price.Value == component.Price.Value)
                    { com.Amount++; com.Total += com.Price.Value; $scope.model.Total += com.Price.Value; old = true; }
                })
                if (!old) {
                    $scope.model.Total += component.Price.Value;
                    $scope.model.Components.push({ ModelId: $scope.model.Id, ComponentId: component.Component.Id, Component: component.Component, PriceId: component.Price.Id, Price: component.Price, Amount: 1, Total: component.Price.Value });
                }
            }
            //Remove
            $scope.remove = function (count) {
                if (count == 1) {

                    if (this.component.Amount == 1)
                        $scope.model.Components.splice(this.$index);
                    else
                        this.component.Amount--;

                    this.component.Total -= this.component.Price.Value;
                    $scope.model.Total -= this.component.Price.Value;
                }
                else {
                    $scope.model.Components.splice(this.$index, 1);
                    $scope.model.Total -= this.component.Total;
                }
            }
            //select
            $scope.selectS = function () {
                $scope.Structures.forEach(function (structure) {
                    if($scope.model.StructureId == structure.Id)
                    {
                        $scope.model.Structure = structure;
                        return;
                    }
                })
            }
            //Save
            $scope.save = function () {
                this.editFrom.$submitted = true;
                if (this.editFrom.$valid) {
                    var scope = this;                   
                    if (!scope.model.Id)//New ComponentType
                        dataSource.insert(scope.model).success(function (data) {
                            dataSource.success(data.Name);
                            $location.path('/Models');
                        }).error(dataSource.error);
                    else
                        dataSource.update(scope.model).success(function (data) {
                            dataSource.success(scope.model.Name);
                            $location.path('/Models');
                        }).error(dataSource.error);
                }
            }
            //PrepareLookups
            $scope.prepareLookups = function () {
                dataSource.getUrl('/api/Structure').success(function (data) {
                    $scope.Structures = data;
                    if (modelId != 0) {
                        dataSource.get(modelId).success(function (data) {
                            $scope.model = data;
                            $scope.selectS();
                        })
                    }
                }).error(dataSource.error);
                dataSource.getUrl('/api/Component/0').success(function (data) {
                    $scope.components = data;
                    data.forEach(function (component) {
                        component.Prices.forEach(function (price) {
                            $scope.Component = component;
                            $scope.$apply();
                            var com = { Price: price, Component: component, Id: component.Id, ComponentTypeId: component.ComponentTypeId, details: $('#details')[0].outerHTML };
                            $scope.Components.push(com);
                        });
                    });
                }).error(dataSource.error);
            }
            //Initialize
            $scope.initialize = function initialize() {
                dataSource.initialize(apiBaseUrl);
                $scope.prepareLookups();
            };

            $scope.initialize();

        }]);
});
//Controller
