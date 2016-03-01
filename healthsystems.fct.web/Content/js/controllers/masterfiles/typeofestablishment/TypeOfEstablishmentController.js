app.controller("TypeOfEstablishmentController", function ($scope, $http, API_URL) {

    $scope.dataList = {};

    $http.get(API_URL + "/Api/TypeOfEstablishment")
		.success(function (data) {
		    $scope.dataList = data;
		});

});
