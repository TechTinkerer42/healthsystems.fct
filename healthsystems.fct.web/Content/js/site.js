
var TabErrorHtml = '&nbsp;<i class="fa fa-warning red" data-placement="top" title="This tab is missing some mandatory fields"></i>';

$(function () {
	
	var runCount = 0;
	
	// Enable tooltips
    $("[data-toggle=\"tooltip\"]").tooltip();
	
	// Make sure fields on all tabs are validated (https://github.com/1000hz/bootstrap-validator/issues/156)
	$.fn.validator.Constructor.INPUT_SELECTOR = ':input:not([type="submit"], button):enabled';
	
	$('#myForm input[required], #myForm select[required]').on("change", function(){
		checkTabErrors();
	});
	
	$( "form" ).on( "keydown", "input[data-type=number]", function(e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
             // Allow: Ctrl+A
            (e.keyCode === 65 && e.ctrlKey === true) ||
             // Allow: Ctrl+C
            (e.keyCode === 67 && e.ctrlKey === true) ||
             // Allow: Ctrl+X
            (e.keyCode === 88 && e.ctrlKey === true) ||
             // Allow: home, end, left, right
            (e.keyCode >= 35 && e.keyCode <= 39)) {
                 // let it happen, don't do anything
                 return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
	});

    // Apply input masks
    $(":input").inputmask();
	
	$(".input-daterange").datepicker({
	    format: "dd/mm/yyyy"
	});

	$("body").on("click", "a.tab-btn", function(e){
		e.preventDefault();
	});
	
});

function checkTabErrors(){
	
	var IsFormValid = true;
	
	var tabDirty = false;
	var tabsCss = "div.tab-pane";
	$(tabsCss).each( function(a, b){
		var helpersCss = "p.help-block.with-errors";
		var helpers = $(b).find(helpersCss);
		
		var tab = $(".nav.nav-tabs li a")[a];
		$(tab).html($(tab).attr("data-description"));
		
		$.each(helpers, function(c, d){
			
			if($(d).html() !== ""){
				IsFormValid = false;
				tabDirty = true;
			}
			
			if(tabDirty){
				// add icon on to tab
				console.log(b.id);
				var tab = $(".nav.nav-tabs li a")[a];
				$(tab).html($(tab).attr("data-description") + TabErrorHtml);
				tabDirty = false;
			}
			
		});
		
	});
	
	return IsFormValid;
}

function handleError(response){
	toastr["error"](response.data + response.status + response.header + response.config);
	console.log(response.data + response.status + response.header + response.config);
}

var app = angular.module("fct",
[
    "ui.router",
    "angular-ladda",
    "ngResource",
    "agGrid"

],
function config($httpProvider) {
	$httpProvider.interceptors.push("AuthInterceptorFactory");
});

