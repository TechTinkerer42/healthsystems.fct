app.controller("EnquiryController", function ($scope, $http, API_URL) {

	$scope.PaymentSearchResponse = {};
	$scope.siteRoot = API_URL;

	$scope.search = function () {

		$http.post(API_URL + "/Search/Enquiry", $scope.Search)
			.success(function(data) {
				$scope.PaymentSearchResponse = data;
			});
	}

});