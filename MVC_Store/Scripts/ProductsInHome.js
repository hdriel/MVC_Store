var app = angular.module("MyProducts", []);
app.controller("Products_Searching", function ($scope, $http) {
    $scope.Product = {
        "IdProduct": "1",
        "Category": "",
        "ForKindAnimal": "",
        "Title": "",
        "Discription": "",
        "Price": ""
    };
    $scope.Products = {};
    $scope.loading = "load.";
    $scope.textSearch = "";

    $scope.Buy = function (p) {
        $scope.loading = "Acquisition process is executed, please wait...";
        $http({ method: "POST", data: p, url: "BuyProduct" })
            .success(function (data, status, headers, config) {
                $scope.Products = data;
                $scope.loading = "You bought your product!";
            });
    };

    $scope.Search = function () {
        $scope.Product.Title = $scope.textSearch;
        $scope.textSearch = "";
        $scope.Products = {};
        $scope.loading = "Loading data, please wait...";
        $http({ method: "POST", data: $scope.Product, url: "SearchByStringText" })
            .success(function (data, status, headers, config) {
                $scope.Products = data;
                $scope.loading = "";
            });
    };

    $scope.Delete = function (p) {
        $scope.textSearch = "";
        $scope.loading = "Remove product, please wait...";
        $http({ method: "POST", data: p, url: "DeleteProductFromDBByJson" })
            .success(function (data, status, headers, config) {
                $scope.Products = data;
                $scope.loading = "";
            });
    };
    

    $scope.Load = function () {
        $scope.loading = "Loading data, please wait...";
        $scope.textSearch = "";
        $scope.Products = {};
        $http({ method: "GET", url: "GetProductsByJson" })
            .success(function (data, status, headers, config) {
                $scope.Products = data;
                $scope.loading = "";
            });
    };
    $scope.Load();
});