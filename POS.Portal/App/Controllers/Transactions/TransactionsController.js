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
            $scope.transaction = { Total: 0, Discount: 0, Paid: 0, Rest: 0 };
            $scope.product = {};
            $scope.selectedIndex = -1;
            $scope.checkBarcode = function (key) {
                if (key === 13) {
                    var barcode = $scope.product.Barcode;
                    dataSource.getUrl("api/products", { barcode }).success(function (product) {
                        if (product) {
                            $scope.product = { Amount: 1.0, Price: 0.0, Name: product.Name, Id: product.Id, Barcode: barcode };
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
                    $scope.product = { Amount: 1.0, Price: 0.0, Name: selected.title, Id: selected.originalObject.Id, Barcode: "", Barcodes: [] };
                    $("#amount").focus();
                }
            }
            $scope.addProduct = function () {
                if (!$scope.product.Id) return;
                var productId = $scope.product.Id + "" + $scope.product.Price;
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
                $("#serialsModal").modal("show");
            }
            $scope.addSerial = function () {
                var item = $scope.transactionDetails[$scope.selectedIndex];
                item.Barcodes.push({ Barcode :{ Barcode : $scope.serial , ProductId : item.Id} });
                if (item.Barcodes.length > item.Amount)
                    item.Amount = item.Barcodes.length;
                $scope.serial = "";
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

                if (page.indexOf("Purchase") !== -1) {
                    dataSource.loadList($scope.suppliers, "api/suppliers");
                    dataSource.loadList($scope.points, "api/points?type=1");

                } else {
                    dataSource.loadList($scope.customers, "api/customers");
                    dataSource.loadList($scope.points, "api/points?type=1");
                }
                $("#serialsModal").modal({ show: false });
            };

            initialize();

        }]);
});
//Controller
