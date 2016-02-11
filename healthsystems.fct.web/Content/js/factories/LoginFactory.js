app.factory('LoginFactory', function LoginFactory($http, API_URL, AuthTokenFactory, LocalStorageFactory, $q) {
    "use strict";
	return {
		login: login,
		logout: logout
	};

	function login(username, password){
      return $http.post(API_URL + '/Api/Login', {
        username: username,
        password: password
      }).then(function success(response) {
        AuthTokenFactory.setToken(response.data.Token);
        LocalStorageFactory.setKey("lockout-reason");
        return response;
      });
	}

    function logout() {
      AuthTokenFactory.setToken();
    }
});