app.constant("API_URL", SITE_URL);
app.config(function ($stateProvider, $urlRouterProvider) {

    var suffix = SITE_SUFFIX;

    // For any unmatched url, redirect to /state1
    $urlRouterProvider.otherwise("/login");

    // Now set up the states
    $stateProvider

        .state("login", {
            url: "/login",
            templateUrl: suffix + "/Content/views/login.html",
            controller: "LoginController"
        })
        .state("logout", {
            url: "/logout",
            templateUrl: suffix + "/Content/views/logout.html",
            controller: "LogoutController"
        })
        .state("dashboard", {
            url: "/dashboard",
            templateUrl: suffix + "/Content/views/dashboard.html",
            controller: "DashboardController"
        })
        .state("registration", {
            url: "/registration",
            templateUrl: suffix + "/Content/views/registration.html",
            controller: "RegistrationController"
        })
        .state("registration-edit", {
            url: "/registration/:id",
            templateUrl: suffix + "/Content/views/registration.html",
            controller: "RegistrationController"
        })
        .state("search", {
            url: "/search",
            templateUrl: suffix + "/Content/views/search.html",
            controller: "SearchController"
        })
        .state("payments", {
            url: "/payments",
            templateUrl: suffix + "/Content/views/payments.html"
        })
        .state("makepayment", {
            url: "/payments/makepayment",
            templateUrl: suffix + "/Content/views/makepayment.html",
            controller: "OutstandingController"
        })
        .state("transact", {
            url: "/payments/transact/:id",
            templateUrl: suffix + "/Content/views/transact.html",
            controller: "TransactController"
        })
        .state("enquiry", {
            url: "/payments/enquiry",
            templateUrl: suffix + "/Content/views/enquiry.html",
            controller: "EnquiryController"
        })
        .state("outstanding", {
            url: "/payments/outstanding",
            templateUrl: suffix + "/Content/views/outstanding.html",
            controller: "OutstandingController"
        })
        .state("location-map", {
            url: "/location-map",
            templateUrl: suffix + "/Content/views/location-map.html",
            controller: "LocationMapController"
        })
        .state("masterfiles", {
            url: "/masterfiles",
            templateUrl: suffix + "/Content/views/masterfiles.html",
            controller: "MasterfileController"
        })
        // ***********************************************************************
        .state("category", {
            url: "/masterfiles/category",
            templateUrl: suffix + "/Content/views/masterfiles/category/index.html",
            controller: "CategoryController"
        })
        .state("category-create", {
            url: "/masterfiles/category/create",
            templateUrl: suffix + "/Content/views/masterfiles/category/manage.html",
            controller: "CategoryManageController"
        })
        .state("category-edit", {
            url: "/masterfiles/category/:id",
            templateUrl: suffix + "/Content/views/masterfiles/category/manage.html",
            controller: "CategoryManageController"
        })
        // ***********************************************************************
        // ***********************************************************************
        .state("costing", {
            url: "/masterfile/costing",
            templateUrl: suffix + "/Content/views/masterfiles/costing/index.html",
            controller: "CostingController"
        })
        .state("costing-create", {
            url: "/masterfile/costing/create",
            templateUrl: suffix + "/Content/views/masterfiles/costing/manage.html",
            controller: "CostingManageController"
        })
        .state("costing-edit", {
            url: "/masterfile/costing/:id",
            templateUrl: suffix + "/Content/views/masterfiles/costing/manage.html",
            controller: "CostingManageController"
        })
        // ***********************************************************************
                // ***********************************************************************
        .state("location", {
            url: "/masterfile/location",
            templateUrl: suffix + "/Content/views/masterfiles/location/index.html",
            controller: "LocationController"
        })
        .state("location-create", {
            url: "/masterfile/location/create",
            templateUrl: suffix + "/Content/views/masterfiles/location/manage.html",
            controller: "LocationManageController"
        })
        .state("location-edit", {
            url: "/masterfile/location/:id",
            templateUrl: suffix + "/Content/views/masterfiles/location/manage.html",
            controller: "LocationManageController"
        })
        // ***********************************************************************
                // ***********************************************************************
        .state("permission", {
            url: "/masterfile/permission",
            templateUrl: suffix + "/Content/views/masterfiles/permission/index.html",
            controller: "PermissionController"
        })
        .state("permission-create", {
            url: "/masterfile/permission/create",
            templateUrl: suffix + "/Content/views/masterfiles/permission/manage.html",
            controller: "PermissionManageController"
        })
        .state("permission-edit", {
            url: "/masterfile/permission/:id",
            templateUrl: suffix + "/Content/views/masterfiles/permission/manage.html",
            controller: "PermissionManageController"
        })
        // ***********************************************************************
                // ***********************************************************************
        .state("role", {
            url: "/masterfile/role",
            templateUrl: suffix + "/Content/views/masterfiles/role/index.html",
            controller: "RoleController"
        })
        .state("role-create", {
            url: "/masterfile/role/create",
            templateUrl: suffix + "/Content/views/masterfiles/role/manage.html",
            controller: "RoleManageController"
        })
        .state("role-edit", {
            url: "/masterfile/role/:id",
            templateUrl: suffix + "/Content/views/masterfiles/role/manage.html",
            controller: "RoleManageController"
        })
        // ***********************************************************************
                // ***********************************************************************
        .state("service", {
            url: "/masterfile/service",
            templateUrl: suffix + "/Content/views/masterfiles/service/index.html",
            controller: "ServiceController"
        })
        .state("service-create", {
            url: "/masterfile/service/create",
            templateUrl: suffix + "/Content/views/masterfiles/service/manage.html",
            controller: "ServiceManageController"
        })
        .state("service-edit", {
            url: "/masterfile/service/:id",
            templateUrl: suffix + "/Content/views/masterfiles/service/manage.html",
            controller: "ServiceManageController"
        })
        // ***********************************************************************
        .state("staffing", {
            url: "/masterfile/staffing",
            templateUrl: suffix + "/Content/views/masterfiles/staffing/index.html",
            controller: "StaffingController"
        })
        .state("staffing-create", {
            url: "/masterfile/staffing/create",
            templateUrl: suffix + "/Content/views/masterfiles/staffing/manage.html",
            controller: "StaffingManageController"
        })
        .state("staffing-edit", {
            url: "/masterfile/staffing/:id",
            templateUrl: suffix + "/Content/views/masterfiles/staffing/manage.html",
            controller: "StaffingManageController"
        })
        // ***********************************************************************
        .state("user", {
            url: "/masterfile/user",
            templateUrl: suffix + "/Content/views/masterfiles/user/index.html",
            controller: "UserController"
        })
        .state("user-create", {
            url: "/masterfile/user/create",
            templateUrl: suffix + "/Content/views/masterfiles/user/manage.html",
            controller: "UserManageController"
        })
        .state("user-edit", {
            url: "/masterfile/user/:id",
            templateUrl: suffix + "/Content/views/masterfiles/user/manage.html",
            controller: "UserManageController"
        });
});
app.filter('moment', function () {
  return function (input, momentFn /*, param1, param2, ...param n */) {
    var args = Array.prototype.slice.call(arguments, 2),
        momentObj = moment(input);
    return momentObj[momentFn].apply(momentObj, args);
  };
});
app.filter('iif', function () {
   return function(input, trueValue, falseValue) {
        return input ? trueValue : falseValue;
   };
});
app.filter('total', function() {
    return function(input, property) {
        var i = input instanceof Array ? input.length : 0;
        if (typeof property === 'undefined' || i === 0) {
            return i;
        } else if (isNaN(input[0][property])) {
            throw 'filter total can count only numeric values';
        } else {
            var total = 0;
            while (i--)
                total += input[i][property];
            return total;
        }
    };
});
  app.factory('LocalStorageFactory', function LocalStorageFactory($window) {
    'use strict';
    var store = $window.localStorage;

    return {
      getKey: getKey,
      setKey: setKey
    };

    function getKey(key) {
      return store.getItem(key);
    }

    function setKey(key, value) {
      if (key && value) {
        store.setItem(key, value);
      } else {
        store.removeItem(key);
      }
    }

  });
  app.factory('AuthTokenFactory', function AuthTokenFactory($window) {
    'use strict';
    var store = $window.localStorage;
    var key = 'auth-token';

    return {
      getToken: getToken,
      setToken: setToken
    };

    function getToken() {
      return store.getItem(key);
    }

    function setToken(token) {
      if (token) {
        store.setItem(key, token);
      } else {
        store.removeItem(key);
      }
    }

  });
  app.factory("AuthInterceptorFactory", function AuthInterceptorFactory(AuthTokenFactory) {
    "use strict";
    return {
      request: addToken
    };

    function addToken(config) {
      var token = AuthTokenFactory.getToken();
      if (token) {
          config.headers = config.headers || {};
          if (config.url.indexOf("maps.google.com") === -1) { // if we are NOT interacting with google maps api via ajax
              config.headers.Authorization = "Bearer " + token; // add Authorization header
          }
      }
      return config;
    }
  });
