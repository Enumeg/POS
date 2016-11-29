'use strict';
/* Controller that manage a list of Model of certain type */

define(['app'], function (app) {
    app.register.controller('ModelController', ['$scope', '$location', 'dataSource', 'uiHeaderService', 'resource', '$modal',
        function ($scope, $location, dataSource, ui, resource, $modal) {

            var apiBaseUrl = "/api/Model";
            $scope.$resource = resource.getAll();
            $scope.models = [];

            //Create
            $scope.new = function () {
                $location.path('/Model/Edit/0');
            };
            //Edit
            $scope.edit = function () {
                $location.path('/Model/Edit/' + this.model.Id);
            };
            //View
            $scope.view = function () {

            };
            //List
            $scope.list = function () {
                dataSource.getList()
                    .success(function (data) {
                        data.forEach(function (model) {
                            model.TypeName = model.Type == 1 ? $scope.$resource.Component : $scope.$resource.Speaker;
                        })
                        $scope.models = data;
                    })
                    .error(dataSource.error);
            };
            //Delete
            $scope.delete = function () {
                if (confirm($scope.$resource.ConfirmDelete + this.model.Name + "?"))
                    dataSource.delete(this.model.Id, $scope.models, this.model);
            };
            //Initialize
            $scope.initialize = function initialize() {
                dataSource.initialize(apiBaseUrl);
                $scope.list();
                $scope.prepareLookups();
            };
            //PrepareLookups
            $scope.prepareLookups = function () {
                dataSource.getUrl('/api/Structure').success(function (data) { $scope.Structures = data; }).error(dataSource.error);
            }

            $scope.initialize();

        }]);
});
//Controller
