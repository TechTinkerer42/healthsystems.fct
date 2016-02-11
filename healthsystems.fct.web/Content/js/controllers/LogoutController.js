app.controller("LogoutController", function($scope, $state, LoginFactory, AuthTokenFactory, LocalStorageFactory){

	AuthTokenFactory.setToken();

	$state.go("login");

});