using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using healthsystems.fct.data;
using healthsystems.fct.common;
using healthsystems.fct.data.Common;

namespace healthsystems.fct.web
{
    public class PaymentController : Controller
    {
		[HttpPost]
		public ActionResult Transact(int renewalId, int paymentMethodId, decimal amountPaid, string receivedFrom)
		{
			using (var session = NHibernateHelper.CreateSessionFactory())
			{
				using (var transaction = session.BeginTransaction())
				{
					// find renewal based on id
					var renewals = new List<Renewal>(session.CreateCriteria(typeof (Renewal)).List<Renewal>());
					var renewalsToInsert = renewals.FirstOrDefault(x => x.Id == renewalId);

					// find transactionType
					var transactionType =
						new List<TransactionType>(
							session.CreateCriteria(typeof (TransactionType)).List<TransactionType>());
					var transactionTypeToInsert = transactionType.FirstOrDefault(x => x.Id == renewalsToInsert.RenewalType.Id);

					// find paymentType
					var paymentType =
						new List<PaymentType>(
							session.CreateCriteria(typeof(PaymentType)).List<PaymentType>());
					var paymentTypeToInsert = paymentType.FirstOrDefault(x => x.Id == paymentMethodId);


					if (AuthHelper.JwtAuth () != 1) {
						return Json ("Error: Not logged in");
					}

					var userId = AuthHelper.GetKey("userId");

                    var userToInsert = session.CreateCriteria(typeof(User)).List<User>().FirstOrDefault(x => x.Id.Equals(Convert.ToInt32(userId)));

					var newTransaction = new Transaction
					{
						Amount = amountPaid,
						Date = DateTime.Now,
						Renewal = renewalsToInsert,
						TransactionType = transactionTypeToInsert,
						PaymentType = paymentTypeToInsert,
						ReceivedFrom = receivedFrom,
						ReceivedBy = userToInsert
					};

					session.SaveOrUpdate(newTransaction);



					if (newTransaction.Id > 0)
					{

						var registrationId = newTransaction.Renewal.Registration.Id;
						var registration =
							new List<Registration>(session.CreateCriteria(typeof (Registration)).List<Registration>())
								.FirstOrDefault(x => x.Id == registrationId);
						if (registration != null)
						{
							if (string.IsNullOrEmpty(registration.PhermcRegistrationNumber))
							{
								registration.PhermcRegistrationNumber =
									new Random().Next(10000, 1000000).ToString();
								session.SaveOrUpdate(registration);                                
							}


						}

					}

					transaction.Commit();
					return Json(newTransaction.Id);
				}
			}

		}
    }
}
