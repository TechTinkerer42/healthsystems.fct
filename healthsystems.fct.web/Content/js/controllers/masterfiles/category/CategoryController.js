app.controller("CategoryController", function ($scope, $http, API_URL) {

    $scope.dataList = {};

    $http.get(API_URL + "/Api/Category")
		.success(function (data) {
		    $scope.dataList = data;
		});

});
