using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using healthsystems.fct.data;
using healthsystems.fct.common;
using healthsystems.fct.data.Common;
using healthsystems.fct.data.DataClasses;

namespace healthsystems.fct.web.Controllers
{
	public class InitController : Controller
	{
		//
		// GET: /Init/

		public ActionResult Index()
		{
			// backup database

			// initialize db
			DbInit();

			// add data

			return Content
				(
					"Db Init ran successfuly"
				);
		}

		public ActionResult Query()
		{
			using (var session = NHibernateHelper.CreateSessionFactory())
			{
				using (var transaction = session.BeginTransaction())
				{
					var sql = "SELECT Ren.Id AS 'RenewalId', Reg.EstablishmentName, Ren.[Date], Ren.RenewalType_Id AS 'RenewalTypeId', Ren.Amount AS 'AmountDue', IFNULL(TR.TotalPaid, 0) AS 'TotalPaid', Ren.Amount - IFNULL(TR.TotalPaid, 0) AS 'Balance' FROM Renewal Ren LEFT JOIN (SELECT Renewal_Id, COUNT(Renewal_Id), SUM(Amount) AS 'TotalPaid' FROM [Transaction] GROUP BY Renewal_Id) AS TR ON Ren.Id = TR.Renewal_Id JOIN Registration Reg ON Ren.Registration_id = Reg.Id";

					var payments =
						from x in  session.CreateSQLQuery(sql).DynamicList()
						select new
					{
						RenewalId = x.Id,
						x.EstablishmentName,
						x.Date,
						x.RenewalTypeId,
						x.AmountDue,
						x.TotalPaid,
						x.Balance
					};
				}
			}

			return Content("abc");

		}

		public static void AddLocationsToState(State state, params Location[] locations)
		{
			foreach (var location in locations)
			{
				state.AddLocation(location);
			}
		}

		public static void AddCostingToCategory(Category category, params Costing[] costings)
		{
			foreach (var location in costings)
			{
				category.AddCosting(location);
			}
		}

		public static void AddRenewalToRegistration(Registration registration, params Renewal[] renewals)
		{
			foreach (var renewal in renewals)
			{
				registration.AddRenewal(renewal);
			}
		}

		public static void AddServicesToRegistration(Registration registration, params Service[] services)
		{
			foreach (var service in services)
			{
				//registration.AddService(service);
			}
		}

		public static void AddStaffToRegistration(Registration registration, params Staffing[] staffings)
		{
			foreach (var staff in staffings)
			{
				//registration.AddRegistrationStaffing(staff);
			}
		}

