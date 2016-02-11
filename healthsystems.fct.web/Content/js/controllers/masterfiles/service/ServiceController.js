app.controller("ServiceController", function ($scope, $http, API_URL) {

    $scope.dataList = {};

    $http.get(API_URL + "/Api/Service")
		.success(function (data) {
		    $scope.dataList = data;
		});

});
