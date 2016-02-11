app.controller("UserController", function($scope, $state, ApiFactory){

    $scope.dataList = {};

	ApiFactory.get("User", "").then(function success(response) {
		$scope.dataList = response.data;	
	}
	,handleError);

});