		void DbInit()
		{
			NHibernateHelper.CreateSessionFactory(NHibernateHelper.Dialect.Sqlite, true);

			using (var session = NHibernateHelper.CreateSessionFactory())
			{
				using (var transaction = session.BeginTransaction())
				{
					// Add state
					var state01 = new State { Name = "Federal Capital Territory", Active = true };

					// Add locations
					var state01Location01 = new Location { Name = "Abaji" };
					var state01Location02 = new Location { Name = "Abuja" };
					var state01Location03 = new Location { Name = "Gwagwalada" };
					var state01Location04 = new Location { Name = "Kuje" };
					var state01Location05 = new Location { Name = "Bwari" };
					var state01Location06 = new Location { Name = "Kwali" };

					// logical add of locations
					AddLocationsToState(state01,
						state01Location01,
						state01Location02,
						state01Location03,
						state01Location04,
						state01Location05,
						state01Location06
					);


					// insert categories (3 categories)
					var category01 = new Category { Name = "Urban"};
					var category02 = new Category { Name = "Semi-Urban" };
					var category03 = new Category { Name = "Informal" };

					// add costing (insert some category prices for each)
					var costing01 = new Costing { EffectiveDate = DateTime.Now, RegistrationCost = 300.00M, RenewalCost = 200.00M, Category = category01, State = state01,};
					var costing02 = new Costing { EffectiveDate = DateTime.Now, RegistrationCost = 200.00M, RenewalCost = 100.00M, Category = category02, State = state01, };
					var costing03 = new Costing { EffectiveDate = DateTime.Now, RegistrationCost = 100.00M, RenewalCost = 50.00M, Category = category03, State = state01, };

					AddCostingToCategory(category01, costing01);
					AddCostingToCategory(category02, costing02);
					AddCostingToCategory(category03, costing03);

					// insert permissions
					var permission01 = new Permission { Name = "Search Registrations" };
					var permission02 = new Permission { Name = "Perform Registrations" };
					var permission03 = new Permission { Name = "Manage Payments" };
					var permission04 = new Permission { Name = "Manage Masterfiles" };

					session.SaveOrUpdate(permission01);
					session.SaveOrUpdate(permission02);
					session.SaveOrUpdate(permission03);
					session.SaveOrUpdate(permission04);

					// insert roles
					var role01 = new Role { Name = "Admin"};
					var role02 = new Role { Name = "Clerk" };

					session.SaveOrUpdate(role01);
					session.SaveOrUpdate(role02);

					var perms = new List<Permission>(session.CreateCriteria(typeof(Permission)).List<Permission>());
					foreach (var p in perms)
					{
						var rp = new RolePermission
						{
							Role = role01,
							Permission = p
						};
						session.SaveOrUpdate(rp);
					}


					// insert user (state, role, permission)
					var user01 = new User
					{
						FirstName = "FCT Admin",
						LastName = "",
						Username = "manager",
						Password = "manager",
						EmailAddress = "admin@healthsystems.co.za",
						Mobile = "",
						Active = true,
						Created = DateTime.Now,
						Modified = DateTime.Now,
						Roles = new List<Role>
						{
							role01,
							role02
						},
						State = state01
					};

					var user02 = new User
					{
						FirstName = "Lyall",
						LastName = "van der Linde",
						Username = "lyall",
						Password = "lyall01",
						EmailAddress = "lyall@healthsystems.co.za",
						Mobile = "0793874802",
						Active = true,
						Created = DateTime.Now,
						Modified = DateTime.Now,
						Roles = new List<Role>
						{
							role02
						},
						State = state01
					};

					// add services
					var service01 = new Service { Name = "Doctors" };
					var service02 = new Service { Name = "Radiology" };
					var service03 = new Service { Name = "Pathology" };
					var service04 = new Service { Name = "Theatre" };

					session.SaveOrUpdate(service01);
					session.SaveOrUpdate(service02);
					session.SaveOrUpdate(service03);
					session.SaveOrUpdate(service04);

					// add staffings
					var staffings01 = new Staffing { Name = "Medical Doctor" };
					var staffings02 = new Staffing { Name = "Nurse" };
					var staffings03 = new Staffing { Name = "Medical Lab Assistant" };
					var staffings04 = new Staffing { Name = "Pharmacist" };

					session.SaveOrUpdate(staffings01);
					session.SaveOrUpdate(staffings02);
					session.SaveOrUpdate(staffings03);
					session.SaveOrUpdate(staffings04);

					// register establishment (location, category, renewal)
					var categories = new List<Category>(session.CreateCriteria(typeof(Category)).List<Category>());
					var category = categories.FirstOrDefault(x => x.Name.Equals("Urban"));

					var locations = new List<Location>(session.CreateCriteria(typeof(Location)).List<Location>());
					var location = locations.FirstOrDefault(x => x.State.Id == 1);

					var estType = new TypeOfEstablishment ();
					estType.Name = "Hospital";

					estType.AddStaff (staffings01);
					estType.AddStaff (staffings02);
					estType.AddStaff (staffings03);
					estType.AddStaff (staffings04);

					session.Save (estType);

					var body = new ProfessionalBody ();
					body.Name = "MEDICAL AND DENTAL COUNCIL OF NIGERIA (MDCN)";
					session.Save (body);

					//Staffing

					var registration = new Registration
					{
						Category = category,
						Location = location,
						LastRenewalDate = null,
						ProfessionalBodyAttendance = "",
						ProfessionalBodyFirstName = "",
						ProfessionalBodyLastName = "",
						ProfessionalBodyRemarks = "",
						ProfessionalBodyPosition = "",
						MedicalDirectorFirstName = "",
						AddressLine1 = "",
						AddressLine2 = "",
						MedicalDirectorEmailAddress = "",
						AcceptanceDetailsReason = "",
						ProprietorGender = "",
						ProprietorFirstName = "",
						ProprietorNinNumber = "",
						ProfessionalBodyInvolvement = "",
						ProprietorEmailAddress = "",
						Lga = "",
						CacNumber = "",
						MedicalDirectorNinNumber = "",
						MedicalDirectorLastName = "",
						RegistrationDate = DateTime.Now,
						MedicalDirectorGender = "",
						EstablishmentName = "",
						PhermcRegistrationNumber = "",
						AdministratorMobile1 = "",
						ProprietorIsMedicalDirector = false,
						AcceptanceDetailsAccepted = true,
						ProprietorMobile1 = "",
						MedicalDirectorMobile1 = "",
						LandMark = "",
						AdministratorFirstName = "",
						AdministratorLastName = "",
						AdministratorMobile2 = "",
						AmountPaid = 200.00M,
						Created = DateTime.Now,
						Deleted = false,
						MedicalDirectorMobile2 = "",
						Modified = DateTime.Now,
						NoOfBeds = 10,
						PaymentMethod = null,
						ProfessionalBodySignatureDate = DateTime.Now,
						ProprietorLastName = "",
						ProprietorMobile2 = "",
						ReferenceNumber = "",
						RegistrationCosts = 0,

						TypeOfEstablishment = estType,
						ProfessionalBody = body
					};

					// add services
					var services = new List<Service>(session.CreateCriteria(typeof(Service)).List<Service>());
					var servicesToInsert = services.Where(x => x.Id >= 1);
					foreach (var s in servicesToInsert)
					{
						var rs = new RegistrationService
						{
							Registration = registration,
							Service = s,
							Selected = true
						};
						registration.AddRegistrationService(rs);
					}




					// add staffing
					var numbersToAdd = 0;
					foreach (var s in estType.Staffings) {
						
						var rts = new RegistrationTypeOfEstablishmentStaffing ();
						rts.TypeOfEstablishment = estType;
						rts.Staffing = s;
						rts.NumberOfStaff = ++numbersToAdd;

						registration.AddRegistrationTypeOfEstablishmentStaffing (rts);
					}



					// insert transactionTypes
					var renewalType01 = new RenewalType
					{
						Name = "Registration"
					};
					var renewalType02 = new RenewalType
					{
						Name = "Renewal"
					};
					session.SaveOrUpdate(renewalType01);
					session.SaveOrUpdate(renewalType02);

					// insert paymentTypes
					var paymentTypes01 = new PaymentType
					{
						Name = "Cash"
					};
					var paymentTypes02 = new PaymentType
					{
						Name = "Bank Deposit"
					};
					session.SaveOrUpdate(paymentTypes01);
					session.SaveOrUpdate(paymentTypes02);

					var renewalType = new List<RenewalType>(session.CreateCriteria(typeof(RenewalType)).List<RenewalType>());
					var renewalTypeToInsert = renewalType.FirstOrDefault(x => x.Id >= 1);

					// insert a renewal
					var renewal = new Renewal
					{
						Registration = registration,
						Date = DateTime.Now,
						Amount = 200.00M,
						Paid = false,
						RenewalType = renewalTypeToInsert
					};

					// insert transactionTypes
					var transactionType01 = new TransactionType
					{
						Name = "Registration"
					};
					var transactionType02 = new TransactionType
					{
						Name = "Renewal"
					};
					session.SaveOrUpdate(transactionType01);
					session.SaveOrUpdate(transactionType02);

					// insert a payment
					var transactionType = new List<TransactionType>(session.CreateCriteria(typeof(TransactionType)).List<TransactionType>());
					var transactionTypeToInsert = transactionType.FirstOrDefault(x => x.Id >= 1);

					var newTransaction = new Transaction
					{
						Amount = 200.00M,
						Date = DateTime.Now,
						Renewal = renewal,
						TransactionType = transactionTypeToInsert,
						PaymentType =  paymentTypes01,
						ReceivedFrom = "Alison"
					};

					session.SaveOrUpdate(state01);
					session.SaveOrUpdate(category01);
					session.SaveOrUpdate(category02);
					session.SaveOrUpdate(category03);

					session.SaveOrUpdate(user01);
					session.SaveOrUpdate(user02);

					session.SaveOrUpdate(registration);
					//session.SaveOrUpdate(renewal);
					//session.SaveOrUpdate(newTransaction);


                    // *************

                    var q1 = new Question();
                    q1.Query = "A";

                    var q2 = new Question();
                    q2.Query = "B";

                    session.SaveOrUpdate(q1);
                    session.SaveOrUpdate(q2);

                    var survey = new Survey();
                    survey.Name = "Lyall";
                    survey.Surname = "van der Linde";
                    survey.EmailAddress = "lyall@gmail.com";
                    survey.MobileNumber = "0716541254";
                    survey.Registration = registration;

                    session.SaveOrUpdate(survey);

                    var surveyQuestion = new SurveyQuestion();
                    surveyQuestion.Survey = survey;
                    surveyQuestion.Question = q1.Query;
                    surveyQuestion.Rating = 4.2;

                    session.SaveOrUpdate(surveyQuestion);

                    surveyQuestion = new SurveyQuestion();
                    surveyQuestion.Survey = survey;
                    surveyQuestion.Question = q2.Query;
                    surveyQuestion.Rating = 5.3;

                    session.SaveOrUpdate(surveyQuestion);

                    // *************

					transaction.Commit();
				}
			}

		}


