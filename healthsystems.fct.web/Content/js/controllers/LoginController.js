app.controller("LoginController", function($scope, $state, LoginFactory, AuthTokenFactory, LocalStorageFactory) {

	$scope.LoginError = null;
    $scope.IsSigningIn = false;

	$('#myForm').validator().on('submit', function (e) {
	  if (e.isDefaultPrevented()) {
		  // invalid form
	  } else {
		login();
	  }
	});

	if(LocalStorageFactory.getKey("lockout-reason") != null){
		$scope.LoginError = "Your session has expired. Please log in again";
		LocalStorageFactory.setKey("lockout-reason");
	}

	$scope.login = login;

	function login() {
	    $scope.IsSigningIn = true;
		LoginFactory.login($scope.Username, $scope.Password).then(
		function success(response){
		    $scope.IsSigningIn = false;
			if(AuthTokenFactory.getToken != null){
				LocalStorageFactory.setKey("lockout-reason");
				$state.go("dashboard");
			}
		},
		function error(response) {
		    $scope.IsSigningIn = false;
			$scope.LoginError = response.data.Reason;
		});
	}				
});