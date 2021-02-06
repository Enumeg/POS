"use strict";
/* Controller that manage a list of list of certain type */

define(["app"], function (app) {
    app.register.controller("CodingController", ["$scope", "dataSource", "$location", "resource",
        function ($scope, dataSource, $location, resource) {        

            $scope.list = [];
            $scope.entity = {};
            //Create
            $scope.new = function (entity) {
                if (entity === null)
                    entity = { Id: 0 }
                $scope.list.splice(0, 0, entity);
            }
            //Edit
            $scope.edit = function () {
                angular.extend($scope.entity, this.entity);
                this.editable = true;
            }
            //Delete
            $scope.delete = function () {
                dataSource.delete(this.entity.Name, this.entity.Id, $scope.list, this.entity);
            };
            //Cancel
            $scope.cancel = function () {
                if (!this.entity.Id)
                    $scope.list.shift();
                else {
                    angular.extend(this.entity, $scope.entity);
                    this.editable = false;
                }
            }
            //Save
            $scope.save = function () {
                var scope = this;
                scope.editForm.$submitted = true;
                if (!scope.editForm.$valid)
                    return;
                if (!scope.entity.Id)//New entity
                    dataSource.insert(scope.entity).success(function (data) {
                        angular.extend(scope.entity, data);
                        dataSource.success(scope.entity.Name);
                        scope.editable = false;
                    }).error(dataSource.error);
                else
                    dataSource.update(scope.entity).success(function () {
                        dataSource.success(scope.entity.Name);
                        scope.editable = false;
                    }).error(dataSource.error);
            };
            //List
            function list() {
                dataSource.getList()
                    .success(function (data) {
                        $scope.list = data;
                    })
                    .error(dataSource.error);
            };
            //Initialize
            function initialize() {
             
                $scope.page = $location.url().split("/")[1];
                resource.loadDictionary(function (data) {
                    $scope.resource = data;
                    $scope.title = resource.getValue($scope.page);
                    $scope.$parent.$root.title = $scope.title;
                });
                dataSource.initialize("/api/" + $scope.page);
                if ($scope.page === "BankAccounts") {
                    $scope.banks = [];
                    dataSource.loadList($scope.banks, "api/banks");
                }
                list();
            };

            initialize();

        }]);
});
//Controller
