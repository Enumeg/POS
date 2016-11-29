'use strict';
/* Controller that manage a list of Component of certain type */

define(['app'], function (app) {
    app.register.controller('ComponentController', ['$scope', '$location', 'dataSource', 'uiHeaderService', 'resource', '$modal',
        function ($scope, $location, dataSource, ui, resource, $modal) {

            var apiBaseUrl = "/api/Component";
            $scope.$resource = resource.getAll();
            $scope.components = [];
            
            //Create
            $scope.new = function () {
                if (!$scope.search || !$scope.search.componentType) {
                    alert("Please select a component type");
                    return
                }
                var modalInstance = $modal.open({
                    templateUrl: 'component.html',
                    controller: ComponentEditController,
                    size: 'lg',
                    resolve: {
                        component: function () {
                            return { ComponentTypeId: $scope.search.componentType.Id }
                        }
                    }
                });
                modalInstance.result.then(function () {
                    $scope.list();
                });
            };
            //Edit
            $scope.edit = function (component) {
                var modalInstance = $modal.open({
                    templateUrl: 'component.html',
                    controller: ComponentEditController,
                    size: 'lg',
                    resolve: {
                        component: function () {
                            return component;
                        }
                    }
                });
                modalInstance.result.then(function () {
                    $scope.list();
                });
            };
            //List
            $scope.list = function () {
                dataSource.getList()
                    .success(function (data) {
                        $scope.components = data;
                    })
                    .error(dataSource.error);
            };
            //Delete
            $scope.delete = function (component) {
                if (confirm($scope.$resource.ConfirmDelete + component.Name + "?"))
                    dataSource.delete(component.Id, $scope.components, component);
            };
            //Initialize
            $scope.initialize = function initialize() {
                dataSource.initialize(apiBaseUrl);
                $scope.list();
                dataSource.getUrl('/api/ComponentType').success(function (data) {
                    $scope.componentTypes = data;
                }).error(dataSource.error);
            };

            $scope.initialize();

            var ComponentEditController = function ($scope, $modalInstance, dataSource, resource, component) {

                $scope.openFile = function (event) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        $scope.component.LgImg = e.target.result;
                        $scope.$apply();
                    };
                    reader.readAsDataURL(event.files[0]);
                }
                $scope.save = function (component) {
                    if (!component.Id)//New Component
                    {
                        dataSource.insert(component).success(function (data) {
                            component = data; dataSource.success(data.Name);
                            $modalInstance.close();
                        }).error(dataSource.error);
                    } else
                        dataSource.update(component).success(function (data) {
                            dataSource.success(component.Name);
                            $modalInstance.close();
                        }).error(dataSource.error);
                };

                $scope.cancel = function () {
                    $modalInstance.dismiss('cancel');
                };

                $scope.getProperties = function (Id) {
                    return dataSource.getUrl('/api/Property/' + Id).error(dataSource.error);
                }

                $scope.initialize = function initialize() {
                    $scope.$resource = resource.getAll();
                    $scope.component = component;
                    dataSource.initialize('/api/Component');
                    if (!component.Id) {
                        $scope.title = $scope.$resource.Add;
                        $scope.component.Properties = [];
                        $scope.getProperties(component.ComponentTypeId).success(function (data) {
                            $scope.properties = data;
                            $scope.height = data.length * 44;
                            $scope.height = $scope.height > 240 ? $scope.height : 240;
                            data.forEach(function (item) {
                                $scope.component.Properties.push({ PropertyId: item.Id, Value: null });
                            });
                        });
                    }
                    else {
                        var ids = [];
                        $scope.title = $scope.$resource.Edit;
                        dataSource.get($scope.component.Id).success(function (data) {
                            $scope.component = data;
                            data.Properties.forEach(function (item) {
                                ids.push(item.PropertyId);
                            });
                            $scope.getProperties(component.ComponentTypeId).success(function (data) {
                                $scope.properties = data;
                                $scope.height = data.length * 44;
                                $scope.height = $scope.height > 240 ? $scope.height : 240;
                                data.forEach(function (item) {
                                    if (ids.indexOf(item.Id) == -1)
                                        $scope.component.Properties.push({ PropertyId: item.Id, Value: null, ComponentId: $scope.component.Id });
                                });
                            });
                        }).error(dataSource.error);

                    }
                };

                $scope.initialize();


            };

        }]);
});
//Controller
