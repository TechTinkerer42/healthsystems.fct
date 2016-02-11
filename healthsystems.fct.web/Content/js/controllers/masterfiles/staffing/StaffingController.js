app.controller("StaffingController", function ($scope, $http, API_URL) {

    $scope.dataList = {};

    $http.get(API_URL + "/Api/Staffing")
		.success(function (data) {
		    $scope.dataList = data;
		});

});
