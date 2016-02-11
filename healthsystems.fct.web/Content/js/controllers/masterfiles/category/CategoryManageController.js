app.controller("CategoryManageController", function ($scope, $http, $state, $stateParams, CategoryFactory) {

    $scope.category = {};
	
	if($stateParams.id > 0) {
	    $scope.action = "Edit";
	}
	else {
	    $scope.action = "Create It";
	}

    CategoryFactory.get($stateParams.id).then(function success(response) {
        $scope.category = response.data;
    }, handleError);

    $scope.save = save;

    function save() {
		
		CategoryFactory.post($scope.category).then(function success(response) {
			$scope.category = response.data;
			
			$('#modal-success').modal({ backdrop: 'static' });
			$('#modal-success').modal('show');
			
		}, handleError);
		
    }

});