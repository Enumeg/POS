﻿"use strict";
/* Controller that manage a list of list of certain type */

define(["app"], function (app) {
    app.register.controller("TransactionsController", ["$scope", "dataSource", "$location", "toastr", "resource",
        function ($scope, dataSource, $location, toastr, resource) {
            var page = $location.url().split("/")[1];          
            resource.loadDictionary(function (data) {
                $scope.resource = data;
            });
            $scope.suppliers = [];
            $scope.customers = [];
            $scope.points = [];
            $scope.products = [];
            $scope.selectedProducts = [];
            $scope.bankAccounts = [];
            $scope.transaction = { Total: 0, Discount: 0, Paid: 0, Rest: 0, PaymentMethod: 1, Installments: [], Cheques: [], Details: [] };
            $scope.product = {};
            $scope.selectedIndex = -1;
            $scope.modalTitle = '';
            $scope.addInstallment = function () {
                addInstallment($scope.transaction.Installments, this.installment);
            };
            $scope.generateInstallments = function () {
                generateInstallments($scope.transaction.Installments, this.installment);
            };
            $scope.addCheque = function () {
                addInstallment($scope.transaction.Cheques, this.cheque);
            };
            $scope.generateCheques = function () {
                generateInstallments($scope.transaction.Cheques, this.cheque);
            };
            $scope.add = function (tmpl) {
                $scope.tmpl = tmpl + ".tmpl";
                $scope.modalTitle = $scope.resource[tmpl];
                $("#Modal").modal("show");
            };
            $scope.checkBarcode = function (key) {
                if (key === 13) {
                    var barcode = $scope.product.Barcode;
                    dataSource.getUrl("api/products", { barcode }).success(function (product) {
                        if (product) {
                            var price = page.indexOf("Sale") !== -1 ? product.SalePrice : 0.0;
                            $scope.product = { Amount: 1.0, Price: price, Name: product.Name, ProductId: product.Id, Barcode: barcode };
                            $scope.$broadcast("angucomplete-alt:changeInput", "productName", product.Name);
                            $("#amount").focus();
                        }
                        else
                            toastr.error("No product found for this serial");
                    }).error(dataSource.error);
                }
            }
            $scope.setValue = function (selected) {
                if (selected) {
                    var price = page.indexOf("Sale") !== -1 ? selected.originalObject.SalePrice : 0.0;
                    $scope.product = { Amount: 1.0, Price: price, Name: selected.title, ProductId: selected.originalObject.Id, Barcode: "", Barcodes: [] };
                    $("#amount").focus();
                }
            }
            $scope.addProduct = function () {
                if (!$scope.product.ProductId) return;
                if (page.indexOf("Sale") !== -1 && !$scope.transaction.PointId) {
                    toastr.error("asdasd");
                    return;
                }
                checkStorage($scope.product.ProductId, $scope.transaction.PointId);
                var productId = $scope.product.ProductId + "" + $scope.product.Price;
                var index = $scope.selectedProducts.indexOf(productId);
                if (index === -1) {
                    $scope.selectedProducts.push(productId);
                    $scope.transaction.Details.push($scope.product);
                } else {
                    $scope.transaction.Details[index].Amount += $scope.product.Amount;
                }
                $scope.product = {};
                $scope.$broadcast("angucomplete-alt:clearInput", "productName");
                $("#Barcode").focus();

            }
            $scope.addSerials = function (index) {
                $scope.selectedIndex = index;
                $scope.tmpl = "serials.tmpl";
                $scope.modalTitle = $scope.resource.Barcode;            
                $("#Modal").modal("show");
            }
            $scope.addSerial = function () {
                var item = $scope.transaction.Details[$scope.selectedIndex];
                item.Barcodes.push({ Barcode: { Barcode: this.serial, ProductId: item.ProductId } });
                if (item.Barcodes.length > item.Amount)
                    item.Amount = item.Barcodes.length;
                this.serial = "";
            }
            //Delete
            $scope.delete = function (index) {
                $scope.transaction.Details.splice(index, 1);
                $scope.selectedProducts.splice(index, 1);
            };
            $scope.getTotal = function () {
                $scope.transaction.Total = 0;
                $scope.transaction.Details.forEach(function (item) {
                    $scope.transaction.Total += item.Amount * item.Price;
                });
                return $scope.transaction.Total;
            }
            $scope.getRest = function () {
                $scope.transaction.Rest = $scope.transaction.Total - $scope.transaction.Discount - $scope.transaction.Paid;
                return $scope.transaction.Rest;
            }
            //Save
            $scope.save = function () {
                var scope = this;
                scope.transactionForm.$submitted = true;
                if (!scope.transactionForm.$valid)
                    return;
                if (!scope.transaction.PaymentMethod) {
                    toastr.error("Payment");
                    return;
                }
                $scope.transaction.Details.forEach(function (item) {
                    if (item.Barcode === "" && item.Barcodes.length !== item.Amount) {
                        toastr.error($scope.resource.Barcodes);
                        return;
                    }
                });
                if (!scope.transaction.Id)//New entity
                    dataSource.insert(scope.transaction).success(function (data) {

                    }).error(dataSource.error);
                else
                    dataSource.update(scope.entity).success(function () {
                        dataSource.success(scope.entity.Name);
                        scope.editable = false;
                    }).error(dataSource.error);
            };
            $scope.savePerson = function (person) {
                var url = page.indexOf("Sale") !== -1 ? "api/customers" : "api/suppliers";
                this.personForm.$submitted = true;
                if (!this.personForm.$valid)
                    return;
                dataSource.insert(person, null, url).success(function (data) {
                    if (page.indexOf("Sale") !== -1) {
                        $scope.customers.push(data);
                        $scope.transaction.CustomerId = data.Id;                       
                    }
                    else
                    {
                        $scope.suppliers.push(data);
                        $scope.transaction.SupplierId = data.Id;
                    }
                    $("#Modal").modal("hide");
                }).error(dataSource.error);

            };
            function checkStorage(productId, PointId) {

            }
            //Initialize
            function initialize() {
                dataSource.initialize("/api/" + page);
                dataSource.loadList($scope.products, "api/products");
                dataSource.loadList($scope.bankAccounts, "api/BankAccounts");

                if (page.indexOf("Purchase") !== -1) {
                    dataSource.loadList($scope.suppliers, "api/suppliers");
                    dataSource.loadList($scope.points, "api/points?type=1");

                } else {
                    dataSource.loadList($scope.customers, "api/customers");
                    dataSource.loadList($scope.points, "api/points?type=1");
                }
                $("#Modal").modal({ show: false });
            };
            var addInstallment = function (list, installment) {
                var total = 0;
                list.forEach(function (item) {
                    total += item.Value;
                });
                if (total + installment.Value > $scope.transaction.Rest) {
                    toastr.error("");
                    return;
                }
                list.push(installment);
            }
            var generateInstallments = function (list, installment) {
                list.length = 0;
                var rest = $scope.transaction.Rest;
                var Value = $scope.transaction.Rest / installment.Count;
                var date = new Date(installment.Date.getTime());
                for (var i = 0; i < installment.Count; i++) {
                    list.push({ Value, DueDate: new Date(date.getTime()), Number: (installment.Number || "") });
                    date.setMonth(date.getMonth() + installment.Months);
                }
            }
            initialize();

        }]);
});
//Controller