app.factory('LoginFactory', function LoginFactory($http, API_URL, AuthTokenFactory, LocalStorageFactory, $q) {
    "use strict";
	return {
		login: login,
		logout: logout
	};

	function login(username, password){
      return $http.post(API_URL + '/Api/Login', {
        username: username,
        password: password
      }).then(function success(response) {
        AuthTokenFactory.setToken(response.data.Token);
        LocalStorageFactory.setKey("lockout-reason");
        return response;
      });
	}

    function logout() {
      AuthTokenFactory.setToken();
    }
});
app.factory("DashboardFactory", function($http, API_URL) {
    "use strict";
	return {
		
	}
});
app.factory('RegistrationFactory', function RegistrationFactory($http, API_URL) {
	'use strict';
	return {
		get: get,
		post: post
	};
	
	function get(id){
		return $http.get(API_URL + '/Api/Registration/' + id);
	}

	function post(registration, isRenewal) {
		var queryString = isRenewal ? "?r=1" : "";
		return $http.post(API_URL + '/Api/Registration' + queryString, registration);
	}
});
app.factory('TransactFactory', function TransactFactory($http, API_URL) {
	'use strict';
	return {
		payment: payment
	};

	function payment(renewalId, paymentMethodId, amountPaid, receivedFrom) {
		return $http.post(API_URL + '/Payment/Transact', {
			renewalId: renewalId,
			paymentMethodId: paymentMethodId,
			amountPaid: amountPaid,
			receivedFrom: receivedFrom
		});
	}
});
app.factory('ApiFactory', function($http, API_URL) {
	'use strict';
	return {
		get: get,
		post: post
	};
	
	function get(scope, paramId){
		var searchId = paramId != null ? paramId : "";
		return $http.get(API_URL + "/Api/" + scope + "/" + searchId);
	}

	function post(scope, model) {
		return $http.post(API_URL + "/Api/" + scope, model);
	}
});
app.factory("UserFactory", function($http, API_URL){
	'use strict';
	var scope = "User";
	return {
		get: get,
		post: post
	};
	
	function get(paramId){
		var searchId = paramId != null ? paramId : "";
		return $http.get(API_URL + "/Api/" + scope + "/" + searchId);
	}

	function post(model) {
		return $http.post(API_URL + "/Api/" + scope, model);
	}
});
app.factory('CategoryFactory', function($http, API_URL) {
	'use strict';
	var scope = "Category";
	return {
		get: get,
		post: post
	};
	
	function get(paramId){
		var searchId = paramId != null ? paramId : "";
		return $http.get(API_URL + "/Api/" + scope + "/" + searchId);
	}

	function post(model) {
		return $http.post(API_URL + "/Api/" + scope, model);
	}
});
app.factory('LocationFactory', function($http, API_URL) {
	'use strict';
	var scope = "Location";
	return {
		get: get,
		post: post
	};
	
	function get(paramId){
		var searchId = paramId != null ? paramId : "";
		return $http.get(API_URL + "/Api/" + scope + "/" + searchId);
	}

	function post(model) {
		return $http.post(API_URL + "/Api/" + scope, model);
	}
});
app.factory("LocationMapService", function ($http, API_URL, AuthTokenFactory, LocalStorageFactory, $q) {
    "use strict";

    function getExistingLocations() {
        return $http.get(API_URL + "/Api/Registration");


    }

    return {
        getExistingLocations: getExistingLocations
    };
});
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
app.factory("LocationMapResource", function ($resource) {
    return $resource("http://maps.google.com/maps/api/geocode/json");
});
app.controller("NavController", function($scope, $state, DashboardFactory, AuthTokenFactory, LocalStorageFactory){

	$scope.IsLoggedIn = false;

	if(AuthTokenFactory.getToken() == null){
		LocalStorageFactory.setKey("lockout-reason", "No session");
		$state.go("login");
	}else{
		$scope.IsLoggedIn = true;
	}

});
app.controller("LogoutController", function($scope, $state, LoginFactory, AuthTokenFactory, LocalStorageFactory){

	AuthTokenFactory.setToken();

	$state.go("login");

});
app.controller("LoginController", function($scope, $state, LoginFactory, AuthTokenFactory, LocalStorageFactory) {

	$scope.LoginError = null;
    $scope.IsSigningIn = false;

	$('#myForm').validator().on('submit', function (e) {
	  if (e.isDefaultPrevented()) {
		  // invalid form
	  } else {
		login();
	  }
	});

	if(LocalStorageFactory.getKey("lockout-reason") != null){
		$scope.LoginError = "Your session has expired. Please log in again";
		LocalStorageFactory.setKey("lockout-reason");
	}

	$scope.login = login;

	function login() {
	    $scope.IsSigningIn = true;
		LoginFactory.login($scope.Username, $scope.Password).then(
		function success(response){
		    $scope.IsSigningIn = false;
			if(AuthTokenFactory.getToken != null){
				LocalStorageFactory.setKey("lockout-reason");
				$state.go("dashboard");
			}
		},
		function error(response) {
		    $scope.IsSigningIn = false;
			$scope.LoginError = response.data.Reason;
		});
	}				
});
app.controller("DashboardController", function($scope, $state, DashboardFactory, AuthTokenFactory, LocalStorageFactory) {

    $scope.LoggedIn = false;
    if (AuthTokenFactory.getToken() == null) {
		LocalStorageFactory.setKey("lockout-reason", "No session");
		$state.go("login");
	} else {
	    $scope.LoggedIn = true;
	}

});
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
app.controller("MasterfileController", function($scope, $state, LoginFactory){

});
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
app.controller("OutstandingController", function ($scope, $http, API_URL) {

    $scope.OutstandingSearchResponse = {};

    $scope.search = function () {

        $http.post(API_URL + "/Search/Outstanding", $scope.Search)
            .success(function(data) {
                $scope.OutstandingSearchResponse = data;
            });
    }




});
app.controller("EnquiryController", function ($scope, $http, API_URL) {

	$scope.PaymentSearchResponse = {};

	$scope.search = function () {

		$http.post(API_URL + "/Search/Enquiry", $scope.Search)
			.success(function(data) {
				$scope.PaymentSearchResponse = data;
			});
	}

});
app.controller("TransactController", function ($scope, $http, API_URL, TransactFactory, $stateParams, $state) {

	$scope.AmountPaidZeroMessage = "";
	$scope.Renewal = {
		AmountDue: 0,
		TotalPaid: 0,
		Balance: 0
	};
    $scope.Payment = {
		
		PaymentMethodId : "",
		AmountPaid : 0,
		ReceivedFrom : ""
		
    };

    $scope.SuccessTransactionId = 0;
	$scope.HidePaymentSection = false;
	$scope.Saving = false;
	
	$('#myForm').validator().on('submit', function (e) {
	  if (e.isDefaultPrevented()) {
		  // invalid form
	  } else {
		payNow();
	  }
	});
	
	// function signatures
	$scope.payNow = payNow;
	$scope.successOKButtonClick = successOKButtonClick;
	
	// function definitions
    function payNow() {

        if ($scope.Payment.AmountPaid == 0) {
            $scope.AmountPaidZeroMessage = "Please enter an amount";
            return;
        }

        if ($scope.Payment.AmountPaid > $scope.Renewal.Balance) {
            $scope.AmountPaidZeroMessage = "Amount cannot be greater than the outstanding amount";
            return;
        }

        $scope.AmountPaidZeroMessage = "";

        $scope.Saving = true;

        TransactFactory.payment(
                $scope.Renewal.RenewalId,
                $scope.Payment.PaymentMethodId,
                $scope.Payment.AmountPaid,
                $scope.Payment.ReceivedFrom
            )
            .then(function success(response) {

                    $scope.SuccessTransactionId = response.data;

                    $scope.Saving = false;

                    // Show modal window
                    $('#modal-transaction-success').modal({ backdrop: 'static' });
                    $('#modal-transaction-success').modal('show');

                },
                handleError);
    }

    function successOKButtonClick(){
    	$('#modal-transaction-success').modal('hide');
    	$('.modal-backdrop').hide();
        $state.go("payments")
	}
	
	function handleError(response){
		console.log(response.data + response.status + response.header + response.config);
	}	

    if($stateParams.id > 0) {
		
		// Get renewal info
        $http.get(API_URL + "/Api/Renewal/" + $stateParams.id)
            .success(function(data) {
                $scope.Renewal = data[0];
				if($scope.Renewal.Balance == 0){
					$scope.HidePaymentSection = true;
				}
				
            });
    }
	
	// watch variables
	// ===============
	$scope.$watchCollection(
		"Payment.AmountPaid",
		function( newValue, oldValue ) {
			if(newValue == null) return;
			if(newValue){
				$scope.AmountPaidZeroMessage = "";
			}
		}
	);

});
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
app.controller("CategoryController", function ($scope, $http, API_URL) {

    $scope.dataList = {};

    $http.get(API_URL + "/Api/Category")
		.success(function (data) {
		    $scope.dataList = data;
		});

});

