app.controller("PermissionController", function ($scope, $http, API_URL) {

    $scope.dataList = {};

    $http.get(API_URL + "/Api/Permission")
		.success(function (data) {
		    $scope.dataList = data;
		});

});
