using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using healthsystems.fct.common;
using healthsystems.fct.data;
using healthsystems.fct.data.Common;

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
                {/*
                    Registration entity;

                    if (id == 0)
                    {
                        entity = new Registration();
                    }
                    else
                    {
                        // collect objects
                        var data = new List<Registration>(session.CreateCriteria(typeof(Registration)).List<Registration>());

                        // collect single object
                        entity = data.FirstOrDefault(x => x.Id == id);
                    }

                    // trim excess
                    if (entity != null)
                    {
                        //if (entity.State != null) entity.State.Locations = null;
                    }

                    // return as HttpResponseMessage
                    return WebApiHelper.ObjectToHttpResponseMessage(entity);*/

                    var registrations = new List<Registration>(session.CreateCriteria(typeof(Registration)).List<Registration>());

                    var registration = registrations.FirstOrDefault(x => x.Id == id);

                    var staffings = session.CreateCriteria(typeof (Staffing)).List<Staffing>();
                    var services = session.CreateCriteria(typeof (Service)).List<Service>();

                    if (registration == null)
                    {
                        registration = new Registration();

                        foreach (var staffing in staffings)
                        {
                            registration.RegistrationStaffing.Add(new RegistrationStaffing
                            {
                                Registration = registration,
                                NumberOfStaff = 0,
                                Staffing = staffing
                            });
                        }

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
                        // Staffing profile
                        foreach (var staffing in staffings)
                        {
                            if (registration.RegistrationStaffing.FirstOrDefault(x => x.Staffing.Id == staffing.Id) == null)
                            {
                                var rs = new RegistrationStaffing
                                {
                                    Registration = registration,
                                    Staffing = staffing,
                                    NumberOfStaff = 0
                                };
                                registration.RegistrationStaffing.Add(rs);
                            }
                        }

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


        //public HttpResponseMessage Post([FromBody]HtmlRegistrationRequest htmlRegistrationRequest)
		public HttpResponseMessage Post([FromBody]Registration htmlRegistrationRequest)
        {

            using (var session = NHibernateHelper.CreateSessionFactory())
            {
                using (var transaction = session.BeginTransaction())
                {
					if (AuthHelper.JwtAuth () != 1) {
						return WebApiHelper.ObjectToHttpResponseMessage("No token was supplied", System.Net.HttpStatusCode.Unauthorized);
					}

					var stateId = Convert.ToInt32( AuthHelper.GetKey ("stateId") );
					
                    var registration = new Registration();
                    Renewal renewal = null;

                    if (htmlRegistrationRequest.Id != 0) // Existing Registration
                    {
                        // Select
                        var registrations = new List<Registration>(session.CreateCriteria(typeof(Registration)).List<Registration>());
                        registration = registrations.FirstOrDefault(x => x.Id == htmlRegistrationRequest.Id);
                    }
                    else // New Registration
                    {
                        var renewalType = new List<RenewalType>(session.CreateCriteria(typeof(RenewalType)).List<RenewalType>());
                        var renewalTypeToInsert = renewalType.FirstOrDefault(x => x.Id >= 1);

                        var costing = new List<Costing>(session.CreateCriteria(typeof(Costing)).List<Costing>());
						var costingTypeToInsert = costing.FirstOrDefault(x => x.Category.Id == htmlRegistrationRequest.Category.Id && x.State.Id == stateId);

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
                        registration.CacNumber = htmlRegistrationRequest.CacNumber ?? "";
						var isRenewal = htmlRegistrationRequest.Renewals.FirstOrDefault (x => x.Id == 0) != null ? true : false;
						if (isRenewal)
                        {
                            registration.LastRenewalDate = DateTime.Now;

                            var renewalType = new List<RenewalType>(session.CreateCriteria(typeof(RenewalType)).List<RenewalType>());
                            var renewalTypeToInsert = renewalType.FirstOrDefault(x => x.Id >= 2);

                            var costing = new List<Costing>(session.CreateCriteria(typeof(Costing)).List<Costing>());
                            var costingTypeToInsert = costing.FirstOrDefault(x => x.Category.Id == htmlRegistrationRequest.Category.Id && x.State.Id == stateId);

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
                            registration.LastRenewalDate = htmlRegistrationRequest.LastRenewalDate;
                        }

                        registration.RegistrationDate = htmlRegistrationRequest.RegistrationDate;
                        registration.ProprietorFirstName = htmlRegistrationRequest.ProprietorFirstName ?? "";
                        registration.ProprietorLastName = htmlRegistrationRequest.ProprietorLastName ?? "";
                        registration.ProprietorGender = htmlRegistrationRequest.ProprietorGender ?? "";
                        registration.ProprietorNinNumber = htmlRegistrationRequest.ProprietorNinNumber ?? "";
                        registration.ProprietorIsMedicalDirector = htmlRegistrationRequest.ProprietorIsMedicalDirector;
                        registration.ProprietorMobile1 = htmlRegistrationRequest.ProprietorMobile1 ?? "";
                        registration.ProprietorMobile2 = htmlRegistrationRequest.ProprietorMobile2 ?? "";
                        registration.ProprietorEmailAddress = htmlRegistrationRequest.ProprietorEmailAddress ?? "";

                        registration.MedicalDirectorFirstName = htmlRegistrationRequest.MedicalDirectorFirstName ?? "";
                        registration.MedicalDirectorLastName = htmlRegistrationRequest.MedicalDirectorLastName ?? "";
                        registration.MedicalDirectorGender = htmlRegistrationRequest.MedicalDirectorGender;
                        registration.MedicalDirectorNinNumber = htmlRegistrationRequest.MedicalDirectorNinNumber ?? "";
                        registration.MedicalDirectorMobile1 = htmlRegistrationRequest.MedicalDirectorMobile1 ?? "";
                        registration.MedicalDirectorMobile2 = htmlRegistrationRequest.MedicalDirectorMobile2 ?? "";
                        registration.MedicalDirectorEmailAddress = htmlRegistrationRequest.MedicalDirectorEmailAddress ?? "";

                        registration.AdministratorFirstName = htmlRegistrationRequest.AdministratorFirstName ?? "";
                        registration.AdministratorLastName = htmlRegistrationRequest.AdministratorLastName ?? "";
                        registration.AdministratorMobile1 = htmlRegistrationRequest.AdministratorMobile1 ?? "";
                        registration.AdministratorMobile2 = htmlRegistrationRequest.AdministratorMobile2 ?? "";

                        registration.EstablishmentName = htmlRegistrationRequest.EstablishmentName ?? "";
                        registration.EstablishmentType = htmlRegistrationRequest.EstablishmentType ?? "";
                        registration.NoOfBeds = htmlRegistrationRequest.NoOfBeds;

                        registration.AddressLine1 = htmlRegistrationRequest.AddressLine1 ?? "";
                        registration.AddressLine2 = htmlRegistrationRequest.AddressLine2 ?? "";
                        registration.LandMark = htmlRegistrationRequest.LandMark ?? "";

                        registration.Latitude = htmlRegistrationRequest.Latitude;
                        registration.Longitude = htmlRegistrationRequest.Longitude;

                        // Add 
                        foreach (var rs in htmlRegistrationRequest.RegistrationStaffing)
                        {
                            var registrationStaffing = registration.RegistrationStaffing.FirstOrDefault(x => x.Staffing.Name == rs.Staffing.Name);

                            if (registrationStaffing != null)
                            {
                                registrationStaffing.NumberOfStaff = rs.NumberOfStaff;
                            }
                            else
                            {
                                var staffToAdd = session.CreateCriteria(typeof(Staffing)).List<Staffing>().FirstOrDefault(x => x.Name == rs.Staffing.Name);

                                var regStaffing = new RegistrationStaffing
                                {
                                    Registration = registration,
                                    Staffing = staffToAdd,
                                    NumberOfStaff = rs.NumberOfStaff
                                };
                                // if not in db, add it
                                registration.AddRegistrationStaffing(regStaffing);
                            }

                        }

                        foreach (var rs in htmlRegistrationRequest.RegistrationServices)
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

                        registration.ProfessionalBodyAttendance = htmlRegistrationRequest.ProfessionalBodyAttendance ?? "";
                        registration.ProfessionalBodyInvolvement = htmlRegistrationRequest.ProfessionalBodyInvolvement ?? "";
                        registration.ProfessionalBodyRemarks = htmlRegistrationRequest.ProfessionalBodyRemarks ?? "";
                        registration.ProfessionalBodyFirstName = htmlRegistrationRequest.ProfessionalBodyFirstName ?? "";
                        registration.ProfessionalBodyLastName = htmlRegistrationRequest.ProfessionalBodyLastName ?? "";
                        registration.ProfessionalBodyPosition = htmlRegistrationRequest.ProfessionalBodyPosition ?? "";
                        registration.ProfessionalBodySignatureDate = htmlRegistrationRequest.ProfessionalBodySignatureDate;

                        registration.AcceptanceDetailsAccepted = htmlRegistrationRequest.AcceptanceDetailsAccepted;
                        registration.AcceptanceDetailsReason = htmlRegistrationRequest.AcceptanceDetailsReason ?? "";

                        registration.Category =
                            (from x in session.CreateCriteria(typeof(Category)).List<Category>()
                             where x.Id == htmlRegistrationRequest.Category.Id
                             select x).FirstOrDefault();

                        registration.Location =
                            (from x in session.CreateCriteria(typeof(Location)).List<Location>()
                             where x.Id == htmlRegistrationRequest.Location.Id
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
