app.controller("SearchController", function ($scope, $http, API_URL) {

	$scope.Search = {
		EstablishmentName : "",
		PhermcRegistrationNumber : "",
		CacNumber : "",
		Location : "",
		RegistrationDateFrom: "",
		RegistrationDateTo: "",
		LastRenewalDateFrom: "",
		LastRenewalDateTo: ""
	}

    $scope.IsSearching = false;
    $scope.RegistrationSearchResponse = {};

    $scope.search = function () {
		
		$scope.Search.RegistrationDateFrom = $scope.Search.RegistrationDateFrom != "" ? moment($scope.Search.RegistrationDateFrom, "DD/MM/YYYY").format() : "";
		$scope.Search.RegistrationDateTo = $scope.Search.RegistrationDateTo != "" ? moment($scope.Search.RegistrationDateTo, "DD/MM/YYYY").format() : "";
		$scope.Search.LastRenewalDateFrom = $scope.Search.LastRenewalDateFrom  != "" ? moment($scope.Search.LastRenewalDateFrom, "DD/MM/YYYY").format() : "";
		$scope.Search.LastRenewalDateTo = $scope.Search.LastRenewalDateTo != "" ? moment($scope.Search.LastRenewalDateTo, "DD/MM/YYYY").format() : "";


        $scope.IsSearching = true;
		
        $http.post(API_URL + "/Search/Establishment", $scope.Search)
            .success(function (data) {

                $scope.IsSearching = false;

                $scope.RegistrationSearchResponse = data;
				
				$scope.Search.RegistrationDateFrom = moment($scope.Search.RegistrationDateFrom).isValid() ? moment($scope.Search.RegistrationDateFrom).format("DD/MM/YYYY") : "";
				$scope.Search.RegistrationDateTo = moment($scope.Search.RegistrationDateTo).isValid() ? moment($scope.Search.RegistrationDateTo).format("DD/MM/YYYY") : "";
				$scope.Search.LastRenewalDateFrom = moment($scope.Search.LastRenewalDateFrom).isValid() ? moment($scope.Search.LastRenewalDateFrom).format("DD/MM/YYYY") : "";
				$scope.Search.LastRenewalDateTo = moment($scope.Search.LastRenewalDateTo).isValid() ? moment($scope.Search.LastRenewalDateTo).format("DD/MM/YYYY") : "";
				
            });
    }

});