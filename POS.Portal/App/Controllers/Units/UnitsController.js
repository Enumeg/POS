"use strict";
/* Controller that manage a list of units of certain type */

define(["app"], function (app) {
    app.register.controller("UnitsController", ["$scope", "dataSource",
        function ($scope, dataSource) {

            $scope.units = [];
            $scope.unit = {};
            //Create
            $scope.new = function () {
                $scope.units.splice(1, 0, { Id: 0, Name: "" });
            }
            //Edit
            $scope.edit = function () {
                angular.extend($scope.unit, this.unit);
                this.editable = true;
            }
            //Delete
            $scope.delete = function () {
                dataSource.delete(this.unit.Name, this.unit.Id, $scope.units, this.unit);
            };
            //Cancel
            $scope.cancel = function () {
                if (!this.unit.Id)
                    $scope.units.shift();
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
                        $scope.units = data;
                    })
                    .error(dataSource.error);
            };
            //Initialize
            function initialize() {
                dataSource.initialize("/api/Units");
                list();
            };

            initialize();

        }]);
});
//Controller