app.controller("CategoryManageController", function ($scope, $http, $state, $stateParams, CategoryFactory) {

    $scope.category = {};
	
	if($stateParams.id > 0) {
	    $scope.action = "Edit";
	}
	else {
	    $scope.action = "Create It";
	}

    CategoryFactory.get($stateParams.id).then(function success(response) {
        $scope.category = response.data;
    }, handleError);

    $scope.save = save;

    function save() {
		
		CategoryFactory.post($scope.category).then(function success(response) {
			$scope.category = response.data;
			
			$('#modal-success').modal({ backdrop: 'static' });
			$('#modal-success').modal('show');
			
		}, handleError);
		
    }

});
app.controller("CostingController", function ($scope, $http, API_URL) {

    $scope.dataList = {};

    $http.get(API_URL + "/Api/Costing")
		.success(function (data) {
		    $scope.dataList = data;
		});

});

app.controller("CostingManageController", function ($scope, $state, $stateParams, ApiFactory) {

	$scope.noCategoriesError = "";
	$scope.action = "Create";
	$scope.id = $stateParams.id != null ? $stateParams.id : 0;
	
	if($scope.id > 0){
		$scope.action = "Edit";
	}

	ApiFactory.get("Category").then(function success(response) {
		$scope.categories = response.data;
	}
	,handleError);

	// if we are creating, then remove all the categories that has costing info
	if($scope.action == "Create"){

		ApiFactory.get("Costing").then(function success(response) {
			$scope.costings = response.data;

			_.each($scope.costings, function(obj){

				for (var i = $scope.categories.length - 1; i > -1; i--) {
				    if ( $scope.categories[i].Id === obj.Category.Id)
				        $scope.categories.splice(i, 1);
				}

			});

			if( $scope.categories.length == 0 ){
				$scope.noCategoriesError = "Please first add some categories which does not having any costing attached, then try to add a new costing.";
			}
		}
		,handleError);
	}



	ApiFactory.get("Costing", $scope.id).then(function success(response) {
		$scope.model = response.data;

	}
	,handleError);

    $scope.save = save;

    function save() {

    	$("form").validator("validate");

    	if(!isFormValid()) {
    		return false;
    	}
		
		ApiFactory.post("Costing", $scope.model).then(function success(response) {

			if(response.data.Error) {
				toastr["warning"](response.data.ErrorMessage);
			}
			else {
				$scope.model = response.data;

					$('#modal-success').modal({ backdrop: 'static' });
					$('#modal-success').modal('show');

			}
			
		}, handleError);
		
    }



	function isFormValid() {

		var formValid = true;
		var helpersCss = "p.help-block.with-errors";
		var helpers = $("form").find(helpersCss);
		$.each(helpers, function(c, d){
			
			if($(d).html() != ""){
				formValid = false;
			}

		});

		return formValid;
	}

});
app.controller("LocationController", function ($scope, $http, API_URL) {

    $scope.dataList = {};

    $http.get(API_URL + "/Api/Location")
		.success(function (data) {
		    $scope.dataList = data;
		});

});

