app.factory("LocationMapService", function ($http, API_URL, AuthTokenFactory, LocalStorageFactory, $q) {
    "use strict";

    function getExistingLocations() {
        return $http.get(API_URL + "/Api/Registration");


    }

    return {
        getExistingLocations: getExistingLocations
    };
});