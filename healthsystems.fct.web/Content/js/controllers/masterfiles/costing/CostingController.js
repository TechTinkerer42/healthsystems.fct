app.controller("CostingController", function ($scope, $http, API_URL) {

    $scope.dataList = {};

    $http.get(API_URL + "/Api/Costing")
		.success(function (data) {
		    $scope.dataList = data;
		});

});
