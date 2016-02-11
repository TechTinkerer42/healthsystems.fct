app.controller("OutstandingController", function ($scope, $http, API_URL) {

    $scope.OutstandingSearchResponse = {};

    $scope.search = function () {

        $http.post(API_URL + "/Search/Outstanding", $scope.Search)
            .success(function(data) {
                $scope.OutstandingSearchResponse = data;
            });
    }




});