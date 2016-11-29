"use strict";
/* Controller that manage a list of properties of certain type */

define(["app"], function (app) {
    app.register.controller("PropertiesController", ["$scope", "dataSource", 
        function ($scope, dataSource) {

            $scope.properties = [];
            $scope.property = {};
            //Create
            $scope.new = function () {
                $scope.properties.splice(1, 0, { Id: 0, Name: "" });
            }
            //Edit
            $scope.edit = function () {
                angular.extend($scope.property, this.property);
                this.editable = true;
            }          
            //Delete
            $scope.delete = function () {
                dataSource.delete(this.property.Name, this.property.Id, $scope.properties, this.property);
            };
            //Cancel
            $scope.cancel = function () {
                if (!this.property.Id)
                    $scope.properties.shift();
                else {
                    angular.extend(this.property, $scope.property);
                    this.editable = false;
                }
            }
            //Save
            $scope.save = function () {
                var scope = this;
                if (!scope.property.Id)//New Property
                    dataSource.insert(scope.property).success(function (data) {
                        angular.extend(scope.property, data);
                        dataSource.success(scope.property.Name);
                        scope.editable = false;
                    }).error(dataSource.error);
                else
                    dataSource.update(scope.property).success(function () {
                        dataSource.success(scope.property.Name);
                        scope.editable = false;
                    }).error(dataSource.error);
            };
            //List
            function list() {
                dataSource.getList()
                    .success(function (data) {
                        $scope.properties = data;
                    })
                    .error(dataSource.error);
            };
            //Initialize
            function initialize() {
                dataSource.initialize("/api/Properties");
                list();
            };

           initialize();

        }]);
});
//Controller
