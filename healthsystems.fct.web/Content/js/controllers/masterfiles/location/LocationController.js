app.controller("LocationController", function ($scope, $http, API_URL) {

    $scope.dataList = {};

    $http.get(API_URL + "/Api/Location")
		.success(function (data) {
		    $scope.dataList = data;
		});

});