app.controller("LocationManageController", function ($scope, $state, $stateParams, ApiFactory) {

	$scope.action = "Create";
	$scope.id = $stateParams.id != null ? $stateParams.id : 0;
	
	if($scope.id > 0){
		$scope.action = "Edit";
	}

	ApiFactory.get("Location", $scope.id).then(function success(response) {
		$scope.model = response.data;

	}
	,handleError);

    $scope.save = save;

    function save() {

    	$("form").validator("validate");

    	if(!isFormValid()) {
    		return false;
    	}
		
		ApiFactory.post("Location", $scope.model).then(function success(response) {

			if(response.data.Error) {
				toastr["warning"](response.data.ErrorMessage);
			}
			else {
				$scope.model = response.data;

					$('#modal-success').modal({ backdrop: 'static' });
					$('#modal-success').modal('show');

			}
			
		}, handleError);
		
    }



	function isFormValid() {

		var formValid = true;
		var helpersCss = "p.help-block.with-errors";
		var helpers = $("form").find(helpersCss);
		$.each(helpers, function(c, d){
			
			if($(d).html() != ""){
				formValid = false;
			}

		});

		return formValid;
	}

});
app.controller("PermissionController", function ($scope, $http, API_URL) {

    $scope.dataList = {};

    $http.get(API_URL + "/Api/Permission")
		.success(function (data) {
		    $scope.dataList = data;
		});

});

