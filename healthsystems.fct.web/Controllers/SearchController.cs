using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using healthsystems.fct.data;
using healthsystems.fct.common;
using healthsystems.fct.data.Common;

namespace healthsystems.fct.web
{
	public class SearchController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Establishment(EstablishmentSearch search)
		{
			using (var session = NHibernateHelper.CreateSessionFactory())
			{
				using (var transaction = session.BeginTransaction())
				{
					var registrations = new List<Registration>(session.CreateCriteria(typeof(Registration)).List<Registration>());

					if (!String.IsNullOrEmpty(search.EstablishmentName))
					{
						registrations = registrations.Where(s => s.EstablishmentName.ToLower().Contains(search.EstablishmentName.ToLower())).ToList();
					}

					if (!String.IsNullOrEmpty(search.PhermcRegistrationNumber))
					{
						registrations = registrations.Where(s => s.PhermcRegistrationNumber.ToLower().Contains(search.PhermcRegistrationNumber.ToLower())).ToList();
					}

					if (!String.IsNullOrEmpty(search.CacNumber))
					{
						registrations = registrations.Where(s => s.CacNumber.ToLower().Contains(search.CacNumber.ToLower())).ToList();
					}

					if (!String.IsNullOrEmpty(search.Location))
					{
						registrations = registrations.Where(s => s.CacNumber.ToLower().Contains(search.Location.ToLower())).ToList();
					}

					registrations = registrations.Where(s => s.RegistrationDate >= search.RegistrationDateFrom.DayMin()).ToList();

					if (search.RegistrationDateTo.Equals(DateTime.MinValue))
					{
						registrations = registrations.Where(s => s.RegistrationDate <= DateTime.MaxValue).ToList();
					}
					else
					{
						registrations = registrations.Where(s => s.RegistrationDate <= search.RegistrationDateTo.DayMax()).ToList();
					}

                    /*
					registrations = registrations.Where(s => s.LastRenewalDate >= search.LastRenewalDateFrom.DayMin()).ToList();

					if (search.LastRenewalDateTo.Equals(DateTime.MinValue))
					{
						registrations = registrations.Where(s => s.LastRenewalDate <= DateTime.MaxValue).ToList();
					}
					else
					{
						registrations = registrations.Where(s => s.LastRenewalDate <= search.LastRenewalDateTo.DayMax()).ToList();
					}*/

					registrations.ForEach(x =>
						{
							//if (x.State != null) x.State.Locations = null;
						});

					return Content(WebApiHelper.ObjectToJsonString(registrations), "application/json");

				}
			}
		}

		[HttpPost]
		public ActionResult Payment(PaymentSearch search)
		{
			using (var session = NHibernateHelper.CreateSessionFactory())
			{
				using (var transaction = session.BeginTransaction())
				{

					var payments = session.CreateCriteria(typeof(Transaction)).List<Transaction>();

					if (!string.IsNullOrEmpty(search.PaymentMethod))
					{
						var paymentMethodId = Convert.ToInt32(search.PaymentMethod);
						payments = payments.Where(s => s.PaymentType.Id.Equals(paymentMethodId)).ToList();
					}

					if (!string.IsNullOrEmpty(search.ReceivedByName))
					{
						payments = payments.Where(s => s.ReceivedBy.Username.ToLower().Contains(search.ReceivedByName.ToLower())).ToList();
					}

					if (!string.IsNullOrEmpty(search.EstablishmentName))
					{
						payments = payments.Where(s => s.Renewal.Registration.EstablishmentName.ToLower().Contains(search.EstablishmentName.ToLower())).ToList();
					}


					payments = payments.Where(s => s.Date >= search.CreatedFrom).ToList();

					if (search.CreatedTo.Equals(DateTime.MinValue))
					{
						payments = payments.Where(s => s.Date <= DateTime.MaxValue).ToList();
					}
					else
					{
						payments = payments.Where(s => s.Date <= search.CreatedTo).ToList();
					}


					var jsonResult =
						from x in payments
						select new
					{
						Search = x
							/*Id = x.Id,
                            Created = x.Date,
                            PaymentMethod = x.PaymentType.Name,
                            ReceivedFrom = x.ReceivedBy,
                            ReceivedByName = x.ReceivedBy.Username,
                            x.Amount,
                            x.Renewal.Registration.EstablishmentName*/
					};

					return Json(jsonResult);

				}
			}
		}

