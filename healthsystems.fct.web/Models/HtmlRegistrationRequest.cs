using System;
using healthsystems.fct.data;
using System.Collections.Generic;

namespace healthsystems.fct.web
{
	public class HtmlRegistrationRequest
	{
		public virtual int? Id { get; set; }
		public virtual string PhermcRegistrationNumber { get; set; }
		public virtual string CacNumber { get; set; }
		public virtual bool IsRenewal { get; set; }
		public virtual DateTime LastRenewalDate { get; set; }
		public virtual DateTime RegistrationDate { get; set; }

		public virtual string ProprietorFirstName { get; set; }
		public virtual string ProprietorLastName { get; set; }
		public virtual string ProprietorGender { get; set; }
		public virtual string ProprietorNinNumber { get; set; }
		public virtual string ProprietorMobile1 { get; set; }
		public virtual string ProprietorMobile2 { get; set; }
		public virtual string ProprietorEmailAddress { get; set; }
		public virtual bool ProprietorIsMedicalDirector { get; set; }

		public virtual string MedicalDirectorFirstName { get; set; }
		public virtual string MedicalDirectorLastName { get; set; }
		public virtual string MedicalDirectorGender { get; set; }
		public virtual string MedicalDirectorNinNumber { get; set; }
		public virtual string MedicalDirectorMobile1 { get; set; }
		public virtual string MedicalDirectorMobile2 { get; set; }
		public virtual string MedicalDirectorEmailAddress { get; set; }

		public virtual string EstablishmentName { get; set; }
		public virtual string TypeOfEstablishment { get; set; }
		public virtual int NoOfBeds { get; set; }
		public virtual string AddressLine1 { get; set; }
		public virtual string AddressLine2 { get; set; }
		public virtual int CategoryId { get; set; }
		public virtual int LocationId { get; set; }
		public virtual string Category { get; set; }
		public virtual string LandMark { get; set; }

		public List<RegistrationStaffing> RegistrationStaffing { get; set; }

		public List<RegistrationService> RegistrationServices { get; set; }

		public virtual string AdministratorFirstName { get; set; }
		public virtual string AdministratorLastName { get; set; }
		public virtual string AdministratorMobile1 { get; set; }
		public virtual string AdministratorMobile2 { get; set; }

		public virtual string ProfessionalBodyAttendance { get; set; }
		public virtual string ProfessionalBodyInvolvement { get; set; }
		public virtual string ProfessionalBodyRemarks { get; set; }
		public virtual string ProfessionalBodyFirstName { get; set; }
		public virtual string ProfessionalBodyPosition { get; set; }
		public virtual string ProfessionalBodyLastName { get; set; }
		public virtual DateTime ProfessionalBodySignatureDate { get; set; }

		public virtual bool AcceptanceDetailsAccepted { get; set; }
		public virtual string AcceptanceDetailsReason { get; set; }

		public virtual string Action { get; set; }
		public virtual bool AddPayment { get; set; }
		public virtual decimal RegistrationCosts { get; set; }
		public virtual string PaymentMethod { get; set; }
		public virtual string ReferenceNumber { get; set; }
		public virtual decimal AmountPaid { get; set; }
		public virtual string ReceivedFrom { get; set; }
		public virtual string ReceivedById { get; set; }
		public virtual string ReceivedByName { get; set; }

		public virtual int UserId { get; set; }
		public virtual string Username { get; set; }
		public virtual int StateId { get; set; }
	}
}

