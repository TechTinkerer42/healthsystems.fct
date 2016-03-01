app.factory('ApiFactory', function($http, API_URL, $q) {
	'use strict';
	return {
		get: get,
		post: post
	};
	
	function get(scope, paramId) {

	    var deferred = $q.defer();

	    var searchId = paramId != null ? paramId : "";

	    $http.get(API_URL + "/Api/" + scope + "/" + searchId)
	        .success(function(data) {
	            deferred.resolve(data);
	        })
	        .error(function(data) {
	            deferred.reject(data);
	        });

	    return deferred.promise;
	}

	function post(scope, model) {
		return $http.post(API_URL + "/Api/" + scope, model);
	}
});