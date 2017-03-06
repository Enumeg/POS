"use strict";
/* Controller that manage a list of componentTypes of certain type */

define(["app"], function (app) {
    app.register.controller("ComponentTypeController", ["$scope", "$location", "dataSource", "uiHeaderService", "resource", "$modal",
        function ($scope, $location, dataSource, ui, resource, $modal) {

            var apiBaseUrl = "/api/ComponentType";
            $scope.$resource = resource.getAll();
            $scope.componentTypes = [];
            $scope.componentType = {};
            $scope.opened = false;
            $scope.propertiesEdit = function () {
                var componentType = this.componentType;
                if (!$scope.opened) {
                    var modalInstance = $modal.open({
                        templateUrl: "properties.html",
                        controller: PropertiesController,
                        resolve: {
                            componentType: function () {
                                return componentType;
                            }
                        }
                    });

                    modalInstance.result.then(function () {
                        $scope.opened = false;
                    }, function () {
                        $scope.opened = false;
                    });
                    $scope.opened = true;
                }
            };

            //Create
            $scope.new = function () {
                $scope.componentTypes.push({ Name: "" });
                window.scrollTo(0, document.body.scrollHeight);
            }
            //Edit
            $scope.edit = function () {
                angular.extend($scope.componentType, this.componentType);
                this.editable = true;
            }
            //List
            $scope.list = function () {
                dataSource.getList()
                    .success(function (data) {
                        $scope.componentTypes = data;
                    })
                    .error(dataSource.error);
            };
            //Delete
            $scope.delete = function () {
                if (confirm($scope.$resource.ConfirmDelete + this.componentType.Name + "?"))
                    dataSource.delete(this.componentType.Id, $scope.componentTypes, this.componentType);
            };
            //Cancel
            $scope.cancel = function () {
                if (!this.componentType.Id)
                    $scope.componentTypes.pop();
                else {
                    angular.extend(this.componentType, $scope.componentType);
                    this.editable = false;
                }
            }
            //Save
            $scope.save = function () {
                var scope = this;
                if (!scope.componentType.Id)//New ComponentType
                    dataSource.insert(scope.componentType).success(function (data) {
                        dataSource.success(data.Name);
                        angular.extend(scope.componentType, data);
                        scope.editable = false;
                    }).error(dataSource.error);
                else
                    dataSource.update(scope.componentType).success(function (data) {
                        dataSource.success(scope.componentType.Name);
                        scope.editable = false;
                    }).error(dataSource.error);
            };
            //Initialize
            $scope.initialize = function initialize() {
                dataSource.initialize(apiBaseUrl);
                $scope.list();
            };

            $scope.initialize();

            var PropertiesController = function ($scope, $modalInstance, dataSource, componentType, resource) {
                $scope.$resource = resource.getAll();

                dataSource.initialize("/api/Property");

                dataSource.getList({ id: componentType.Id }).success(function (data) {
                    $scope.ctProperties = data;
                }).error(dataSource.error);

                dataSource.getList().success(function (data) {
                    $scope.properties = data;
                }).error(dataSource.error);

                $scope.save = function (property) {
                    if (property.Id) {
                        property.ComponentTypes.push(componentType);
                        dataSource.update(property).success(function () { property.ComponentTypes = []; $scope.ctProperties.push(property); }).error(dataSource.error);
                    }
                    else {
                        property.ComponentTypes = [];
                        property.ComponentTypes.push(componentType);
                        property.Name = $("#properties_value").val();
                        dataSource.insert(property).success(function (data) {
                            property = data;
                            $scope.ctProperties.push(property);
                        }).error(dataSource.error);
                    }
                    $scope.property = null;
                }

                $scope.delete = function (property) {
                    if (confirm($scope.$resource.ConfirmDelete + property.Name + "?")) {
                        var index = $scope.ctProperties.indexOf(property)
                        property.ComponentTypes.push({ Id: componentType.Id, ComponentType: { Name: "Del" } });
                        dataSource.update(property).success(function () {
                            $scope.ctProperties.splice(index, 1);
                        }).error(dataSource.error);
                    }
                }

                $scope.cancel = function () {
                    $modalInstance.dismiss("cancel");
                };
            };

        }]);
});
//Controller
