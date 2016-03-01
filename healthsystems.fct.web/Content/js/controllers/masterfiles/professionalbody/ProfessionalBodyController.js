app.controller("ProfessionalBodyController", function ($scope, $http, API_URL) {

    $scope.dataList = {};

    $http.get(API_URL + "/Api/ProfessionalBody")
		.success(function (data) {
		    $scope.dataList = data;
		});

});
