app.controller("LocationMapController", function ($scope, LocationMapResource, LocationMapService, StringFactory, $state) {

    // *********************
    // Local Scope Functions
    // *********************
    function addMarker(lat, lng) {

        $scope.startPoint = new google.maps.LatLng(lat, lng);

        var marker = new google.maps.Marker({
            position: $scope.startPoint,
            icon: "http://maps.google.com/mapfiles/ms/icons/blue-dot.png"
        });

        marker.setMap(map);
    }

    function initialize(markers) {

        var labels = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        //var map;
        var bounds = new google.maps.LatLngBounds();

        // Display a map on the page
        map.setTilt(45);

        // Display multiple markers on a map
        var infoWindow = new google.maps.InfoWindow(), marker, i;

        // Loop through our array of markers & place each one on the map  
        for (i = 0; i < markers.length; i++) {

            var address = markers[i].address,
                lat = markers[i].lat,
                lng = markers[i].lng,
                html = markers[i].html;

            // define a lat/lng position
            var position = new google.maps.LatLng(lat, lng);

            // ?
            bounds.extend(position);

            // create a new marker
            marker = new google.maps.Marker({
                position: position,
                map: map,
                title: address
            });

            map.setCenter(marker.getPosition());

            var infowindow = new google.maps.InfoWindow();

            google.maps.event.addListener(marker, "click", (function (marker, html, infowindow) {
                return function () {
                    infowindow.setContent(html);
                    infowindow.open(map, marker);
                };
            })(marker, html, infowindow));


            // Automatically center the map fitting all markers on the screen
            map.fitBounds(bounds);
        }

    }

    // **********************
    // Scope definition start
    // **********************

    $scope.search = "abuja, nigeria";
    
    $scope.markers = [];
    $scope.searchCoordinatesResult = [];
    $scope.startPoint = null;

    var map = new google.maps.Map(document.getElementById("googleMap"), {
        mapTypeId: "roadmap"
    });

    var directionsService = new google.maps.DirectionsService;
    var directionsDisplay = new google.maps.DirectionsRenderer;

    directionsDisplay.setMap(map);
    directionsDisplay.setPanel(document.getElementById("right-panel"));

    LocationMapService.getExistingLocations().then(
        function(response){

            _.each(response.data, function(registration){
                var marker = {
                    address: registration.AddressLine1,
                    lat: registration.Latitude,
                    lng: registration.Longitude,
                    html: StringFactory.f("<div><h4>{0}</h4><p>{1}</p></div>", registration.EstablishmentName, registration.AddressLine1),
                    description: registration.EstablishmentName
                };

                $scope.markers.push(marker);

            });

            initialize($scope.markers);

        });

    $scope.searchClick = function () {

        LocationMapResource.get({ address: $scope.search }, {},

        function (response) {
            $scope.searchCoordinatesResult = response.results;
            console.log(response);
            addMarker(response.results[0].geometry.location.lat, response.results[0].geometry.location.lng);

        },

        function (response) {

            console.log(response);
            alert("Something went wrong! " + response);

        });
    }

    $scope.resetSearch = function() {
        $state.go("location-map");
    }

    $scope.calculateRoute = function() {
        $state.go("location-map");
    }

    function calculateAndDisplayRoute(directionsService, directionsDisplay, startPlace, endPlace) {
        directionsService.route({
            origin: startPlace,
            destination: endPlace,
            travelMode: google.maps.TravelMode.DRIVING
        }, function (response, status) {
            if (status === google.maps.DirectionsStatus.OK) {
                directionsDisplay.setDirections(response);
            } else {
                window.alert("Directions request failed due to " + status);
            }
        });
    }

    $scope.calculateRoute = function () {
        if ($scope.startPoint) {
            var endPoint = new google.maps.LatLng($scope.selectedMarker.lat, $scope.selectedMarker.lng);
            calculateAndDisplayRoute(directionsService, directionsDisplay, $scope.startPoint, endPoint);
        }
    }
});