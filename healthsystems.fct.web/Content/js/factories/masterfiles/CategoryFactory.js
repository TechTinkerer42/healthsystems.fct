app.factory('CategoryFactory', function($http, API_URL) {
	'use strict';
	var scope = "Category";
	return {
		get: get,
		post: post
	};
	
	function get(paramId){
		var searchId = paramId != null ? paramId : "";
		return $http.get(API_URL + "/Api/" + scope + "/" + searchId);
	}

	function post(model) {
		return $http.post(API_URL + "/Api/" + scope, model);
	}
});