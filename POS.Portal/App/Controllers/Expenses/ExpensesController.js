"use strict";

define(["app"], function (app) {
    app.register.controller("ExpensesController", ["$scope", "dataSource", "$cookies",
        function ($scope, dataSource, $cookies) {

            $scope.expenses = [];
            $scope.people = [];
            $scope.safes = [];
            $scope.expense = {};
            //Create
            $scope.new = function () {
                $scope.expenses.splice(0, 0, { Id: 0, Date: new Date(), Value: 0, AccountType: 0, Description: '', SafeId: parseInt($cookies.get("Safe")) });
            }
            //Edit
            $scope.edit = function () {

                angular.merge($scope.expense, this.expense);
                this.editable = true;

            }
            //Delete
            $scope.delete = function () {
                dataSource.delete(this.expense.Name, this.expense.Id, $scope.expenses, this.expense);
            };
            //Cancel
            $scope.cancel = function () {
                if (!this.expense.Id)
                    $scope.expenses.shift();
                else {
                    $scope.expenses.splice($scope.expenses.indexOf(this.expense), 1, $scope.expense);
                    this.editable = false;
                }
            }
            //Save
            $scope.save = function () {
                var scope = this;
                this.editForm.$submitted = true;
                if (!this.editForm.$valid)
                    return;
                if (!scope.expense.Id)//New Category
                    dataSource.insert(scope.expense).success(function (data) {
                        data.Date = new Date(data.Date);
                        angular.extend(scope.expense, data);
                        dataSource.success(scope.expense.Name);
                        scope.editable = false;
                    }).error(dataSource.error);
                else
                    dataSource.update(scope.expense).success(function () {
                        dataSource.success(scope.expense.Name);
                        scope.editable = false;
                    }).error(dataSource.error);
            };
            //Initialize
            function initialize() {
                dataSource.initialize("/api/Expenses");
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
                    $scope.expenses = data;
                }).error(dataSource.error);
            };
            initialize();

        }]);
});
//Controller
