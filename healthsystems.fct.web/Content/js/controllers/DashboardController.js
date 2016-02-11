app.controller("DashboardController", function($scope, $state, DashboardFactory, AuthTokenFactory, LocalStorageFactory) {

    $scope.LoggedIn = false;
    if (AuthTokenFactory.getToken() == null) {
		LocalStorageFactory.setKey("lockout-reason", "No session");
		$state.go("login");
	} else {
	    $scope.LoggedIn = true;
	}

});