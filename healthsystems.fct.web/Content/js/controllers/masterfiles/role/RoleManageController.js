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
