using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace healthsystems.fct.data
{
    [Serializable]
    [DataContract]
    public class Registration
    {
        [DataMember]
        public virtual int Id { get; protected set; }
        [DataMember]
        public virtual string PhermcRegistrationNumber { get; set; }
        [DataMember]
        public virtual string CacNumber { get; set; }
        [DataMember]
        public virtual DateTime? LastRenewalDate { get; set; }
        [DataMember]
        public virtual DateTime RegistrationDate { get; set; }
        [DataMember]
        public virtual string ProprietorFirstName { get; set; }
        [DataMember]
        public virtual string ProprietorLastName { get; set; }
        [DataMember]
        public virtual string ProprietorGender { get; set; }
        [DataMember]
        public virtual string ProprietorNinNumber { get; set; }
        [DataMember]
        public virtual string ProprietorMobile1 { get; set; }
        [DataMember]
        public virtual string ProprietorMobile2 { get; set; }
        [DataMember]
        public virtual string ProprietorEmailAddress { get; set; }
        [DataMember]
        public virtual bool ProprietorIsMedicalDirector { get; set; }
        [DataMember]
        public virtual string MedicalDirectorFirstName { get; set; }
        [DataMember]
        public virtual string MedicalDirectorLastName { get; set; }
        [DataMember]
        public virtual string MedicalDirectorGender { get; set; }
        [DataMember]
        public virtual string MedicalDirectorNinNumber { get; set; }
        [DataMember]
        public virtual string MedicalDirectorMobile1 { get; set; }
        [DataMember]
        public virtual string MedicalDirectorMobile2 { get; set; }
        [DataMember]
        public virtual string MedicalDirectorEmailAddress { get; set; }
        [DataMember]
        public virtual string AdministratorFirstName { get; set; }
        [DataMember]
        public virtual string AdministratorLastName { get; set; }
        [DataMember]
        public virtual string AdministratorMobile1 { get; set; }
        [DataMember]
        public virtual string AdministratorMobile2 { get; set; }
        [DataMember]
        public virtual string EstablishmentName { get; set; }
        [DataMember]
        public virtual string EstablishmentType { get; set; }
        [DataMember]
        public virtual int NoOfBeds { get; set; }
        [DataMember]
        public virtual string AddressLine1 { get; set; }
        [DataMember]
        public virtual string AddressLine2 { get; set; }
        [DataMember]
        public virtual Category Category { get; set; }
        [DataMember]
        public virtual Location Location { get; set; }
        [DataMember]
        public virtual string Lga { get; set; }
        [DataMember]
        public virtual string LandMark { get; set; }
        [DataMember]
        public virtual string Latitude { get; set; }
        [DataMember]
        public virtual string Longitude { get; set; }
        [DataMember]
        public virtual IList<RegistrationStaffing> RegistrationStaffing { get; set; }
        [DataMember]
        public virtual IList<RegistrationService> RegistrationServices { get; set; }
        [DataMember]
        public virtual IList<Renewal> Renewals { get; set; }
        [DataMember]
        public virtual string ProfessionalBodyAttendance { get; set; }
        [DataMember]
        public virtual string ProfessionalBodyInvolvement { get; set; }
        [DataMember]
        public virtual string ProfessionalBodyRemarks { get; set; }
        [DataMember]
        public virtual string ProfessionalBodyFirstName { get; set; }
        [DataMember]
        public virtual string ProfessionalBodyPosition { get; set; }
        [DataMember]
        public virtual string ProfessionalBodyLastName { get; set; }
        [DataMember]
        public virtual DateTime ProfessionalBodySignatureDate { get; set; }
        [DataMember]
        public virtual bool AcceptanceDetailsAccepted { get; set; }
        [DataMember]
        public virtual string AcceptanceDetailsReason { get; set; }
        [DataMember]
        public virtual decimal RegistrationCosts { get; set; }
        [DataMember]
        public virtual string PaymentMethod { get; set; }
        [DataMember]
        public virtual string ReferenceNumber { get; set; }
        [DataMember]
        public virtual decimal AmountPaid { get; set; }
        [DataMember]
        public virtual DateTime Created { get; set; }
        [DataMember]
        public virtual DateTime Modified { get; set; }
        [DataMember]
        public virtual bool Deleted { get; set; }

        public Registration()
        {
            RegistrationStaffing = new List<RegistrationStaffing>();
            RegistrationServices = new List<RegistrationService>();
            Renewals = new List<Renewal>();
        }


        public virtual void AddRenewal(Renewal renewal)
        {
            renewal.Registration = this;
            Renewals.Add(renewal);
        }

        public virtual void AddRegistrationStaffing(RegistrationStaffing registrationStaffing)
        {
            registrationStaffing.Registration = this;
            RegistrationStaffing.Add(registrationStaffing);
        }

        public virtual void AddRegistrationService(RegistrationService registrationService)
        {
            registrationService.Registration = this;
            RegistrationServices.Add(registrationService);
        }
    }
}
