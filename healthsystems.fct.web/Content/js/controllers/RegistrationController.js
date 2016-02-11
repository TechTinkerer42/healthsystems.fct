app.controller("RegistrationController", function ($scope, $http, RegistrationFactory, CategoryFactory, LocationFactory, API_URL, $stateParams, LocationMapResource, StringFactory) {

    $scope.IsSaving = false;
    $scope.SaveButtonText = "Save Registration";
    $scope.TooltipText = "tooltip";
    $scope.allowPrint = false;
    $scope.New = false;
    $scope.Registration = {};
    $scope.Registration.Renewal = false;
    $scope.Id = $stateParams.id != null ? $stateParams.id : 0;

    $scope.Registration.AddressLine1 = "";
    $scope.Registration.AddressLine2 = "";
	
    // Collect Categories
    CategoryFactory.get().then(function success(response) {
        $scope.categories = response.data;
    }, handleError);

    // Collect Locations
    LocationFactory.get().then(function success(response) {
        $scope.stateLocations = response.data;
    }, handleError);

    if ($scope.Id > 0) {

        $scope.allowPrint = true;
        $scope.TooltipText = "";
        $scope.Action = "edit";
	}
    else {
        $scope.New = true;
    }
	
	// Retrieve the Registration
	RegistrationFactory.get($scope.Id).then(function success(response) {

		// Collect data
		$scope.Registration = response.data;

		// Repair date format - for ui
		$scope.Registration.RegistrationDate = moment($scope.Registration.RegistrationDate, moment.ISO_8601).format('DD/MM/YYYY');
		if($scope.Registration.RegistrationDate === "01/01/0001"){
			$scope.Registration.RegistrationDate = "";
		}

		// Category and Location
		if($scope.Id > 0){

            
		
			// Check whether a LastRenewalDate is present
			if ($scope.Registration.LastRenewalDate != null) {
				// Repair date format - for ui
				$scope.Registration.LastRenewalDate = moment($scope.Registration.LastRenewalDate, moment.ISO_8601).format('DD/MM/YYYY');
				// Is date C# MinDate?
				if ($scope.Registration.LastRenewalDate === "01/01/0001") {
					// Blank out the LastRenewalDate
					$scope.Registration.LastRenewalDate = "";
				}
			}
		}
	}, handleError);

    // function calls signatures
    // =========================
    $scope.submitButtonClick = submitButtonClick;
    $scope.successOKButtonClick = successOKButtonClick;
    $scope.saveOrUpdateRegistration = saveOrUpdateRegistration;

    // function definitions
    // ====================
	
    function submitButtonClick() {

        $("#myForm").validator("validate");

        var isFormValid = checkTabErrors();
        console.log(isFormValid);
        if (isFormValid) {
            $scope.saveOrUpdateRegistration();
        }

    }

    function successOKButtonClick() {
        //window.location = API_URL + "/Dashboard/Registration/" + $scope.Registration.Id;
    }

    function saveOrUpdateRegistration() {

        $scope.IsSaving = true;
        $scope.SaveButtonText = "Saving...";

        // Fix Date strings
        $scope.Registration.RegistrationDate = moment($scope.Registration.RegistrationDate, "DD/MM/YYYY").add(1, 'days').toDate().toUTCString();
        //$scope.Registration.LastRenewalDate = moment($scope.Registration.LastRenewalDate, "DD/MM/YYYY").add(1, 'days').toDate().toUTCString();

        // Initialize user object
        //$scope.Registration.UserId = user.Id;
        //$scope.Registration.Username = user.Username;
        //$scope.Registration.StateId = user.StateId;

        // get the latitude and longitude from the entered address
        // *******************************************************
        // concatenate address
        var addLine1 = $scope.Registration.AddressLine1 != "" ? $scope.Registration.AddressLine1 : "";
        var addLine2 = $scope.Registration.AddressLine2 != "" ? ", " + $scope.Registration.AddressLine2 : "";
        var concatedAddress = StringFactory.f("{0}{1}, {2}, Nigeria", addLine1 , addLine2, $scope.Registration.Location.Name);
        console.log(concatedAddress);

        // call google to get lat and lng
        LocationMapResource.get({ address: concatedAddress }, {},

            function (response) {
                try{

                    console.log(response);

                    if(response.results.length > 0) {
                        console.log("Hooray there is a result for a position");
                        var reg_lat = response.results[0].geometry.location.lat;
                        var reg_lng = response.results[0].geometry.location.lng;

                        console.log("lat: " + reg_lat);
                        console.log("lng: " + reg_lng);

                        $scope.Registration.Latitude = reg_lat;
                        $scope.Registration.Longitude = reg_lng;
                    }

                    console.log("about to save");
                    console.log("lat: " + $scope.Registration.Latitude );
                    console.log("lng: " + $scope.Registration.longitude );

                    // Post registration object
                    RegistrationFactory.post($scope.Registration, $scope.Registration.IsRenewal).then(function success(response) {

                        $scope.Registration = response.data;
                        $scope.IsSaving = false;
                        $scope.SaveButtonText = "Save Registration";

                        // Repair date format - for ui
                        $scope.Registration.RegistrationDate = moment($scope.Registration.RegistrationDate, moment.ISO_8601).format('DD/MM/YYYY');
                        if($scope.Registration.RegistrationDate === "01/01/0001"){
                            $scope.Registration.RegistrationDate = "";
                        }

                        // Show modal window
                        $('#modal-success').modal({ backdrop: 'static' });
                        $('#modal-success').modal('show');

                    },
                        handleError);
                }
                catch(err){
                    console.log("Error getting position. " + err.message)
                }
            }
        );

        // *************************************************************************************************



    } // save or update function

    // watch variables
    // ===============
    $scope.$watchCollection(
		"Registration.Renewal",
		function (newValue, oldValue) {
		    if (newValue == null) return;
		    if (newValue) {
		        $scope.Registration.isRenewal = true;
		        console.log("is renewal: " + $scope.Registration.isRenewal);
		    }
		    else {
		        $scope.successOKButtonClick();
		    }
		}
	);

});