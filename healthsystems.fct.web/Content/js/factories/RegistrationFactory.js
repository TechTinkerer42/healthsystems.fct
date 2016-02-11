app.factory('RegistrationFactory', function RegistrationFactory($http, API_URL) {
	'use strict';
	return {
		get: get,
		post: post
	};
	
	function get(id){
		return $http.get(API_URL + '/Api/Registration/' + id);
	}

	function post(registration, isRenewal) {
		var queryString = isRenewal ? "?r=1" : "";
		return $http.post(API_URL + '/Api/Registration' + queryString, registration);
	}
});