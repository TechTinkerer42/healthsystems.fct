app.factory('StringFactory', function() {
	'use strict';
	return {
		f: f,
		post: post
	};
	
	function f (fmtstr) {
	  var args = Array.prototype.slice.call(arguments, 1);
	  return fmtstr.replace(/\{(\d+)\}/g, function (match, index) {
	    return args[index];
	  });
	}

	function post() {
	}
});