app.controller("TypeOfEstablishmentManageController", function ($scope, $state, $stateParams, ApiFactory) {

	$scope.action = "Create";
	$scope.id = $stateParams.id != null ? $stateParams.id : 0;
	
	if($scope.id > 0){
		$scope.action = "Edit";
	}

	ApiFactory.get("Staffing").then(function success(response) {
	    $scope.staff = response.data;
	}
	, handleError);

	ApiFactory.get("TypeOfEstablishment", $scope.id).then(function success(response) {
		$scope.model = response.data;

	}
	,handleError);

	$scope.save = save;
	$scope.addStaff = addStaff;
	$scope.removeStaff = removeStaff;

    function save() {

    	$("form").validator("validate");

    	if(!isFormValid()) {
    		return false;
    	}
		
		ApiFactory.post("TypeOfEstablishment", $scope.model).then(function success(response) {

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

    function addStaff() {
        var itemExist = _.findWhere($scope.model.Staffings, { Id: $scope.selectedStaff });
        if (itemExist) {
            toastr["warning"]("This item is already in the list!");
            return;
        }
        var itemToAdd = _.findWhere($scope.staff, { Id: $scope.selectedStaff });
        if (itemToAdd) {
            $scope.model.Staffings.push(itemToAdd);
        }
    }

    function removeStaff(array, index) {
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