app.controller("PermissionManageController", function ($scope, $state, $stateParams, ApiFactory) {

	$scope.action = "Create";
	$scope.id = $stateParams.id != null ? $stateParams.id : 0;
	
	if($scope.id > 0){
		$scope.action = "Edit";
	}

	ApiFactory.get("Permission", $scope.id).then(function success(response) {
		$scope.model = response.data;

	}
	,handleError);

    $scope.save = save;

    function save() {

    	$("form").validator("validate");

    	if(!isFormValid()) {
    		return false;
    	}
		
		ApiFactory.post("Permission", $scope.model).then(function success(response) {

			if(response.data.Error) {
				toastr["warning"](response.data.ErrorMessage);
			}
			else {
				$scope.model = response.data;

					$('#modal-success').modal({ backdrop: 'static' });
					$('#modal-success').modal('show');

			}
			
		}, handleError);
		
    }



	function isFormValid() {

		var formValid = true;
		var helpersCss = "p.help-block.with-errors";
		var helpers = $("form").find(helpersCss);
		$.each(helpers, function(c, d){
			
			if($(d).html() != ""){
				formValid = false;
			}

		});

		return formValid;
	}

});
app.controller("RoleController", function ($scope, $http, API_URL) {

    $scope.dataList = {};

    $http.get(API_URL + "/Api/Role")
		.success(function (data) {
		    $scope.dataList = data;
		});

});

