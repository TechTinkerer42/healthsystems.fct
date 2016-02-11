app.factory("LocationMapResource", function ($resource) {
    return $resource("http://maps.google.com/maps/api/geocode/json");
});