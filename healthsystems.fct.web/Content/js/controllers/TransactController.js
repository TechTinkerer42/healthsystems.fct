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