app.controller("RoleManageController", function ($scope, $state, $stateParams, ApiFactory) {

	$scope.action = "Create";
	$scope.selectedPermission = 0;
	$scope.id = $stateParams.id != null ? $stateParams.id : 0;
	$scope.blankRolePermission = {
      Id:0,
      Role:{},
      Permission:{}
	};
	
	if($scope.id > 0){
		$scope.action = "Edit";
	}

	ApiFactory.get("Permission").then(function success(response) {
		$scope.permissions = response.data;
	}
	,handleError);

	ApiFactory.get("Role", $scope.id).then(function success(response) {
		$scope.model = response.data;

		if($scope.id != null) {
			ApiFactory.get("PermissionsOfRole", $scope.id).then(function success(response) {
				$scope.rolePermissions = response.data;
			}
			,handleError);
		}
	}
	,handleError);

    $scope.save = save;
    $scope.addPermission = addPermission;
    $scope.removePermission = removePermission;

    function save() {

    	$("form").validator("validate");

    	if(!isFormValid()) {
    		return false;
    	} else {

			ApiFactory.post("Role", $scope.model).then(function success(response) {

				if(response.data.Error) {
					toastr["warning"](response.data.ErrorMessage);
				}
				else {
					$scope.model = response.data;

					_.each($scope.rolePermissions, function(obj){
						obj.Role.Id = $scope.model.Id;
					});

					ApiFactory.post("RolePermission" + "?roleId=" + $scope.model.Id, $scope.rolePermissions).then(function success(response) {
						$scope.rolePermissions = response.data;

						$('#modal-success').modal({ backdrop: 'static' });
						$('#modal-success').modal('show');

					}
					,handleError);

				}
				
			}, handleError);

    	}
		
    }

    function addPermission() {
    	var itemExist = false;

		for(i = 0; i < $scope.rolePermissions.length; i++) {
		  var rp = $scope.rolePermissions[i];
		  if(rp.Permission.Id == $scope.selectedPermission) {
		  	itemExist = true;
		  }
		}

		if(itemExist){
			toastr["warning"]("This item is already in the list!");
			return;
		}
		var permissionToAdd = _.findWhere($scope.permissions, {Id: $scope.selectedPermission});
		if(permissionToAdd){
			var newRolePermission = {
				Id: 0,
				Role: $scope.model,
				Permission: permissionToAdd
			};
			$scope.rolePermissions.push(newRolePermission);
		}
    }
	
	function removePermission(array, index){
		array.splice(index, 1);
	}

	function isFormValid() {

		var formValid = true;
		var helpersCss = "div.help-block.with-errors";
		var helpers = $("form").find(helpersCss);
		$.each(helpers, function(c, d){
			
			if($(d).html() != ""){
				formValid = false;
			}

		});

		return formValid;
	}

});

app.controller("ServiceController", function ($scope, $http, API_URL) {

    $scope.dataList = {};

    $http.get(API_URL + "/Api/Service")
		.success(function (data) {
		    $scope.dataList = data;
		});

});