		[HttpPost]
		public ActionResult Enquiry(PaymentSearch search)
		{
			var sqlPath = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/sql/payments.txt");

			using (var session = NHibernateHelper.CreateSessionFactory())
			{
				using (var transaction = session.BeginTransaction())
				{

					var sql = System.IO.File.ReadAllText(sqlPath);

					var data =
						from x in session.CreateSQLQuery(sql).DynamicList()
						select new
					{
						x.Id,
						x.ReceivedFrom,
						x.Date,
						x.Amount,
						x.PaymentType,
						x.TransactionType,
						x.Renewal_id,
						x.Username,
						x.EstablishmentName
					};

					var enquirySearchList = new List<EnquirySearch>();

					if (data.Any())
					{
						foreach (var d in data)
						{
							var enquirySearch = new EnquirySearch
							{
								Id = d.Id,
								ReceivedFrom = d.ReceivedFrom,
								Date = d.Date,
								Amount = d.Amount,
								PaymentType = d.PaymentType,
								TransactionType = d.TransactionType,
								ReceivedBy = d.Username,
								EstablishmentName = d.EstablishmentName
							};
							enquirySearchList.Add(enquirySearch);
						}
					}

					var jsonResult =
						from x in enquirySearchList
						select x;

					return Json(jsonResult);

				}
			}
		}

		[HttpPost]
		public ActionResult Outstanding(OutstandingSearch search)
		{
			var sqlPath = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/sql/renewals.txt");

			using (var session = NHibernateHelper.CreateSessionFactory())
			{
				using (var transaction = session.BeginTransaction())
				{

					var sql = System.IO.File.ReadAllText(sqlPath);

					var renewalsData =
						from x in session.CreateSQLQuery(sql).DynamicList()
						select new
					{
						x.RenewalId,
						x.CacNumber,
						x.EstablishmentName,
						x.Date,
						x.RenewalTypeId,
						x.AmountDue,
						x.TotalPaid,
						x.Balance
					};

					var renewalSearchList = new List<RenewalSearch>();

					if (renewalsData.Any())
					{
						foreach (var r in renewalsData)
						{
							var renewalSearchItem = new RenewalSearch
							{
								AmountDue = r.AmountDue,
								Balance = r.Balance,
								CacNumber = r.CacNumber,
								Date = r.Date,
								EstablishmentName = r.EstablishmentName,
								RenewalId = r.RenewalId,
								RenewalTypeId = r.RenewalTypeId,
								TotalPaid = r.TotalPaid
							};
							renewalSearchList.Add(renewalSearchItem);
						}
					}

					if (!string.IsNullOrEmpty(search.EstablishmentName))
					{
						renewalSearchList = renewalSearchList.Where(s => s.EstablishmentName.ToLower().Contains(search.EstablishmentName.ToLower())).ToList();
					}

					// filter to only show outstanding ones
					renewalSearchList = renewalSearchList.Where(s => s.AmountDue > 0).ToList();

					var jsonResult =
						from x in renewalSearchList
						select x;

					return Json(jsonResult);

				}
			}
		}

	}

	public class EnquirySearch
	{
		public long Id { get; set; }
		public string ReceivedFrom { get; set; }
		public DateTime Date { get; set; }
		public decimal Amount { get; set; }
		public string PaymentType { get; set; }
		public string TransactionType { get; set; }
		public string ReceivedBy { get; set; }
		public string EstablishmentName { get; set; }
	}

	public class EstablishmentSearch
	{
		public string EstablishmentName { get; set; }
		public string PhermcRegistrationNumber { get; set; }
		public string CacNumber { get; set; }
		public string Location { get; set; }
		public DateTime RegistrationDateFrom { get; set; }
		public DateTime RegistrationDateTo { get; set; }
		public DateTime LastRenewalDateFrom { get; set; }
		public DateTime LastRenewalDateTo { get; set; }
	}

	public class PaymentSearch
	{
		public DateTime CreatedFrom { get; set; }
		public DateTime CreatedTo { get; set; }
		public string PaymentMethod { get; set; }
		public string ReceivedByName { get; set; }
		public string EstablishmentName { get; set; }
	}

	public class OutstandingSearch
	{
		public int RenewalId { get; set; }
		public string EstablishmentName { get; set; }
	}

	public class RenewalSearch
	{
		public long RenewalId { get; set; }
		public string CacNumber { get; set; }
		public string EstablishmentName { get; set; }
		public DateTime Date { get; set; }
		public long RenewalTypeId { get; set; }
		public decimal AmountDue { get; set; }
		public decimal TotalPaid { get; set; }
		public decimal Balance { get; set; }
	}
}
