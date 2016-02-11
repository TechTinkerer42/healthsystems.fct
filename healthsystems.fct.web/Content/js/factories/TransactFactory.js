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