app.controller("NavController", function($scope, $state, DashboardFactory, AuthTokenFactory, LocalStorageFactory){

	$scope.IsLoggedIn = false;

	if(AuthTokenFactory.getToken() == null){
		LocalStorageFactory.setKey("lockout-reason", "No session");
		$state.go("login");
	}else{
		$scope.IsLoggedIn = true;
	}

});