app.controller("ServiceManageController", function ($scope, $state, $stateParams, ApiFactory) {

	$scope.action = "Create";
	$scope.id = $stateParams.id != null ? $stateParams.id : 0;
	
	if($scope.id > 0){
		$scope.action = "Edit";
	}

	ApiFactory.get("Service", $scope.id).then(function success(response) {
		$scope.model = response.data;

	}
	,handleError);

    $scope.save = save;

    function save() {

    	$("form").validator("validate");

    	if(!isFormValid()) {
    		return false;
    	}
		
		ApiFactory.post("Service", $scope.model).then(function success(response) {

			if(response.data.Error) {
				toastr["warning"](response.data.ErrorMessage);
			}
			else {
				$scope.model = response.data;

					$('#modal-success').modal({ backdrop: 'static' });
					$('#modal-success').modal('show');

			}
			
		}, handleError);
		
    }



	function isFormValid() {

		var formValid = true;
		var helpersCss = "p.help-block.with-errors";
		var helpers = $("form").find(helpersCss);
		$.each(helpers, function(c, d){
			
			if($(d).html() != ""){
				formValid = false;
			}

		});

		return formValid;
	}

});
app.controller("StaffingController", function ($scope, $http, API_URL) {

    $scope.dataList = {};

    $http.get(API_URL + "/Api/Staffing")
		.success(function (data) {
		    $scope.dataList = data;
		});

});

app.controller("StaffingManageController", function ($scope, $state, $stateParams, ApiFactory) {

	$scope.action = "Create";
	$scope.id = $stateParams.id != null ? $stateParams.id : 0;
	
	if($scope.id > 0){
		$scope.action = "Edit";
	}

	ApiFactory.get("Staffing", $scope.id).then(function success(response) {
		$scope.model = response.data;

	}
	,handleError);

    $scope.save = save;

    function save() {

    	$("form").validator("validate");

    	if(!isFormValid()) {
    		return false;
    	}
		
		ApiFactory.post("Staffing", $scope.model).then(function success(response) {

			if(response.data.Error) {
				toastr["warning"](response.data.ErrorMessage);
			}
			else {
				$scope.model = response.data;

					$('#modal-success').modal({ backdrop: 'static' });
					$('#modal-success').modal('show');

			}
			
		}, handleError);
		
    }



	function isFormValid() {

		var formValid = true;
		var helpersCss = "p.help-block.with-errors";
		var helpers = $("form").find(helpersCss);
		$.each(helpers, function(c, d){
			
			if($(d).html() != ""){
				formValid = false;
			}

		});

		return formValid;
	}

});

app.controller("UserController", function($scope, $state, ApiFactory){

    $scope.dataList = {};

	ApiFactory.get("User", "").then(function success(response) {
		$scope.dataList = response.data;	
	}
	,handleError);

});
app.controller("UserManageController", function($scope, $state, $stateParams, ApiFactory){

	$scope.action = "Create";
	$scope.selectedRole = 0;
	$scope.id = $stateParams.id != null ? $stateParams.id : 0;
	
	if($scope.id > 0){
		$scope.action = "Edit";
	}

	ApiFactory.get("Role").then(function success(response) {
		$scope.roles = response.data;
	}
	,handleError);

	ApiFactory.get("User", $scope.id).then(function success(response) {
		$scope.model = response.data;
	}
	,handleError);

    $scope.save = save;
    $scope.addRole = addRole;
    $scope.removeRole = removeRole;

    function save() {

    	$("form").validator("validate");

    	if(!isFormValid()) {
    		return false;
    	}
		
		ApiFactory.post("User", $scope.model).then(function success(response) {

			if(response.data.Error) {
				toastr["warning"](response.data.ErrorMessage);
			}
			else {
				$scope.model = response.data;
				
				$('#modal-success').modal({ backdrop: 'static' });
				$('#modal-success').modal('show');

			}
			
		}, handleError);
		
    }

    function addRole() {
		var itemExist = _.findWhere($scope.model.Roles, { Id: $scope.selectedRole});
		if(itemExist){
			toastr["warning"]("This item is already in the list!");
			return;
		}
		var roleToAdd = _.findWhere($scope.roles, {Id: $scope.selectedRole});
		if(roleToAdd){
			$scope.model.Roles.push(roleToAdd);
		}
    }
	
	function removeRole(array, index){
		array.splice(index, 1);
	}

	function isFormValid() {

		var formValid = true;
		var helpersCss = "p.help-block.with-errors";
		var helpers = $("form").find(helpersCss);
		$.each(helpers, function(c, d){
			
			if($(d).html() != ""){
				formValid = false;
			}

		});

		return formValid;
	}

});