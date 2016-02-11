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