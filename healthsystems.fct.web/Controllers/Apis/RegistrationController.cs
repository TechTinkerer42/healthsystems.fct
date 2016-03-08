using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using healthsystems.fct.common;
using healthsystems.fct.data;
using healthsystems.fct.data.Common;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Linq;
using System.Net;
using Newtonsoft.Json;

namespace healthsystems.fct.web
{
    public class RegistrationController : ApiController
    {
        public HttpResponseMessage Get()
        {
            using (var session = NHibernateHelper.CreateSessionFactory())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var registrations = session.CreateCriteria(typeof(Registration)).List<Registration>();
                    return WebApiHelper.ObjectToHttpResponseMessage(registrations);
                }
            }
        }

        public HttpResponseMessage Get(int id)
        {
            using (var session = NHibernateHelper.CreateSessionFactory())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var registrations = new List<Registration>(session.CreateCriteria(typeof(Registration)).List<Registration>());

                    var registration = registrations.FirstOrDefault(x => x.Id == id);

                    var staffings = session.CreateCriteria(typeof (Staffing)).List<Staffing>();
                    var services = session.CreateCriteria(typeof (Service)).List<Service>();

                    if (registration == null)
                    {
                        registration = new Registration();
					
                        foreach (var service in services)
                        {
                            registration.RegistrationServices.Add(new RegistrationService
                            {
                                Registration = registration,
                                Selected = false,
                                Service = service
                            });
                        }

                    }
                    else
                    {

                        // Services
                        foreach (var service in services)
                        {
                            if (registration.RegistrationServices.FirstOrDefault(x => x.Service.Id == service.Id) == null)
                            {
                                var rs = new RegistrationService
                                {
                                    Registration = registration,
                                    Service = service,
                                    Selected = false
                                };
                                registration.RegistrationServices.Add(rs);
                            }
                        }
                    }

                    // trim excess
                    if (registration.Category != null)
                    {
                        if (registration.Category.Costings != null) registration.Category.Costings = null;
                    }

                    return WebApiHelper.ObjectToHttpResponseMessage(registration);
                }

            }
        }


		/*

		public HttpResponseMessage Post([FromBody]FormDataCollection  body)
		{
			var field1Values = body.GetValues("ProfessionalBody");

			return null;
		}
         * 
         * 
*/

        Registration PopulateRegistration(JToken j)
        {
            var registration = new Registration();

            // Main details
            registration.PhermcRegistrationNumber = j["PhermcRegistrationNumber"].ToObject<string>();
            registration.LastRenewalDate = j["LastRenewalDate"].ToObject<DateTime?>();
            registration.CacNumber = (string)j["CacNumber"];
            registration.RegistrationDate = j["RegistrationDate"].ToObject<DateTime>();

            // Proprietor details
            registration.ProprietorGender = (string)j["ProprietorGender"];
            registration.ProprietorFirstName = (string)j["ProprietorFirstName"];
            registration.ProprietorNinNumber = (string)j["ProprietorNinNumber"];
            registration.ProprietorEmailAddress = (string)j["ProprietorEmailAddress"];
            registration.PaymentMethod = (string)j["ProprietorLastName"];
            registration.ProprietorLastName = (string)j["ProprietorLastName"];
            registration.ProprietorMobile2 = (string)j["ProprietorMobile2"];
            registration.ProprietorIsMedicalDirector = (bool)j["ProprietorIsMedicalDirector"];
            registration.ProprietorMobile1 = (string)j["ProprietorMobile1"];

            // Professional body details
            registration.ProfessionalBodyAttendance = (string) j["ProfessionalBodyAttendance"];
            registration.ProfessionalBodyFirstName = (string) j["ProfessionalBodyFirstName"];
            registration.ProfessionalBodyLastName = (string) j["ProfessionalBodyLastName"];
            registration.ProfessionalBodyRemarks = (string) j["ProfessionalBodyRemarks"];
            registration.ProfessionalBodyPosition = (string) j["ProfessionalBodyPosition"];
            registration.MedicalDirectorFirstName = (string) j["MedicalDirectorFirstName"];
            registration.AddressLine1 = (string) j["AddressLine1"];
            registration.AddressLine2 = (string) j["AddressLine2"];
            registration.MedicalDirectorEmailAddress = (string) j["MedicalDirectorEmailAddress"];
            registration.AcceptanceDetailsReason = (string) j["AcceptanceDetailsReason"];


            registration.MedicalDirectorNinNumber = (string) j["MedicalDirectorNinNumber"];
            registration.MedicalDirectorLastName = (string) j["MedicalDirectorLastName"];
            registration.MedicalDirectorGender = (string) j["MedicalDirectorGender"];
            registration.MedicalDirectorMobile1 = (string) j["MedicalDirectorMobile1"];
            registration.MedicalDirectorMobile2 = (string)j["MedicalDirectorMobile2"];


            registration.EstablishmentName = (string)j["EstablishmentName"];
			registration.TypeOfEstablishment = j["TypeOfEstablishment"].ToObject<TypeOfEstablishment>();

			registration.RegistrationTypeOfEstablishmentStaffing = j["RegistrationTypeOfEstablishmentStaffing"].ToObject<List<RegistrationTypeOfEstablishmentStaffing>>();

            registration.NoOfBeds = (int)j["NoOfBeds"];


            registration.AddressLine1 = j["AddressLine1"].ToObject<string>();
            registration.AddressLine2 = j["AddressLine2"].ToObject<string>();
            /*
            registration.Category = JsonConvert.DeserializeObject(j.ToString(), typeof (Category)) as Category;
            registration.Location = JsonConvert.DeserializeObject(j.ToString(), typeof(Location)) as Location;
            */
            registration.Location = j["Location"].ToObject<Location>();
            registration.Category = j["Location"].ToObject<Category>();
            registration.LandMark = j["LandMark"].ToObject<string>();

            // Number of Staff

            // Services
            registration.RegistrationServices = j["RegistrationServices"].ToObject<List<RegistrationService>>();

            // Administrator details
            registration.AdministratorFirstName = (string)j["AdministratorFirstName"];
            registration.AdministratorLastName = (string)j["AdministratorLastName"];
            registration.AdministratorMobile1 = (string)j["AdministratorMobile1"];
            registration.AdministratorMobile2 = (string)j["AdministratorMobile2"];

            // Professional details
            registration.ProfessionalBody = j["ProfessionalBody"].ToObject<ProfessionalBody>();
            registration.ProfessionalBodyAttendance = j["ProfessionalBodyAttendance"].ToObject<string>();
            registration.ProfessionalBodyInvolvement = j["ProfessionalBodyInvolvement"].ToObject<string>();
            registration.ProfessionalBodyRemarks = j["ProfessionalBodyRemarks"].ToObject<string>();
            registration.ProfessionalBodyFirstName = j["ProfessionalBodyFirstName"].ToObject<string>();
            registration.ProfessionalBodyLastName = j["ProfessionalBodyLastName"].ToObject<string>();
            registration.ProfessionalBodyPosition = j["ProfessionalBodyPosition"].ToObject<string>();

            registration.AcceptanceDetailsAccepted = (bool)j["AcceptanceDetailsAccepted"];
            registration.AcceptanceDetailsReason = j["AcceptanceDetailsReason"].ToObject<string>();
            registration.ProfessionalBodySignatureDate = (DateTime)j["ProfessionalBodySignatureDate"];

            registration.Lga = (string) j["Lga"];
            
            registration.ReferenceNumber = (string) j["ReferenceNumber"];

            registration.AmountPaid = (decimal)j["AmountPaid"];
            registration.RegistrationCosts = j.Value<decimal>("RegistrationCosts");

            registration.Created = (DateTime)j["Created"];
            registration.Deleted = (bool)j["Deleted"];
            registration.Modified = (DateTime)j["Modified"];

            return registration;

        }

        public HttpResponseMessage Post([FromBody]JToken body)
		{

		    using (var session = NHibernateHelper.CreateSessionFactory())
		    {
		        using (var transaction = session.BeginTransaction())
		        {
                    if (AuthHelper.JwtAuth() != 1)
                    {
                        return WebApiHelper.ObjectToHttpResponseMessage("No token was supplied", System.Net.HttpStatusCode.Unauthorized);
                    }


                    var stateId = Convert.ToInt32(AuthHelper.GetKey("stateId"));

                    var id = body.Value<decimal>("Id");

		            var r = PopulateRegistration(body);

                    var registration = new Registration();
                    Renewal renewal = null;

                    if (id != 0) 
                    {
                        // Existing Registration
                        var registrations = new List<Registration>(session.CreateCriteria(typeof(Registration)).List<Registration>());
                        registration = registrations.FirstOrDefault(x => x.Id == id);
                    }
                    else
                    {
                        // New Registration
                        var renewalType = new List<RenewalType>(session.CreateCriteria(typeof(RenewalType)).List<RenewalType>());
                        var renewalTypeToInsert = renewalType.FirstOrDefault(x => x.Id >= 1);

                        var costing = new List<Costing>(session.CreateCriteria(typeof(Costing)).List<Costing>());
                        var costingTypeToInsert = costing.FirstOrDefault(x => x.Category.Id == r.Category.Id && x.State.Id == stateId);

                        renewal = new Renewal
                        {
                            Date = DateTime.Now,
                            RenewalType = renewalTypeToInsert,
                            Registration = registration,
                            Amount = costingTypeToInsert.RenewalCost
                        };

                        registration.Renewals.Add(renewal);
                    }

                    if (registration != null)
                    {
                        //
                        registration.CacNumber = r.CacNumber ?? "";
                        var isRenewal = r.Renewals.FirstOrDefault(x => x.Id == 0) != null ? true : false;
                        if (isRenewal)
                        {
                            registration.LastRenewalDate = DateTime.Now;

                            var renewalType = new List<RenewalType>(session.CreateCriteria(typeof(RenewalType)).List<RenewalType>());
                            var renewalTypeToInsert = renewalType.FirstOrDefault(x => x.Id >= 2);

                            var costing = new List<Costing>(session.CreateCriteria(typeof(Costing)).List<Costing>());
                            var costingTypeToInsert = costing.FirstOrDefault(x => x.Category.Id == r.Category.Id && x.State.Id == stateId);

                            renewal = new Renewal
                            {
                                Date = DateTime.Now,
                                RenewalType = renewalTypeToInsert,
                                Registration = registration,
                                Amount = costingTypeToInsert.RenewalCost
                            };
                            registration.Renewals.Add(renewal);
                        }
                        else
                        {
                            registration.LastRenewalDate = r.LastRenewalDate;
                        }

                        registration.RegistrationDate = r.RegistrationDate;
                        registration.ProprietorFirstName = r.ProprietorFirstName ?? "";
                        registration.ProprietorLastName = r.ProprietorLastName ?? "";
                        registration.ProprietorGender = r.ProprietorGender ?? "";
                        registration.ProprietorNinNumber = r.ProprietorNinNumber ?? "";
                        registration.ProprietorIsMedicalDirector = r.ProprietorIsMedicalDirector;
                        registration.ProprietorMobile1 = r.ProprietorMobile1 ?? "";
                        registration.ProprietorMobile2 = r.ProprietorMobile2 ?? "";
                        registration.ProprietorEmailAddress = r.ProprietorEmailAddress ?? "";

                        registration.MedicalDirectorFirstName = r.MedicalDirectorFirstName ?? "";
                        registration.MedicalDirectorLastName = r.MedicalDirectorLastName ?? "";
                        registration.MedicalDirectorGender = r.MedicalDirectorGender;
                        registration.MedicalDirectorNinNumber = r.MedicalDirectorNinNumber ?? "";
                        registration.MedicalDirectorMobile1 = r.MedicalDirectorMobile1 ?? "";
                        registration.MedicalDirectorMobile2 = r.MedicalDirectorMobile2 ?? "";
                        registration.MedicalDirectorEmailAddress = r.MedicalDirectorEmailAddress ?? "";

                        registration.AdministratorFirstName = r.AdministratorFirstName ?? "";
                        registration.AdministratorLastName = r.AdministratorLastName ?? "";
                        registration.AdministratorMobile1 = r.AdministratorMobile1 ?? "";
                        registration.AdministratorMobile2 = r.AdministratorMobile2 ?? "";

                        registration.EstablishmentName = r.EstablishmentName ?? "";

                        registration.TypeOfEstablishment = r.TypeOfEstablishment;

                        registration.ProfessionalBody = r.ProfessionalBody;

                        registration.NoOfBeds = r.NoOfBeds;

                        registration.AddressLine1 = r.AddressLine1 ?? "";
                        registration.AddressLine2 = r.AddressLine2 ?? "";
                        registration.LandMark = r.LandMark ?? "";

                        registration.Latitude = r.Latitude;
                        registration.Longitude = r.Longitude;

                        // Add 
                        foreach (var rs in r.RegistrationTypeOfEstablishmentStaffing)
                        {
                            rs.Registration = registration;
                        }
                        registration.RegistrationTypeOfEstablishmentStaffing = r.RegistrationTypeOfEstablishmentStaffing;
						

                        foreach (var rs in r.RegistrationServices)
                        {
                            var registrationService = registration.RegistrationServices.FirstOrDefault(x => x.Service.Name == rs.Service.Name);

                            if (registrationService != null)
                            {
                                registrationService.Selected = rs.Selected;
                            }
                            else
                            {
                                var serviceToAdd = session.CreateCriteria(typeof(Service)).List<Service>().FirstOrDefault(x => x.Name == rs.Service.Name);

                                var regService = new RegistrationService
                                {
                                    Registration = registration,
                                    Service = serviceToAdd,
                                    Selected = rs.Selected
                                };
                                // if not in db, add it
                                registration.AddRegistrationService(regService);
                            }

                        }

                        registration.ProfessionalBodyAttendance = r.ProfessionalBodyAttendance ?? "";
                        registration.ProfessionalBodyInvolvement = r.ProfessionalBodyInvolvement ?? "";
                        registration.ProfessionalBodyRemarks = r.ProfessionalBodyRemarks ?? "";
                        registration.ProfessionalBodyFirstName = r.ProfessionalBodyFirstName ?? "";
                        registration.ProfessionalBodyLastName = r.ProfessionalBodyLastName ?? "";
                        registration.ProfessionalBodyPosition = r.ProfessionalBodyPosition ?? "";
                        registration.ProfessionalBodySignatureDate = r.ProfessionalBodySignatureDate;

                        registration.AcceptanceDetailsAccepted = r.AcceptanceDetailsAccepted;
                        registration.AcceptanceDetailsReason = r.AcceptanceDetailsReason ?? "";

                        registration.Category =
                            (from x in session.CreateCriteria(typeof(Category)).List<Category>()
                             where x.Id == r.Category.Id
                             select x).FirstOrDefault();

                        registration.Location =
                            (from x in session.CreateCriteria(typeof(Location)).List<Location>()
                             where x.Id == r.Location.Id
                             select x).FirstOrDefault();

                        registration.Created = DateTime.Now;
                        registration.Modified = DateTime.Now;
                        registration.Deleted = false;
                    }

                    session.SaveOrUpdate(registration);
                    if (renewal != null)
                    {
                        session.SaveOrUpdate(renewal);
                    }
                    transaction.Commit();

                    return Get(registration.Id);
		        }
		    }
		}

        HttpResponseMessage StringToJsonActionResult(string jsonString)
        {
            var content = new StringContent(jsonString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = new HttpResponseMessage { Content = content };

            return response;
        }


    }
}
