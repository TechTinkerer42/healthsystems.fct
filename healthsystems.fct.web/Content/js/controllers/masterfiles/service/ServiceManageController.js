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