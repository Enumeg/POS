"use strict";
/* Controller that manage a list of list of certain type */

define(["app"], function (app) {
    app.register.controller("TransactionsController", ["$scope", "dataSource", "$location", "toastr", "resource",
        function ($scope, dataSource, $location, toastr, resource) {
            var page = $location.url().split("/")[1];
            $scope.resource = resource.getAll();
            $scope.suppliers = [];
            $scope.customers = [];
            $scope.points = [];
            $scope.products = [];
            $scope.transactionDetails = [];
            $scope.selectedProducts = [];
            $scope.bankAccounts = [];
            $scope.transaction = { Total: 0, Discount: 0, Paid: 0, Rest: 0, PaymentMethod: 1, Installments: [], Cheques :[] };
            $scope.product = {};
            $scope.selectedIndex = -1;

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

            $scope.addInstallments = function () {
                $scope.tmpl = "installments.tmpl";
                $("#Modal").modal("show");
            };
            $scope.addCheques = function () {
                $scope.tmpl = "cheques.tmpl";
                $("#Modal").modal("show");
            };
            $scope.PayVisa = function () {
                $scope.tmpl = "visa.tmpl";
                $("#Modal").modal("show");
            };
            
            $scope.checkBarcode = function (key) {
                if (key === 13) {
                    var barcode = $scope.product.Barcode;
                    dataSource.getUrl("api/products", { barcode }).success(function (product) {
                        if (product) {
                            $scope.product = { Amount: 1.0, Price: 0.0, Name: product.Name, ProductId: product.Id, Barcode: barcode };
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
                    $scope.product = { Amount: 1.0, Price: 0.0, Name: selected.title, ProductId: selected.originalObject.Id, Barcode: "", Barcodes: [] };
                    $("#amount").focus();
                }
            }
            $scope.addProduct = function () {
                if (!$scope.product.ProductId) return;
                var productId = $scope.product.ProductId + "" + $scope.product.Price;
                var index = $scope.selectedProducts.indexOf(productId);
                if (index === -1) {
                    $scope.selectedProducts.push(productId);
                    $scope.transactionDetails.push($scope.product);
                } else {
                    $scope.transactionDetails[index].Amount += $scope.product.Amount;
                }
                $scope.product = {};
                $scope.$broadcast("angucomplete-alt:clearInput", "productName");
                $("#Barcode").focus();

            }
            $scope.addSerials = function (index) {
                $scope.selectedIndex = index;
                $scope.tmpl = "serials.tmpl";
                $("#Modal").modal("show");
            }
            $scope.addSerial = function () {
                var item = $scope.transactionDetails[$scope.selectedIndex];
                item.Barcodes.push({ Barcode: { Barcode: this.serial, ProductId: item.ProductId } });
                if (item.Barcodes.length > item.Amount)
                    item.Amount = item.Barcodes.length;
                this.serial = "";
            }
            //Delete
            $scope.delete = function (index) {
                $scope.transactionDetails.splice(index, 1);
                $scope.selectedProducts.splice(index, 1);
            };
            $scope.getTotal = function () {
                $scope.transaction.Total = 0;
                $scope.transactionDetails.forEach(function (item) {
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
                $scope.transactionDetails.forEach(function (item) {
                    if (item.Barcode === "" && item.Barcodes.length !== item.Amount) {
                        toastr.error($scope.resource.Barcodes);
                        return;
                    }
                });
                scope.transaction[page.substr(0, page.length - 1) + "Details"] = $scope.transactionDetails;
                if (!scope.transaction.Id)//New entity
                    dataSource.insert(scope.transaction).success(function (data) {

                    }).error(dataSource.error);
                else
                    dataSource.update(scope.entity).success(function () {
                        dataSource.success(scope.entity.Name);
                        scope.editable = false;
                    }).error(dataSource.error);
            };
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
