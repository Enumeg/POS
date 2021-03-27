"use strict";

define(["app"], function (app) {
    app.register.controller("IncomeController", ["$scope", "dataSource", "$cookies",
        function ($scope, dataSource, $cookies) {

            $scope.incomes = [];
            $scope.people = [];
            $scope.safes = [];
            $scope.income = {};

            //Create
            $scope.new = function () {
                $scope.incomes.splice(0, 0, { Id: 0, Date: new Date(), Value: 0, AccountType: 0, Description: '', SafeId: parseInt($cookies.get("Safe")) });
            }
            //Edit
            $scope.edit = function () {

                angular.merge($scope.income, this.income);
                this.editable = true;

            }
            //Delete
            $scope.delete = function () {
                dataSource.delete(this.income.Name, this.income.Id, $scope.incomes, this.income);
            };
            //Cancel
            $scope.cancel = function () {
                if (!this.income.Id)
                    $scope.incomes.shift();
                else {
                    $scope.incomes.splice($scope.incomes.indexOf(this.income), 1, $scope.income);
                    this.editable = false;
                }
            }
            //Save
            $scope.save = function () {
                var scope = this;
                this.editForm.$submitted = true;
                if (!this.editForm.$valid)
                    return;
                if (!scope.income.Id)//New Category
                    dataSource.insert(scope.income).success(function (data) {
                        data.Date = new Date(data.Date);
                        angular.extend(scope.income, data);
                        dataSource.success(scope.income.Name);
                        scope.editable = false;
                    }).error(dataSource.error);
                else
                    dataSource.update(scope.income).success(function () {
                        dataSource.success(scope.income.Name);
                        scope.editable = false;
                    }).error(dataSource.error);
            };
            //Initialize
            function initialize() {
                dataSource.initialize("/api/Income");
                list();
                prepareLookups();
            }

         
            //PrepareLookups
            function prepareLookups() {
                dataSource.loadList($scope.people, "api/people");
                dataSource.loadList($scope.safes, "api/safe");
            }
            //List
            function list() {
                dataSource.getList().success(function (data) {
                    data.forEach(function (item) {
                        item.Date = new Date(item.Date);
                    });
                    $scope.incomes = data;
                }).error(dataSource.error);
            };
            initialize();

        }]);
});
//Controller
