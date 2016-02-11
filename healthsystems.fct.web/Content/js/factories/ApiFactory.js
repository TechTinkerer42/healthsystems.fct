app.factory('ApiFactory', function($http, API_URL) {
	'use strict';
	return {
		get: get,
		post: post
	};
	
	function get(scope, paramId){
		var searchId = paramId != null ? paramId : "";
		return $http.get(API_URL + "/Api/" + scope + "/" + searchId);
	}

	function post(scope, model) {
		return $http.post(API_URL + "/Api/" + scope, model);
	}
});