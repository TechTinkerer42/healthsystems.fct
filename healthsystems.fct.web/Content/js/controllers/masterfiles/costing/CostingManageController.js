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