		private IEnumerable<Category> Categories()
		{
			return new List<Category>
			{
				new Category
				{
					Name = "",
					Description = "",
					Costings = new List<Costing>
					{
						new Costing
						{
							EffectiveDate = DateTime.Now,
							RegistrationCost = 500.00M,
							RenewalCost = 250.00M
						}
					}
				}
			};
		}

		private IEnumerable<Role> Roles()
		{
			return new List<Role>
			{
				new Role
				{
					Name = "Admin",
					Description = "Admin"
				},
				new Role
				{
					Name = "Clerk",
					Description = "Clerk"
				}
			};
		}

		private IEnumerable<Permission> Permissions()
		{
			return new List<Permission>
			{
				new Permission
				{
					Name = "Search Registrations",
					Description = "Search Registrations"
				},
				new Permission
				{
					Name = "Perform Registrations",
					Description = "Perform Registrations"
				},
				new Permission
				{
					Name = "Search Payments",
					Description = "Search Payments"
				},
				new Permission
				{
					Name = "Perform Payments",
					Description = "Perform Payments"
				},
				new Permission
				{
					Name = "Manage Masterfiles",
					Description = "Manage Masterfiles"
				},
			};
		}

		private IEnumerable<User> Users()
		{
			return new List<User>
			{
				new User
				{
					FirstName = "Lyall",
					LastName = "van der Linde",
					Username = "lyall",
					Password = "lyall01",
					EmailAddress = "lyall@healthsystems.co.za",
					Mobile = "0793874802",
					Active = true,
					Created = DateTime.Now,
					Modified = DateTime.Now,
				}
			};
		}

	}
}
