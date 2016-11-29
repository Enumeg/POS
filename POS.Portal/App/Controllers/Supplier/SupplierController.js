'use strict';
/* Controller that manage a list of Supplier of certain type */

define(['app'], function (app) {
    app.register.controller('SupplierController', ['$scope', '$location', 'dataSource', 'uiHeaderService','resource',
        function ($scope, $location, dataSource, ui, resource) {

            var apiBaseUrl = "/api/Supplier";
            $scope.$resource = resource.getAll();
            $scope.suppliers = [];
            $scope.supplier = {};
            //Create
            $scope.new = function () {
                $scope.suppliers.push({ Name: '' });
                window.scrollTo(0, document.body.scrollHeight);
            }
            $scope.edit = function () {
                angular.extend($scope.supplier, this.supplier);
                this.editable = true;
            }           
            //List
            $scope.list = function () {
                dataSource.getList()
                    .success(function (data) {
                        $scope.suppliers = data;
                    })
                    .error(dataSource.error);
            };
            //Delete
            $scope.delete = function () {
                if (confirm($scope.$resource.ConfirmDelete + this.supplier.Name + "?"))
                    dataSource.delete(this.supplier.Id, $scope.suppliers, this.supplier);
            };
            //Cancel
            $scope.cancel = function () {
                if (!this.supplier.Id)
                    $scope.suppliers.pop();
                else {
                    angular.extend(this.supplier, $scope.supplier);
                    this.editable = false;
                }
            }
            //Save
            $scope.save = function (supplier) {
                var scope = this;
                if (!scope.supplier.Id)//New Supplier
                    dataSource.insert(scope.supplier).success(function (data) {
                        angular.extend(scope.supplier, data);
                        dataSource.success(data.Name);
                        scope.editable = false;
                    }).error(dataSource.error);
                else
                    dataSource.update(scope.supplier).success(function (data) {
                        dataSource.success(scope.supplier.Name);
                        scope.editable = false;
                    }).error(dataSource.error);
            };
            //Initialize
            $scope.initialize = function initialize() {
                dataSource.initialize(apiBaseUrl);
                $scope.list();
            };

            $scope.initialize();
                     
        }]);
});
//Controller
