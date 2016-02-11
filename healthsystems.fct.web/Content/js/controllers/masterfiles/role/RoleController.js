app.controller("RoleController", function ($scope, $http, API_URL) {

    $scope.dataList = {};

    $http.get(API_URL + "/Api/Role")
		.success(function (data) {
		    $scope.dataList = data;
		});

});
