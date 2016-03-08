app.controller("RatingController", function ($scope, $http, ApiFactory) {

    $scope.max = 5;
    $scope.isReadonly = true;

    ApiFactory.get("Rating").then(function success(response) {

        $scope.rating = response;

        for (var i = 0; i < $scope.rating.length; i++) {
            var score = $scope.rating[i].TotalOfRatings / $scope.rating[i].NumberOfQuestions
            $scope.rating[i].score = score;
        }

    }, handleError);

});